using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DebugGA : GameAction
{
    [SerializeField]
    private string message;
    public override void Action()
    {
        Debug.Log(message);
    }
}