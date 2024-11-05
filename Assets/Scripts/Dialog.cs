using System.Collections.Generic;
using UnityEngine;

// Script that stores all of the dialog
// instance when needed and advance through text with .MoveNext() and .Current
public static class Dialog
{
    public static void Say(IEnumerator<string> d)
    {
        d.MoveNext();
        // show d.Current in the text box
        Debug.Log(d.Current);
    }

    public static IEnumerator<string> DefaultNPC()
    {
        yield return "Hello";
        yield return "Goodbye";
    }

    public static IEnumerator<string> NPC1()
    {
        yield return "Hello, I am NPC 1";
        yield return "How are you?";
        yield return "Sounds good";
        yield return "Nevermind";
    }

    public static IEnumerator<string> NPC2()
    {
        yield return "Hello, I am NPC 2";
        yield return "How are you?";
        yield return "Sounds good";
        yield return "Nevermind";
    }
}