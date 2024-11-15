using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct GameState
{
    public GameState(Player player, GameObject textbox, TMP_Text text)
    {
        Player = player;
        Text = text;
        Textbox = textbox;
        Dialog = new List<string>();
        Inventory = new List<string>();

        Textbox.SetActive(false);
        Dialog.Add(null);
    }

    public void TextboxActive(bool target)
    {
        if (target != Textbox.activeSelf)
            Textbox.SetActive(target);
    }

    private GameObject Textbox { get; set; }
    public TMP_Text Text { get; set; }
    public Player Player { get; set; }

    // a record of the dialog being said
    public List<string> Dialog { get; set; }

    // packages that the player has picked up
    public List<string> Inventory { get; set; }
}