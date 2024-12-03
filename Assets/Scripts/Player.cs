using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int snowClearDistance = 3;
    [SerializeField] private GameObject snowClearEffect;
    [SerializeField] private GameObject whiteBackground;
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private float transitionTime;
    public static float ActionDistance = 3f;

    public static HashSet<string> deliveredPackages; // incremented in Dialog.cs
    private static TMP_Text _progressText;
    private static GameObject _interactBubble;
    private static Vector3 _startPos;

    public delegate void Subject(GameState gs);

    public static Subject state;
    private static GameState gs;

    private void Start()
    {
        deliveredPackages = new HashSet<string>();
        _progressText = GameObject.Find("ProgressText").GetComponent<TMP_Text>();
        _startPos = transform.position;

        var db = GameObject.Find("DialogueBackground");
        gs = new GameState(
            this,
            db,
            db.GetComponentInChildren<TMP_Text>()
        );

        InvokeRepeating(nameof(bubbleHandler), 0, 0.25f);
    }

    private void addAllPackages()
    {
        gs.Inventory.Add("TowerPackage");
        gs.Inventory.Add("RichPackage");
        gs.Inventory.Add("ElevatedPackage");
        gs.Inventory.Add("ShopPackage");
        gs.Inventory.Add("PackageA");
        gs.Inventory.Add("PuzzlePackage1");
        gs.Inventory.Add("PuzzlePackage2");
        gs.Inventory.Add("PuzzlePackage3");
        gs.Inventory.Add("PuzzlePackage4");
        gs.Inventory.Add("PuzzlePackage5");
    }

    private void bubbleHandler()
    {
        gs.TryShowingBubble();
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            Debug.Log(transform.position);
            transform.position = _startPos;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeAction();
            state?.Invoke(gs); // all actions send game state to the characters
            _progressText.text = $"{deliveredPackages.Count()}/10";

            if (deliveredPackages.Count() == 10)
            {
                Debug.Log("Successfully delivered all packages!");
                whiteBackground.GetComponent<Transform>().DOScale(new Vector3(200, 200, 200), 20).Play();
                endGameScreen.transform.DOLocalMove(Vector3.zero, transitionTime).Play();
            }
        }
    }

    // Call the correct method with the correct parameters to execute an action if possible
    private void TakeAction()
    {
        GameObject package = NearestWithTag("Package");
        if (package && Vector3.Distance(package.transform.position, transform.position) < ActionDistance)
        {
            PickUp(package.name);
            package.SetActive(false); // hide the package when it is picked up
            return;
        }

        GameObject npc = NearestWithTag("NPC");
        var children = npc.GetComponentsInChildren<Transform>();
        var doorPos = children.First(x => x.gameObject.name == "Door").position;
        if (npc && Vector3.Distance(doorPos, transform.position) < ActionDistance)
        {
            SpeakTo(npc.name);
            return;
        }

        TryClearSnow();
    }

    public GameObject NearestWithTag(string tagName)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
        if (objs.Length == 0) return null;
        var distances = objs.Select(obj => // calculate distances from the player to the objects
            Vector3.Distance(obj.transform.position, transform.position)).ToArray();
        var nearest = distances.Aggregate((c, d) => c < d ? c : d); // find the index of the shortest distance
        return objs[Array.IndexOf(distances, nearest)];
    }

    // add npc name or null to the dialog list
    private void SpeakTo(string nameOfNpc)
    {
        gs.Dialog.Add(nameOfNpc);
        if (nameOfNpc == null) gs.Text.text = string.Empty; // clear text if no npc
        else Debug.Log($"Speaking to {nameOfNpc}");
    }

    private void DoNotSpeak()
    {
        SpeakTo(null);
    }

    private void PickUp(string packageName)
    {
        DoNotSpeak();
        gs.Inventory.Add(packageName);
        Debug.Log($"Added {packageName} to inventory");
    }

    // clearing means replacing the snow tile with either a package, obstacle or nothing
    private void TryClearSnow()
    {
        DoNotSpeak();

        // look for a game object in front of the player
        var origin = transform.position;
        origin.y -= 0.5f;
        var hit = Physics.Raycast(
            origin,
            transform.TransformDirection(Vector3.forward),
            out var info,
            snowClearDistance
        );
        if (!hit) return;
        var obj = info.collider.gameObject;

        // checking the tag doesnt work because the tag gets replaced with the "Rule Tile" tag
        if (!obj.name.Contains("DeepSnow")) return;

        Debug.Log($"Clearing snow at {obj.transform.position}");
        GameObject ParticleSystem = Instantiate(snowClearEffect);
        ParticleSystem.transform.position = obj.transform.position;

        Destroy(obj);
    }
}