using UnityEngine;

public class makePlayerInverlnerable : GameAction
{
    public override void Action()
    {
        GameObject.FindWithTag("Player").layer = 16;
    }
}