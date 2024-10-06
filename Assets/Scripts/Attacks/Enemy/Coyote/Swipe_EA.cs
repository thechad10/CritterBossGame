using System.Collections;
using UnityEngine;

public class Swipe_EA : EnemyAttack
{
    [SerializeField, Tooltip("Speed this attack goes")]
    private float attackSpeed;
    [SerializeField, Tooltip("Used for the x-scale")]
    private AnimationCurve xScale;
    [SerializeField, Tooltip("Used for the x AnimationCurve's calculation")]
    private Transform xMaxPos;
    [SerializeField, Tooltip("Used for the y-scale")]
    private AnimationCurve yScale;
    [SerializeField, Tooltip("Used for the y AnimationCurve's calculation")]
    private Transform yMaxPos;
    [SerializeField, Tooltip("Used for the y AnimationCurve's calculation")]
    private Transform yMinPos;
    private float rate;
    [SerializeField]
    private Transform tPivot;
    [SerializeField] private float telegraphTime = 0.5f;

    private Animator coyoteAnimator;

    private void Start()
    {
        coyoteAnimator = GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        StartCoroutine(nameof(Telegraph));
    }
    private void Update()
    {
        if (!IsAttacking) return;
        rate += Time.deltaTime * attackSpeed;
        tPivot.localScale = new Vector3(xMaxPos.localPosition.x * xScale.Evaluate(rate), (yMaxPos.localPosition.y - yMinPos.localPosition.y)* yScale.Evaluate(rate), 0);
        
        if (rate >= 1)
        {
            rate = 0;
            IsAttacking = false;
            hitbox.enabled = false;
            coyoteAnimator.Play("Coyote_Idle");
            GetComponent<Boss>().FinishAttack();
        }
    }

    IEnumerator Telegraph()
    {
        yield return new WaitForSeconds(telegraphTime);
        hitbox.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        hitbox.enabled = true;
        IsAttacking = true;
    }
}
