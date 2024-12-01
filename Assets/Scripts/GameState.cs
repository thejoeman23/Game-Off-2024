using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public struct GameState
{
    public GameState(Player player, GameObject textbox, TMP_Text text)
    {
        Player = player;
        Text = text;
        Textbox = textbox;
        Bubble = GameObject.Find("InteractBackdrop");
        Dialog = new List<string>();
        Inventory = new System.Collections.Generic.HashSet<string>();
        Textbox.SetActive(false);
        Bubble.SetActive(false);
        Dialog.Add(null);

        Interactables = new List<Transform>();
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            var children = npc.GetComponentsInChildren<Transform>();
            var doorPos = children.First(x => x.gameObject.name == "Door").transform;
            Interactables.Add(doorPos);
        }

        GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");
        foreach (GameObject package in packages)
            Interactables.Add(package.transform);
    }


    public void SetTextboxActive(bool target)
    {
        if (target == Textbox.activeSelf) return;
        if (target)
        {
            Textbox.transform.localScale = Vector3.zero;
            Textbox.SetActive(true);
            Textbox.transform.DOScale(new Vector3(.45f, .45f, .45f), .1f).Play();
        }
        else
        {
            GameObject textbox = Textbox;

            Tween shrink = Textbox.transform.DOScale(Vector3.zero, .1f);
            shrink.Play();
            shrink.OnComplete(() => { textbox.SetActive(true); });
        }
    }

    public void UpdateInteractables()
    {
        foreach (Transform t in Interactables.ToList())
        {
            if (Inventory.Contains(t.name))
                Interactables.Remove(t);
        }
    }

    public void TryShowingBubble()
    {
        UpdateInteractables();
        foreach (Transform t in Interactables)
        {
            if (Vector3.Distance(Player.transform.position, t.position) <= Player.ActionDistance)
            {
                if (!Bubble.activeSelf)
                {
                    Bubble.transform.localScale = Vector3.zero;
                    Bubble.SetActive(true);
                    Bubble.transform.DOScale(new Vector3(.45f, .45f, .45f), .1f).Play();
                }
                return;
            }
        }

        shrinkBubble();
    }

    void shrinkBubble()
    {
        GameObject interactBubble = Bubble;

        Tween shrink = interactBubble.transform.DOScale(Vector3.zero, .1f);
        shrink.Play();
        shrink.OnComplete(() => { interactBubble.SetActive(false); });
    }

    public GameObject Bubble { get; set; }
    public List<Transform> Interactables { get; set; }
    private GameObject Textbox { get; set; }
    public TMP_Text Text { get; set; }
    public Player Player { get; set; }
    public List<string> Dialog { get; set; }
    public HashSet<string> Inventory { get; set; }
}