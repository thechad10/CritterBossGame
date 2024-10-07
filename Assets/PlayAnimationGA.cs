using UnityEngine;

public class PlayAnimationGA : GameAction
{
    [SerializeField]private Animator animator;

    public override void Action()
    {
        animator.Play("Bear_Dead");
    }
}