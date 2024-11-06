using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    private IEnumerator<string> myDialog;

    private void handleDialog(GameState gs)
    {
        if (gs.Dialog.Last() != name) return; // check if I am being spoken to
        myDialog ??= Dialog.NPC1(); // get new dialog if current dialog is empty
        Dialog.Say(myDialog, gs.DialogBox);
    }

    public void Awake()
    {
        Player.state += handleDialog;
    }

    public void OnDestroy()
    {
        Player.state -= handleDialog;
    }
}