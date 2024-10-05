using UnityEngine;

public class Boomerang_EA : EnemyAttack
{
    [SerializeField, Tooltip("Speed this attack goes")]
    private float attackSpeed;
    [SerializeField, Tooltip("Used for the x-position")]
    private AnimationCurve xPosition;
    [SerializeField, Tooltip("Used for the x AnimationCurve's calculation")]
    private Transform xMaxPos;
    [SerializeField, Tooltip("Used for the y-position")]
    private AnimationCurve yPosition;
    [SerializeField, Tooltip("Used for the y AnimationCurve's calculation")]
    private Transform yMaxPos;
    private bool isAttacking;
    private float rate;
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
        hitbox.transform.localPosition = 
            new Vector3(xMaxPos.position.x * xPosition.Evaluate(rate), yMaxPos.position.y * yPosition.Evaluate(rate), 0);

        if(rate >= 1)
        {
            rate = 0;
            isAttacking = false;
        }
    }
}
