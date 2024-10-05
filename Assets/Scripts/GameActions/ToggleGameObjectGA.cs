using UnityEngine;
using System.Collections.Generic;

public class ToggleGameObjectGA : GameAction
{
    [SerializeField, Tooltip("GameObject(s) to toggle")]
    private List<GameObject> gameObjects;

    [SerializeField, Tooltip("If True: sets to active. If False: sets to inactive.")]
    private bool setActive;

    public override void Action()
    {
        foreach(GameObject go in gameObjects)
        {
            go.SetActive(setActive);
        }
    }

    public override void DeAction()
    {
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(!setActive);
        }
    }
}