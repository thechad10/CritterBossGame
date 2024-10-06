using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombingRun_EA : EnemyAttack
{
    [SerializeField, Tooltip("How long this attack performs")]
    private float attackTime;
    [SerializeField, Tooltip("How long it takes to rise and fall")]
    private float yMoveTime;
    public float TotalTime => attackTime + yMoveTime;
    private float timeLeft;
    private BombingState bombState;
    [SerializeField, Tooltip("How many this will drop a bomb during a run")]
    private int bombDrops;
    [SerializeField, Tooltip("Speed this moves")]
    private float moveSpeed;
    [SerializeField, Tooltip("Used for the x-position")]
    private AnimationCurve xPosition;
    [SerializeField, Tooltip("Used for the y-position")]
    private AnimationCurve yPosition;
    [SerializeField, Tooltip("Used for the max position calculations")]
    private Transform maxPos;
    [SerializeField, Tooltip("Used for the min position calculations")]
    private Transform minPos;
    private float moveRate;
    private float attackRate;
    private List<GameObject> bombs = new();
    [SerializeField]
    private Bird bird;

    [SerializeField]
    private GameObject bombPrefab;


    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }

        hitbox.gameObject.SetActive(true);
        IsAttacking = true;
        //timeLeft = attackLength;
        timeLeft = yMoveTime;
        bombState = BombingState.rising;
    }
    private void Update()
    {
        if (!IsAttacking) return;
        if(bombState == BombingState.rising)
        {
            timeLeft -= Time.deltaTime;
            moveRate = (yMoveTime - timeLeft) / yMoveTime;
            transform.position = new Vector3(minPos.position.x, Mathf.Lerp(minPos.position.y, maxPos.position.y, yPosition.Evaluate(moveRate)), 0);
            if (timeLeft <= 0)
            {
                moveRate = 0;
                timeLeft = attackTime;
                bombState = BombingState.bombing;
            }
        }
        else if(bombState == BombingState.bombing)
        {
            timeLeft -= Time.deltaTime;
            moveRate = (attackTime - timeLeft) / attackTime;
            attackRate += Time.deltaTime;
            Debug.Log(attackRate);
            transform.position = new Vector3(Mathf.Lerp(maxPos.position.x, minPos.position.x, xPosition.Evaluate(moveRate)), maxPos.position.y, 0);
            if (attackRate >= bombDrops / attackTime)
            {
                attackRate = 0;
                Bomb();
            }

            if (timeLeft <= 0)
            {
                moveRate = 0;
                timeLeft = yMoveTime;
                bombState = BombingState.falling;
            }
        }
        else if(bombState == BombingState.falling)
        {
            timeLeft -= Time.deltaTime;
            moveRate = (yMoveTime - timeLeft) / yMoveTime;
            transform.position = new Vector3(minPos.position.x, Mathf.Lerp(maxPos.position.y, minPos.position.y, yPosition.Evaluate(moveRate)), 0);
            if (timeLeft <= 0)
            {
                moveRate = 0;
                timeLeft = attackTime;
                IsAttacking = false;
                bird.FinishAttack();
            }
        }
    }

    private void Bomb()
    {
        bombs.Add(Instantiate(bombPrefab, transform.position, transform.rotation));
        //spawn a bomb
    }

    private enum BombingState
    {
        rising,
        bombing,
        falling
    }
}
