using UnityEngine;

public class DestroyGA : GameAction
{
    public override void Action()
    {
        Destroy(gameObject);
    }
}