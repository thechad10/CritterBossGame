using UnityEngine;

public class ChangeTimeScale_GA : GameAction
{
    [SerializeField, Tooltip("First Time Scale")]
    private float timeScale1 = 0;

    [SerializeField, Tooltip("Second Time Scale")]
    private float timeScale2 = 0;

    [SerializeField, Tooltip("If True: Timescale = Timescale1. If False: Timescale = Timescale2.")]
    private bool setActive;

    public override void Action()
    {
        Time.timeScale = timeScale1;
    }

    public override void DeAction()
    {
        Time.timeScale = timeScale2;
    }
}
