using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeakingDistance = 5f;

    // allow scripts to access GameState by observing Player.state
    public delegate void Subject(GameState gs);

    public static Subject state;
    private GameState gs;

    private void Start()
    {
        gs = new GameState(
            name,
            GameObject.Find("DialogBox").GetComponent<TMP_Text>()
        );
    }

    private void Update()
    {
        // speak to the nearest npc within speaking distance when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject npc = NearestWithTag("NPC");
            if (npc && Vector3.Distance(npc.transform.position, transform.position) < maxSpeakingDistance)
                SpeakTo(npc.name);
            else SpeakTo(null);
        }
    }

    private GameObject NearestWithTag(string tagName)
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(tagName);
        if (npcs.Length == 0) return null;
        float[] distances = npcs.Select(npc => // distances from player to the npcs
            Vector3.Distance(npc.transform.position, transform.position)).ToArray();
        float nearest = distances.Aggregate((c, d) => c < d ? c : d); // find shortest distance
        return npcs[Array.IndexOf(distances, nearest)]; // get the index of the npc with the smallest distance
    }

    // add npc name to dialog list and update the game state
    private void SpeakTo(string nameOfNpc)
    {
        gs.Dialog.Add(nameOfNpc);
        if (nameOfNpc == null) gs.DialogBox.text = string.Empty; // clear text if no npc
       state?.Invoke(gs);
    }
}