using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Dialog
{
    private static Array _inventory = Array.Empty<string>();

    // advance dialog and put text into dialogBox, return true if dialog is over
    public static bool Say(IEnumerator<string> script, TMP_Text text)
    {
        if (script == null) throw new ArgumentNullException(nameof(script));

        var next = Advance(script);

        // show next line of dialog or end conversation
        if (next == null)
        {
            text.text = string.Empty;
            return true;
        }

        text.text = next;
        return false;
    }

    // return the next line of dialog in the given script
    private static string Advance(IEnumerator<string> script)
    {
        script.MoveNext();
        return script.Current;
    }

    // call the method with the same name as the character
    public static IEnumerator<string> Get(string characterName, GameState gs)
    {
        _inventory = gs.Inventory.ToArray();
        return (IEnumerator<string>)Type.GetType("Dialog")
            ?.GetMethod(characterName)?.Invoke(null, null);
    }

    private static bool InventoryContains(string packageName)
    {
        return Array.IndexOf(_inventory, packageName) != -1;
    }


    private static bool HasPackages()
    {
        return _inventory.Length != 0;
    }

    public static IEnumerator<string> HouseA()
    {
        yield return "Do you have my package?";

        if (HasPackages())
        {
            if (InventoryContains("PackageA"))
            {
                yield return "Is this your package?";
                yield return "It is! Thank you so much! My night is saved!";
                Player.score++;
                yield return null;
            }
        }
        else yield return "no...";

        yield return "You better find it! I want to use my new ice skates tonight!";
        yield return null;
    }

    public static IEnumerator<string> HouseB()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("PackageB"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.score++;
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }
}