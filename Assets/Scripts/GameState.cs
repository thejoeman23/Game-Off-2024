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
        Inventory = new System.Collections.Generic.HashSet<string>();
        Textbox.SetActive(false);
        Dialog.Add(null);
    }

    public void SetTextboxActive(bool target)
    {
        if (target != Textbox.activeSelf)
            Textbox.SetActive(target);
    }

    private GameObject Textbox { get; set; }
    public TMP_Text Text { get; set; }
    public Player Player { get; set; }
    public List<string> Dialog { get; set; }
    public HashSet<string> Inventory { get; set; }
}