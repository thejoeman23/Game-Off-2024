using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void Subject(GameState gs);
    public static Subject state;
    public GameState gs = new("myName");

    public void SpeakTo(string name)
    {
        gs.Dialog.Add(name);
        state?.Invoke(gs);
    }

    public void Update()
    {
        // speak to the nearest npc within 10 units from me when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject npc = NearestNPC();

            // if distance is not too large
            SpeakTo(npc.name);
        }
    }

    public GameObject NearestNPC()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        // Aggregate to find the smallest distance
        GameObject nearest = npcs[0];
        return nearest;
    }
}
