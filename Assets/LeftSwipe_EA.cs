using System.Collections;
using UnityEngine;

public class LeftSwipe_EA : EnemyAttack
{
    private Animator animator;
    private Bear bear;


    private void Start()
    {
        animator = GetComponent<Animator>();
        bear = GetComponent<Bear>();

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
        if (!bear.IsDead)
            animator.Play("Bear_Swipe_Left");

        FindObjectOfType<SoundControl>().Play("BearSlash");

        float animationDuration = GetAnimationDuration("Bear_Swipe_Left");

        yield return new WaitForSeconds(animationDuration);

        if (!bear.IsDead)
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