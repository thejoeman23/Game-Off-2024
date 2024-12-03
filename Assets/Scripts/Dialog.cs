using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UIElements;

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
        string packageName = "PackageA";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "<funky>Thank you weird snail!"; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "<wave>Hooray! My package!";
                yield return "<funky>Thank you weird snail!";
                Player.deliveredPackages.Add("PackageA");
                yield return null;
            }
        }
        else yield return "Hey, do you have my package?";

        yield return "<!delay=.5>...No?<!delay=.035><!wait=.5>Arent you supposed to be finding our packages?";
        yield return "Well, you better find it! <!wait=1> I'm counting on <shake>you";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse1()
    {
        string packageName = "PuzzlePackage1";
        if(Player.deliveredPackages.Contains(packageName)) { yield return "<shake>Get outa here!"; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "What do you mean, <!wait=.25><wave>'is this your package?!'</wave><!wait=.25> Of course it is!";
                yield return "<shake>Gimme that!";
                Player.deliveredPackages.Add("PuzzlePackage1");
                
                yield return null;
            }
        } 
        else yield return "Where is my package, <!wait=.5><shake>snaily boy!?"; 
        yield return "<shake>You dont have it?!";
        yield return "It will expire soon! <!wait=1>What are you waiting for? <!wait=.25><wave>GOOOOOO!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse2()
    {
        string packageName = "PuzzlePackage2";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "You've already delivered here, <!wait=.25><palette>snail. <!wait=.25><wave>Bye bye now."; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "Oh! <!wait=.25> Thanks so much!";
                yield return "Good luck on your journey!";
                yield return "I recommend you talk to everyone <i>before</i> you deliver the packages.";
                yield return "We have quite some <palette>charaters</palette> in this town :)";
                Player.deliveredPackages.Add("PuzzlePackage2");
                yield return null;
            }
        }
        else yield return "You're supposed to bring me my package! <!wait=.25>Where is it?"; 

        yield return "You dont know? <!wait=.25>Well its your job to find them!";
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
        string packageName = "PuzzlePackage2";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "<wave>Slug along now."; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "Why thank you, <!wait=.3><wave><palette>slimy lad!";
                yield return "<wave>Slug along now.";
                Player.deliveredPackages.Add("PuzzlePackage3");
                yield return null;
            }
        }
        else yield return "I would like my package please, <!wait=.3><wave><palette>good sir"; 
        yield return "You want to know the <wave>lore</wave> of the town?<!wait=.5> Why <wave>of course</wave> you do!";
        yield return "You must find it <shake>strange</shake> that all of the houses here are <palette>puzzle shaped!";
        yield return "Long story short, <!wait=.3>the <wave>Great Blueprint Mix-Up of '89.";
        yield return "The <wave><b>town contractor</b></wave> accidentally <b>swapped</b> our housing plans with a <wave>jigsaw puzzle factory.";
        yield return "Instead of fixing it, everyone just <palette>rolled</palette> with it. <!wait=.3> <wave>'Adds charm!'</wave> <!wait=.1> they said.";
        yield return "Now we’re <palette>Puzzleton</palette>, <!wait=.3>the town where everything fits<!delay=.25>… <!delay=.035><!wait=.3><wave>eventually.";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse4()
    {
        string packageName = "PuzzlePackage4";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "Get outa here, <!wait=.25><wave>dude."; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "About time <!delay=.25>...<!wait=.5><!delay=.1><wave>DUDE!";
                Player.deliveredPackages.Add("PuzzlePackage4");
                yield return null;
            }
        }
        else yield return "<palette>Yo yo snail bro! <!wait=.25>Where is my package <grow>dudeeee?"; 
        yield return "You dont have it? <!wait=.25><wave>Aw shucks man :(";
        yield return "Go get me that package <grow>dude!</grow> <!wait=.25><shake>ASAP!";
        yield return null;
    }

    public static IEnumerator<string> PuzzleHouse5()
    {
        string packageName = "PuzzlePackage5";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "Nope, <!wait=.25>you've already been here."; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "What! <!delay=.25>...<!wait=.5><!delay=.1> It was under <!wait=.25>a tree?";
                yield return "How odd<!delay=.25>...";
                Player.deliveredPackages.Add("PuzzlePackage5");
                yield return null;
            }
        }
        else yield return "<grow>Package please!";
        yield return "Yeah thats right! <!wait=.5>I <shake>AM</shake> the only house without a texture!";
        yield return "<wave>What of it?";
        yield return null;
    }

    public static IEnumerator<string> ElevatedHouse()
    {
        string packageName = "ElevatedPackage";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "The squerrels arent going to give my <wave>familia</wave> back, <!wait=.25>are they :("; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "Ah oui oui, <palette>my baguette in a box!";
                yield return "<wave>Finally!";
                Player.deliveredPackages.Add("ElevatedPackage");
                yield return null;
            }
        }
        else yield return "Mon package, <wave>monsieur?"; 
        yield return "<wave>Oui</wave>, <!wait=.3> I did build ma maison above the ground!";
        yield return "Those darn<!delay=.1> squirrels.<!delay=.035> Always eating my <wave>familia!";
        yield return null;
    }

    public static IEnumerator<string> RichHouse()
    {
        string packageName = "RichPackage";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "<wave>GO AWAY</wave>, <!wait=.25>CANT YOU SEE IM <shake>BUSY?"; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "ITS ABOUT TIME!";
                yield return "<wave>I DONT CARE IF YOURE A</wave> <shake>SNAIL</shake>, <wave>YOULL BE HEARING FROM MY </wave><shake>LAWYERS!";
                Player.deliveredPackages.Add("RichPackage");
                yield return null;
            }
        }
        else yield return "<wave>I PAYED EXTRA TO GET MY PACKAGE ON TIME! <!wait=.25>WHERE IS IT!"; 
        yield return "<wave>YOU HAD BETTER FIND IT,</wave><!wait=.5><shake> SLUG!";
        yield return "<wave>I'll SUE YOU!";
        yield return null;
    }

    public static IEnumerator<string> ShopHouse()
    {
        string packageName = "ShopPackage";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "<wave>GET DOWN ITS THE COPS!"; yield return "Oh, <!wait=.25> its just <wave><palette>the snail</palette></wave>, guys."; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "Finally! <!wait=.25>Now they'll let me go!";
                yield return "<shake>'WE'LL NEVER LET YOU GO'";
                yield return "<wave>HEELLLLPPPP";
                Player.deliveredPackages.Add("ShopPackage");
                yield return null;
            }
        }
        else yield return "I need my package, its for my customers!"; 
        yield return "Please find it, <!wait=.25>they demand perfection.";
        yield return "<wave>THEYRE HOLDING ME HOSTA</wave><!wait=.75>---";
        yield return "Give us the package, <!wait=.75> or <shake><b>he</b></shake> gets <shake>it!";
        yield return null;
    }

    public static IEnumerator<string> Tower()
    {
        string packageName = "TowerPackage";
        if (Player.deliveredPackages.Contains(packageName)) { yield return "i'd like to be <wave>alone</wave> now that i have my <b>package</b>"; yield return null; }

        if (HasPackages())
        {
            if (InventoryContains(packageName))
            {
                yield return "<wave>Still nobodyyy";
                yield return "<!wait=.75> ok ill take it.";
                Player.deliveredPackages.Add("TowerPackage");
                yield return null;
            }
        } else yield return "Sorry nobody's home.";
        yield return null;
    }
}