using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    private IEnumerator<string> _myDialog; // the set of dialog lines currently being spoken
    private static GameState _gs;

    private void HandleDialog(GameState gs)
    {
        // end and clear dialog if someone else or nobody is speaking
        if (gs.Dialog.Last() != name)
        {
            _myDialog = null;
            return;
        }

        _gs = gs;
        GetPlayersAttention();
        _myDialog ??= Dialog.Get(name, _gs); // get new dialog if MyDialog is null

        var dialogIsOver = Dialog.Say(_myDialog, _gs.DialogBox);
        if (dialogIsOver) _myDialog = null;
    }

    // make the player look at you, the player does not look up or down
    private void GetPlayersAttention()
    {
        Vector3 myPosition = transform.position;
        myPosition.y = _gs.Player.transform.position.y;
        _gs.Player.transform.LookAt(myPosition);
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