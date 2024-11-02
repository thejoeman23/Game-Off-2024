using System.Collections.Generic;
using JetBrains.Annotations;
// stores information about what has happened in the world
public struct GameState
{
    public GameState(string myName)
    {
        PlayerName = myName;
        Dialog = new List<string>();
        Quests = new List<string>();
        Objects = new List<string>();
    }

    public string PlayerName { get; }
    public List<string> Dialog { get; set; }
    public List<string> Quests { get; set; }
    public List<string> Objects { get; set; }
}
