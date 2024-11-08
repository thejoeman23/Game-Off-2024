using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected IEnumerator<string> MyDialog; // the dialog currently being spoken

    // method implemented in child that chooses what dialog to say by setting MyDialog
    protected abstract void GetDialog(GameState gs);

    private void HandleDialog(GameState gs)
    {
        // end and clear dialog if someone else or nobody is speaking
        if (gs.Dialog.Last() != name)
        {
            MyDialog = null;
            return;
        }

        // player looks at character
        Vector3 myPosition = transform.position;
        myPosition.y = gs.Player.transform.position.y; // player does not look up or down
        gs.Player.transform.LookAt(myPosition);

        GetDialog(gs); // implemented in child

        var dialogIsOver = Dialog.Say(MyDialog, gs.DialogBox);
        if (dialogIsOver) MyDialog = null;
    }

    protected virtual void Awake()
    {
        Player.state += HandleDialog;
    }

    protected virtual void OnDestroy()
    {
        Player.state -= HandleDialog;
    }
}