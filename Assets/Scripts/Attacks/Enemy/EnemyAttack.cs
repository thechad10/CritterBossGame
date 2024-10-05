using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("Hitbox this attack will do")]
    protected Collider2D hitbox;

    public abstract void Attack(); 
}
