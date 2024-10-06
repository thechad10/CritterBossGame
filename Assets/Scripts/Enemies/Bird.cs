using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Bird : Boss
{
    [Header("Bird Specific")]
    [SerializeField, Tooltip("The amount of time it takes to execute the first attack")]
    private float firstAttackDelay;
    [SerializeField, Tooltip("The amount of time it takes to execute another attack")]
    private float attackDelay;
    private bool isBeginningOfFight;
    private int attackIndex;
    private int cachedIndex = 0;
    private float startingTimer;

    private void Update()
    {
        if (isBeginningOfFight)
        {
            startingTimer += Time.deltaTime;

            if (startingTimer >= firstAttackDelay)
            {
                isBeginningOfFight = false;
                startingTimer = 0;
            }
            else
            {
                return;
            }
        }
        if (IsAttacking) return;
        StartAttack();
        
        do
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
        } while (attackIndex == cachedIndex);
        attackIndex = cachedIndex;

        StartCoroutine(StartAttack(attackIndex));
    }

    IEnumerator StartAttack(int attackIndex)
    {
        yield return new WaitForSeconds(attackDelay);
        Attacks[attackIndex].Attack();
    }
}