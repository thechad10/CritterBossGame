using UnityEngine;

public class PlayAnim_GA : GameAction
{
    [SerializeField, Tooltip("The animator this animation will play on")]
    private Animator animator;
    [SerializeField, Tooltip("String name of the animation to be played")]
    private string animName;
    public override void Action()
    {
        animator.Play(animName);
    }
}