using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    private IEnumerator<string> myDialog;

    private void handleDialog(GameState gs)
    {
        // end and clear dialog if someone else or nobody is speaking
        if (gs.Dialog.Last() != name)
        {
            myDialog = null;
            return;
        }

        myDialog ??= Dialog.NPC1(); // get new dialog if current dialog is empty

        bool dialogIsOver = Dialog.Say(myDialog, gs.DialogBox);
        if (dialogIsOver) myDialog = null;
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