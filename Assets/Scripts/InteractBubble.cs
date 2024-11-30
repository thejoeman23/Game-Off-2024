using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class InteractBubble : MonoBehaviour
{
    // positions of all game objects that can interact
    public List<Vector3> interactables;
    private GameObject _player;
    private GameObject _interactBubble;
    private GameObject _dialogueBox;

    void Start()
    {
        interactables = new List<Vector3>();
        _player = GameObject.Find("Player");
        _interactBubble = GameObject.Find("InteractBackdrop");
        _dialogueBox = GameObject.Find("DialogueBackground");

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            var children = npc.GetComponentsInChildren<Transform>();
            var doorPos = children.First(x => x.gameObject.name == "Door").position;
            interactables.Add(doorPos);
        }

        GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");
        foreach (GameObject package in packages)
            interactables.Add(package.transform.position);

        InvokeRepeating("TryShowingBubble", 0, 0.25f);
    }

    [System.Obsolete]
    void TryShowingBubble()
    {
        //if (_dialogueBox.active) { shrinkBubble();  return; }

        foreach (Vector3 v in interactables)
        {
            if (Vector3.Distance(_player.transform.position, v) <= Player.ActionDistance && !_interactBubble.active)
            {
                _interactBubble.transform.localScale = Vector3.zero;
                _interactBubble.SetActive(true);
                _interactBubble.transform.DOScale(new Vector3(.45f, .45f, .45f), .1f).Play();
                return;
            } else if (Vector3.Distance(_player.transform.position, v) <= Player.ActionDistance && _interactBubble.active)
            {
                _interactBubble.SetActive(true);
                return;
            }
        }

        shrinkBubble();
    }

    void shrinkBubble()
    {
        GameObject interactBubble = _interactBubble;

        Tween shrink = _interactBubble.transform.DOScale(Vector3.zero, .1f);
        shrink.Play();
        shrink.OnComplete(() => { interactBubble.SetActive(false); });
        return;
    }
}