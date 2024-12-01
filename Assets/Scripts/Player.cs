using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int snowClearDistance = 3;
    [SerializeField] private GameObject snowClearEffect;
    public static float ActionDistance = 3f;

    public static HashSet<string> deliveredPackages; // incremented in Dialog.cs
    private static TMP_Text _progressText;
    private static Transform _hoveringTransform;
    private static GameObject _interactBubble;

    public delegate void Subject(GameState gs);

    public static Subject state;
    private static GameState gs;

    private void Start()
    {
        deliveredPackages = new HashSet<string>();
        _progressText = GameObject.Find("ProgressText").GetComponent<TMP_Text>();
        _hoveringTransform = GameObject.Find("HoveringBody").transform;

        var db = GameObject.Find("DialogueBackground");
        gs = new GameState(
            this,
            db,
            db.GetComponentInChildren<TMP_Text>()
        );

        InvokeRepeating(nameof(bubbleHandler), 0, 0.25f);
    }

    private void bubbleHandler()
    {
        gs.TryShowingBubble();
    }

    private void Update()
    {
        gs.UpdateInteractables();
        hoverEffect();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeAction();
            state?.Invoke(gs); // all actions send game state to the characters
            _progressText.text = $"{deliveredPackages.Count()}/10";
        }
    }

    private void hoverEffect()
    {
        var pos = _hoveringTransform.position;
        pos.y += Mathf.Sin(Time.timeSinceLevelLoad) * 0.0025f;
        _hoveringTransform.position = pos;
    }

    // Call the correct method with the correct parameters to execute an action if possible
    private void TakeAction()
    {
        GameObject package = NearestWithTag("Package");
        if (package && Vector3.Distance(package.transform.position, transform.position) < ActionDistance)
        {
            // if (not inside DeepSnow)
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