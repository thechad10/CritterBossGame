using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BombingRun_EA : EnemyAttack
{
    [SerializeField, Tooltip("Speed this move attacks")]
    private float attackSpeed;
    [SerializeField, Tooltip("Speed this moves")]
    private float moveSpeed;
    [SerializeField, Tooltip("Used for the x-position")]
    private AnimationCurve xPosition;
    [SerializeField, Tooltip("Used for the x AnimationCurve's calculation")]
    private Transform xMaxPos;
    [SerializeField, Tooltip("Used for the x AnimationCurve's calculation")]
    private Transform xMinPos;
    [SerializeField, Tooltip("Used for the y height of Bird")]
    private Transform yPos;
    private float moveRate;
    private float attackRate;
    private List<GameObject> Bombs = new List<GameObject>();
    [SerializeField]
    private Bird bird;
    private Vector3 maxPos;
    private Vector3 minPos;

    [SerializeField]
    private GameObject bombPrefab;

    private void OnEnable()
    {
        maxPos = xMaxPos.position;
        minPos = xMinPos.position;
    }
    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }

        hitbox.gameObject.SetActive(true);
        IsAttacking = true;
    }
    private void Update()
    {
        bird.bIsAttacking = IsAttacking;
        if (!IsAttacking) return;
        moveRate += Time.deltaTime * moveSpeed;
        attackRate += Time.deltaTime * attackSpeed;
        float range = maxPos.x - minPos.x;
        transform.position =
            new Vector3(Mathf.Clamp((range * xPosition.Evaluate(moveRate)) + minPos.x, minPos.x, maxPos.x), yPos.position.y, 0);
        if (attackRate >= 10)
        {
            attackRate = 0;
            Bomb();
        }
        if (moveRate >= 1)
        {
            moveRate = 0;
            IsAttacking = false;
        }
    }

    private void Bomb()
    {
        Bombs.Add(Instantiate(bombPrefab, transform.position, transform.rotation));
        //spawn a bomb
    }
}
