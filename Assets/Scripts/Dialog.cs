using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public static class Dialog
{
    // advance dialog and put text into dialogBox, return true if dialog is over
    public static bool Say(IEnumerator<string> script, TMP_Text dialog)
    {
        var next = Advance(script);

        // show next line of dialog or end conversation
        if (next == null)
        {
            dialog.text = string.Empty;
            return true;
        }

        dialog.text = next;
        return false;
    }

    // return the next line of dialog in the given script
    private static string Advance(IEnumerator<string> script)
    {
        script.MoveNext();
        return script.Current;
    }

    public static IEnumerator<string> NiceDialog()
    {
        yield return "You: Hello";
        yield return "NPC1: Hi, I am friendly";
        yield return "You: Cool";
        yield return "NPC1: Goodbye";
        yield return null;
    }

    public static IEnumerator<string> AngryDialog()
    {
        yield return "You: Hello";
        yield return "NPC2: Go away!";
        yield return "NPC2: Go away!!";
        yield return "NPC2: Go away!!!";
        yield return null;
    }
}