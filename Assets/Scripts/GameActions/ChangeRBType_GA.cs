using UnityEngine;

public class ChangeRBType_GA : GameAction
{
    [SerializeField, Tooltip("Whether or not the RigidBody will be kinematic")]
    private RigidbodyType2D bodyType;
    [SerializeField, Tooltip("Rigidbody2D to have its type changed")]
    private Rigidbody2D rb;
    public override void Action()
    {
        rb.bodyType = bodyType;
    }

}
