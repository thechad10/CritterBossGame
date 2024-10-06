using UnityEngine;
using System.Collections.Generic;
public class ToggleSpriteRenderer_GA : GameAction
{
    [SerializeField, Tooltip("SpriteRenderer(s) to toggle")]
    private List<SpriteRenderer> spriteRenderers;

    [SerializeField, Tooltip("If True: sets to active. If False: sets to inactive.")]
    private bool setActive;

    public override void Action()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = setActive;
        }
    }

    public override void DeAction()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = !setActive;
        }
    }
}
