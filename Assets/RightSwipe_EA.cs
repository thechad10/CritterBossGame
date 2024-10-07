using System.Collections;
using UnityEngine;

public class RightSwipe_EA : EnemyAttack
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        StartCoroutine(nameof(FinishAnimsAttack));
    }

    private IEnumerator FinishAnimsAttack()
    {
        animator.Play("BearSwipeRight");

        float animationDuration = GetAnimationDuration("BearSwipeRight");

        yield return new WaitForSeconds(animationDuration);
        animator.Play("Bear_Idle");

        GetComponent<Boss>().FinishAttack();
    }

    private float GetAnimationDuration(string animationName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        Debug.LogError($"Animation {animationName} not found!");
        return 0f;
    }
}