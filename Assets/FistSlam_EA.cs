using UnityEngine;

public class FistSlam_EA : EnemyAttack
{
    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        //start attack shit here
    }
}