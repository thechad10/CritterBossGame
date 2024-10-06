using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombingRun_EA : EnemyAttack
{
    [SerializeField, Tooltip("How long this attack takes to perform")]
    private float attackLength;
    private float timeLeft;
    [SerializeField, Tooltip("How many this will drop a bomb during a run")]
    private int bombDrops;
    [SerializeField, Tooltip("Speed this moves")]
    private float moveSpeed;
    [SerializeField, Tooltip("Used for the x-position")]
    private AnimationCurve xPosition;
    [SerializeField, Tooltip("Used for the y-position")]
    private AnimationCurve yPosition;
    [SerializeField, Tooltip("Used for the x AnimationCurve's calculation")]
    private Transform xMaxPos;
    [SerializeField, Tooltip("Used for the x AnimationCurve's calculation")]
    private Transform xMinPos;
    [SerializeField, Tooltip("Used for the y height of Bird")]
    private Transform yMaxPos;
    [SerializeField, Tooltip("Used for the y height of Bird")]
    private Transform yMinPos;
    private float moveRate;
    private float attackRate;
    private List<GameObject> bombs = new();
    [SerializeField]
    private Bird bird;
    private Vector3 maxPos;
    private Vector3 minPos;

    [SerializeField]
    private GameObject bombPrefab;

    private void OnEnable()
    {
        maxPos = new Vector3(xMaxPos.position.x, yMaxPos.position.y, 0);
        minPos = new Vector3(xMinPos.position.x, yMinPos.position.y, 0);
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
        timeLeft = attackLength;
    }
    private void Update()
    {
        if (!IsAttacking) return;
        timeLeft -= Time.deltaTime;
        moveRate = (attackLength - timeLeft) / attackLength;
        attackRate += Time.deltaTime;
        Debug.Log(attackRate);
        float range = maxPos.x - minPos.x;
        transform.position =
            new Vector3(Mathf.Clamp((range * xPosition.Evaluate(moveRate)) + minPos.x, minPos.x, maxPos.x),
                        Mathf.Clamp((range * yPosition.Evaluate(moveRate)) + minPos.y, minPos.y, maxPos.y), 
                        0);
        if (attackRate >= bombDrops/attackLength)
        {
            attackRate = 0;
            Bomb();
        }

        if (timeLeft <= 0)
        {
            moveRate = 0;
            IsAttacking = false;
            bird.FinishAttack();
        }
    }

    private void Bomb()
    {
        bombs.Add(Instantiate(bombPrefab, transform.position, transform.rotation));
        //spawn a bomb
    }
}
