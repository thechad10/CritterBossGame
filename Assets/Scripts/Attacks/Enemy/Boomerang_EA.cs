using UnityEngine;

public class Boomerang_EA : EnemyAttack
{
    [SerializeField, Tooltip("Used for the x-position")]
    private AnimationCurve xPosition;
    [SerializeField, Tooltip("Used for the y-position")]
    private AnimationCurve yPosition;
    public override void Attack()
    {
        hitbox.gameObject.SetActive(true);
    }
    private void Update()
    {
        
    }
}
