using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
        public static string myName = "npc1";
        public IEnumerator<string> normal = normalDialog();
        public static IEnumerator<string> normalDialog()
        {
                yield return "hello I am " + myName;
                yield return "how are you?";
                yield return "sounds good";
                yield return "nevermind";
        }

        // choose what dialog to respond with using data about the games state
        private void handleDialog(GameState gs)
        {
                if (gs.Dialog[0] != myName) return;

                // conditions...
                // if (you finished the quest I gave you)
                // say "awesome thanks"

                // else
                normal.MoveNext(); Debug.Log(normal.Current);
        }
        public void Awake() { Player.state += handleDialog; }
        public void OnDestroy() { Player.state -= handleDialog; }
}