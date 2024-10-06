using UnityEngine;
using System.Collections;

public class Suck_EA : EnemyAttack
{
    [SerializeField, Tooltip("How strong the suck force is")]
    public float suckStrength;
    [SerializeField, Tooltip("How long the suck will last")]
    public float suckTime;
    private float currentTime = 0;
    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        hitbox.enabled = true;
        IsAttacking = true;
    }

    private void Update()
    {
        if (!IsAttacking) return;
        if (currentTime >= suckTime)
        {
            currentTime = 0f;
            IsAttacking = false;
            hitbox.enabled = false;
            GetComponent<Boss>().FinishAttack();
            return;
        }
        currentTime += Time.deltaTime;
        //Debug.Log(currentTime);
    }
}
