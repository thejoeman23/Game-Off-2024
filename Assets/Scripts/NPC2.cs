using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
        private IEnumerator<string> myDialog = Dialog.NPC2();

        // choose what dialog to respond with using data about the games state
        private void handleDialog(GameState gs)
        {
                if (gs.Dialog.Last() != name) return;

                Dialog.Say(myDialog);
        }

        public void Awake() { Player.state += handleDialog; }
        public void OnDestroy() { Player.state -= handleDialog; }
}
