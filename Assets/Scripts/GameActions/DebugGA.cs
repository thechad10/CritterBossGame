using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DebugGA : GameAction
{
    public override void Action()
    {
        Debug.Log("GameActionTriggered");
    }
}