using UnityEngine;
using System.Collections.Generic;

public class ToggleScripts_GA : GameAction
{
    [SerializeField, Tooltip("Scripts/MonoBehaviours(s) to toggle")]
    private List<MonoBehaviour> scripts;

    [SerializeField, Tooltip("If True: sets to active. If False: sets to inactive.")]
    private bool setActive;

    public override void Action()
    {
        foreach (MonoBehaviour s in scripts)
        {
            s.enabled = setActive;
        }
    }

    public override void DeAction()
    {
        foreach (MonoBehaviour s in scripts)
        {
            s.enabled = !setActive;
        }
    }
}