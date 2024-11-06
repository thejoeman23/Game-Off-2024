using System.Collections.Generic;
using TMPro;

public struct GameState
{
    public GameState(string myName, TMP_Text dialogBox)
    {
        PlayerName = myName;
        DialogBox = dialogBox;
        Dialog = new List<string>();
        Quests = new List<string>();
        Objects = new List<string>();
    }

    public TMP_Text DialogBox { get; set; }
    public string PlayerName { get; set; }
    public List<string> Dialog { get; set; }
    public List<string> Quests { get; set; }
    public List<string> Objects { get; set; }
}