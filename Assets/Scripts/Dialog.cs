using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public static class Dialog
{
    public static void Say(IEnumerator<string> dialog, TMP_Text dialogBox)
    {
        dialogBox.text = Advance(dialog);
    }
    private static string Advance(IEnumerator<string> dialog)
    {
        dialog.MoveNext();
        return dialog.Current;
    }

    public static IEnumerator<string> NPC1()
    {
        yield return "Hello, I am NPC 1";
        yield return "How are you?";
        yield return "I am a friendly npc";
        yield return "Goodbye";
        // yield return null
    }

    public static IEnumerator<string> NPC2()
    {
        yield return "Hello, I am NPC 2";
        yield return "Go away!";
        yield return "Go away!!";
        yield return "Go away!!!";
        // yield return null
    }
}