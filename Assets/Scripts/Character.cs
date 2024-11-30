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
        if (gs.Dialog.Last() == null) gs.SetTextboxActive(false);
        if (gs.Dialog.Last() != name)
        {
            _myDialog = null;
            return;
        }

        GetAttention(gs.Player);
        _myDialog ??= Dialog.Get(name, gs); // get new dialog if MyDialog is null
        var dialogIsOver = Dialog.Say(_myDialog, gs.Text);
        if (dialogIsOver)
        {
            gs.SetTextboxActive(false);
            _myDialog = null;
        } else gs.SetTextboxActive(true);
    }

    // make the player look at you, the player does not look up or down
    private void GetAttention(Player player)
    {
        Vector3 myPosition = transform.position;
        myPosition.y = player.transform.position.y;
        player.transform.LookAt(myPosition);
    }

    protected void Awake()
    {
        Player.state += HandleDialog;
    }

    protected void OnDestroy()
    {
        Player.state -= HandleDialog;
    }
}