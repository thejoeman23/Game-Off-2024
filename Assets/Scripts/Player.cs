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
        if (Input.GetKeyDown(KeyCode.Space)) SpeakTo("npc1");
    }
}
