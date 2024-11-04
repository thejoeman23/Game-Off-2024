using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
        public IEnumerator<string> defaultDialog = Dialog.DefaultNPC();

        // choose what dialog to respond with using data about the games state
        private void handleDialog(GameState gs)
        {
                if (gs.Dialog[0] != name) return;

                // conditions...
                // if (you finished the quest I gave you)
                // say "awesome thanks"

                // else
                AdvanceDialog(defaultDialog);
        }

        public void AdvanceDialog(IEnumerator<string> d) {
                d.MoveNext(); 
                // show d.Current in the text box
                Debug.Log(d.Current);
        }

        public void Awake() { Player.state += handleDialog; }
        public void OnDestroy() { Player.state -= handleDialog; }
}