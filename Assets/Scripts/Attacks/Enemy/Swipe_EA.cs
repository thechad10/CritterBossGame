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
    private bool isAttacking;
    private float rate;
    [SerializeField]
    private Transform tPivot;
    public override void Attack()
    {
        if (isAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        hitbox.gameObject.SetActive(true);
        isAttacking = true;
    }
    private void Update()
    {
        if (!isAttacking) return;
        rate += Time.deltaTime * attackSpeed;
        Debug.Log(xMaxPos.localPosition.x);
        tPivot.localScale = new Vector3(xMaxPos.localPosition.x * xScale.Evaluate(rate), (yMaxPos.localPosition.y - yMinPos.localPosition.y)* yScale.Evaluate(rate), 0);
        //hitbox.transform.localScale = new Vector3(xMaxPos.position.x * xScale.Evaluate(rate), yMaxPos.localPosition.y * yScale.Evaluate(rate), 0);

        if (rate >= 1)
        {
            rate = 0;
            isAttacking = false;
        }
    }
}
