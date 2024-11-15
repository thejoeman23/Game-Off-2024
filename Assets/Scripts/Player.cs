using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float actionDistance = 5f;
    public static int completedDeliveriesCount; // value to be displayed on the ui to show progress

    public delegate void Subject(GameState gs);

    public static Subject state;
    private GameState _gs;

    private void Start()
    {
        completedDeliveriesCount = 0;
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
        GameObject package = NearestWithTag("Package");
        if (package && Vector3.Distance(package.transform.position, transform.position) < actionDistance)
        {
            PickUp(package.name);
            package.SetActive(false); // hide the package when it is picked up
            return;
        }

        GameObject npc = NearestWithTag("NPC");
        if (npc && Vector3.Distance(npc.transform.position, transform.position) < actionDistance)
        {
            SpeakTo(npc.name);
            return;
        }

        // if ( snow in front of me )
        ClearSnow(Vector2.zero);
    }

    private GameObject NearestWithTag(string tagName)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
        if (objs.Length == 0) return null;
        var distances = objs.Select(obj => // calculate distances from the player to the objects
            Vector3.Distance(obj.transform.position, transform.position)).ToArray();
        var nearest = distances.Aggregate((c, d) => c < d ? c : d); // find the index of the shortest distance
        return objs[Array.IndexOf(distances, nearest)];
    }

    // add npc name or null to the dialog list
    private void SpeakTo(string nameOfNpc)
    {
        _gs.Dialog.Add(nameOfNpc);
        if (nameOfNpc == null) _gs.Text.text = string.Empty; // clear text if no npc
        else Debug.Log($"Speaking to {nameOfNpc}");
    }

    private void DoNotSpeak()
    {
        SpeakTo(null);
    }
    private void PickUp(string packageName)
    {
        DoNotSpeak();
        _gs.Inventory.Add(packageName);
        Debug.Log($"Added {packageName} to inventory");
    }

    // clearing means replacing the snow tile with either a package, obstacle or nothing
    private void ClearSnow(Vector2 snowPosition)
    {
        DoNotSpeak();
    }
}