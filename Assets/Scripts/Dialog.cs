using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public static IEnumerator<string> DefaultNPC()
    {
        yield return "hello";
        yield return "how are you?";
        yield return "sounds good";
        yield return "nevermind";
    }
}
