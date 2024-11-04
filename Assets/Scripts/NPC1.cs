using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
        public IEnumerator<string> myDialog = Dialog.NPC1();

        // choose what dialog to respond with using data about the games state
        private void handleDialog(GameState gs)
        {
                if (gs.Dialog[0] != name) return;

                AdvanceDialog(myDialog);
        }

        public void AdvanceDialog(IEnumerator<string> d) {
                d.MoveNext();
                // show d.Current in the text box
                Debug.Log(d.Current);
        }

        public void Awake() { Player.state += handleDialog; }
        public void OnDestroy() { Player.state -= handleDialog; }
}
