using System.Collections.Generic;
using TMPro;

public struct GameState
{
    public GameState(Player player, TMP_Text dialogBox)
    {
        Player = player;
        DialogBox = dialogBox;
        Dialog = new List<string>();
        Inventory = new List<string>();
    }

    public TMP_Text DialogBox { get; set; }
    public Player Player { get; set; }

    // a record of the dialog being said
    public List<string> Dialog { get; set; }

    // packages that the player has picked up
    public List<string> Inventory { get; set; }
}