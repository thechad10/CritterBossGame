using UnityEngine;
using System;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("Hitbox this attack will do")]
    protected Collider2D hitbox;
    public bool IsAttacking { get; protected set; }
    public abstract void Attack();
}
