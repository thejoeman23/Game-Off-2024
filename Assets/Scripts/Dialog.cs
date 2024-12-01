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
        yield return "Hey, do you have my package?";

        if (HasPackages())
        {
            if (InventoryContains("PackageA"))
            {
                yield return "<wave>Hooray! My package!";
                yield return "<funky>Thank you weird snail!";
                Player.deliveredPackages.Add("PackageA");
                yield return null;
            }
        }
        else yield return "<!delay=.5>...No?<!delay=.035><!wait=.5>Arent you supposed to be finding our packages?";

        yield return "Well, you better find it! <!wait=1> I'm counting on <shake>you";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse1()
    {
        yield return "Where is my package, <!wait=.5><shake>snaily boy!?";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage1"))
            {
                yield return "What do you mean, <!wait=.25><wave>'is this your package?!'</wave><!wait=.25> Of course it is!";
                yield return "<shake>Gimme that!";
                Player.deliveredPackages.Add("PuzzlePackage1");
                yield return null;
            }
        }
        else yield return "<shake>You dont have it?!";

        yield return "It will expire soon! <!wait=1>What are you waiting for? <!wait=.25><wave>GOOOOOO!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse2()
    {
        yield return "You're supposed to bring me my package! <!wait=.25>Where is it?";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage2"))
            {
                yield return "Oh! <!wait=.25> Thanks so much!";
                yield return "Good luck on your journey!";
                Player.deliveredPackages.Add("PuzzlePackage2");
                yield return null;
            }
        }
        else yield return "You dont know? <!wait=.25>Well its your job to find them!";

        yield return "The packages got lost in the <shake>blizzard</shake>, <!wait=.25>so theyre scattered <palette>across the island.";
        yield return "Each package has the same <palette>color</palette> as the house its from. <wave>Mine is purple.";
        yield return "Go find it for me, <!wait=.25>will you?";
        yield return "Oh, <!wait=.25> one last thing.";
        yield return "If any snow gets in your way you can <wave>clear it</wave> with your plow.";
        yield return "You can do so by pressing <palette>SPACE</palette> while facing a pesky block of snow.";
        yield return "Thats all <palette>:)";
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
        yield return "<palette>Yo yo snail bro! <!wait=.25>Where is my package <grow>dudeeee?";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage4"))
            {
                yield return "About time <!delay=.25>...<!wait=.5><!delay=.1><wave>DUDE!";
                Player.deliveredPackages.Add("PuzzlePackage4");
                yield return null;
            }
        }
        else yield return "You dont have it? <!wait=.25><wave>Aw shucks man :(";

        yield return "Go get me that package <grow>dude!</grow> <!wait=.25><shake>ASAP!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse5()
    {
        yield return "<grow>Package please!";

        if (HasPackages())
        {
            if (InventoryContains("PuzzlePackage5"))
            {
                yield return "What! <!delay=.25>...<!wait=.5><!delay=.1> It was under <!wait=.25>a tree?";
                yield return "How odd<!delay=.25>...";
                Player.deliveredPackages.Add("PuzzlePackage5");
                yield return null;
            }
        }
        else yield return "Yeah thats right! <!wait=.5> I <shake>AM</shake> the only house without a texture!";

        yield return "Yeah thats right! <!wait=.5> I <shake>AM</shake> the only house without a texture!";
        yield return "<wave>What of it?";
        yield return null;
    }

    public static IEnumerator<string> ElevatedHouse()
    {
        yield return "Mon package, <wave>miseur?";

        if (HasPackages())
        {
            if (InventoryContains("ElevatedPackage"))
            {
                yield return "Ah oui oui, <palette>my baguette in a box!";
                yield return "<wave>Finally!";
                Player.deliveredPackages.Add("ElevatedPackage");
                yield return null;
            }
        }
        else yield return "<wave>Oui</wave>, <!wait=.3> I did build ma maison above the ground!";

        yield return "Those darn<!delay=.1> squirrels.<!delay=.035> Always eating my <wave>familia!";
        yield return null;
    }

    public static IEnumerator<string> RichHouse()
    {
        yield return "I PAYED EXTRA TO GET MY PACKAGE ON TIME! WHhERE IS IT!";

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