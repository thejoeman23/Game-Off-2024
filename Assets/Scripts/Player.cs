using System;
using System.Linq;
using TMPro;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeakingDistance = 5f;
    public static int score;

    public delegate void Subject(GameState gs);

    public static Subject state;
    private GameState _gs;

    private void Start()
    {
        score = 0;
        var db = GameObject.Find("DialogueBackground");
        _gs = new GameState(
            this,
            db,
            db.GetComponentInChildren<TMP_Text>()
        );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeAction();
            state?.Invoke(_gs); // all actions update gameState
        }
    }

    // Call the correct method with the correct parameters to execute an action if possible
    // action priority: pick up package > talk to npc > clear snow
    private void TakeAction()
    {
        // if a (package is close enough then pick it up) -> add to the game state and return
        // GameObject package = NearestWithTag("package");
        GameObject package = null;
        if (package)
        {
            PickUp(package);
        }

        // if (npc character is close enough) -> talk to them and return; else clear dialog and don't return
        GameObject npc = NearestWithTag("NPC");
        if (npc && Vector3.Distance(npc.transform.position, transform.position) < maxSpeakingDistance)
        {
            SpeakTo(npc.name);
            return;
        }
        SpeakTo(null);

        // if (snow is covering the tile in front of the player) -> clearSnow(snowPosition)
        ClearSnow(Vector2.zero);
    }

    private GameObject NearestWithTag(string tagName)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
        if (objs.Length == 0) return null;
        var distances = objs.Select(obj => // distances from player to the npcs
            Vector3.Distance(obj.transform.position, transform.position)).ToArray();
        var nearest = distances.Aggregate((c, d) => c < d ? c : d); // find shortest distance
        return objs[Array.IndexOf(distances, nearest)]; // get the index of the npc with the smallest distance
    }

    // add npc name to dialog list and update the game state
    private void SpeakTo(string nameOfNpc)
    {
        _gs.Dialog.Add(nameOfNpc);
        if (nameOfNpc == null) _gs.Text.text = string.Empty; // clear text if no npc
    }

    private void PickUp(GameObject package)
    {
        if (!package) return;
        _gs.Inventory.Add("PackageA");
    }

    // clearing means replacing the snow tile with either a package, obstacle or nothing
    private void ClearSnow(Vector2 gridPosition)
    {
    }
}