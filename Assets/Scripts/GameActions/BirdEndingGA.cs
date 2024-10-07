using UnityEngine;

public class BirdEndingGA : GameAction
{
    public override void Action()
    {
        FindFirstObjectByType<PlatformMovementHandler>().isMoving = false;
    }

}
