using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

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

        text.text = "<+spread>" + next;
        return false;
    }

    // return the next line of dialog in the given script
    private static string Advance(IEnumerator<string> script)
    {
        script.MoveNext();
        return script.Current;
    }


    private static bool InventoryContains(string packageName)
    {
        return Array.IndexOf(_inventory, packageName) != -1;
    }


    private static bool HasPackages()
    {
        return _inventory.Length != 0;
    }

    // call the dialog script with the same name as the character
    public static IEnumerator<string> Get(string characterName, GameState gs)
    {
        _inventory = gs.Inventory.ToArray();
        return (IEnumerator<string>)Type.GetType("Dialog")
            ?.GetMethod(characterName)?.Invoke(null, null);
    }

    // dialog scripts are below this line

    public static IEnumerator<string> HouseA()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("PackageA"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("PackageA");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse1()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage1"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("PuzzlePackage1");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse2()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage2"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("PuzzlePackage2");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse3()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage3"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("PuzzlePackage3");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse4()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage4"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("PuzzlePackage4");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse5()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage5"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("PuzzlePackage5");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> ElevatedHouse()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("ElevatedPackage"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("ElevatedPackage");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> RichHouse()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("RichPackage"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("RichPackage");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> ShopHouse()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("ShopPackage"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("ShopPackage");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }

    public static IEnumerator<string> Tower()
    {
        yield return "Give me my package!";

        if (HasPackages())
        {
            if (InventoryContains("TowerPackage"))
            {
                yield return "Is this your package?";
                yield return "Its about time!";
                Player.deliveredPackages.Add("ShopPackage");
                yield return null;
            }
        }
        else yield return "I dont have it...";

        yield return "It will expire soon! Go!";
        yield return null;
    }
}