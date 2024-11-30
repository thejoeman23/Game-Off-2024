using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractBubble : MonoBehaviour
{
    // positions of all game objects that can interact
    public List<Vector3> interactables;
    private GameObject _player;
    private GameObject _interactBubble;

    void Start()
    {
        interactables = new List<Vector3>();
        _player = GameObject.Find("Player");
        _interactBubble = GameObject.Find("InteractBackdrop");

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

    void TryShowingBubble()
    {
        foreach (Vector3 v in interactables)
        {
            if (Vector3.Distance(_player.transform.position, v) <= Player.ActionDistance)
            {
                _interactBubble.SetActive(true);
                return;
            }
        }

        if (_interactBubble.activeInHierarchy) _interactBubble.SetActive(false);
    }
}