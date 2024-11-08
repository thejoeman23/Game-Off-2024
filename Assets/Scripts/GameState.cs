using System.Collections.Generic;
using TMPro;

public struct GameState
{
    public GameState(Player player, TMP_Text dialogBox)
    {
        Player = player;
        DialogBox = dialogBox;
        Dialog = new List<string>();
        Quests = new List<string>();
        Objects = new List<string>();
    }

    public TMP_Text DialogBox { get; set; }
    public Player Player { get; set; }
    public List<string> Dialog { get; set; }
    public List<string> Quests { get; set; }
    public List<string> Objects { get; set; }
}