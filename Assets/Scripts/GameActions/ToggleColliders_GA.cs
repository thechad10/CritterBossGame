using UnityEngine;
using System.Collections.Generic;

public class ToggleColliders_GA : GameAction
{
    [SerializeField, Tooltip("Scripts/MonoBehaviours(s) to toggle")]
    private List<Collider2D> colliders;

    [SerializeField, Tooltip("If True: sets to active. If False: sets to inactive.")]
    private bool setActive;

    public override void Action()
    {
        foreach (Collider2D c in colliders)
        {
            c.enabled = setActive;
        }
    }

    public override void DeAction()
    {
        foreach (Collider2D c in colliders)
        {
            c.enabled = !setActive;
        }
    }
}
