using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Bird : Boss
{
    [Header("Bird Specific")]
    [SerializeField, Tooltip("Is the Phoenix")]
    private bool isSecondPhase;
    [SerializeField, Tooltip("The amount of time it takes to execute the first attack")]
    private float firstAttackDelay;
    [SerializeField, Tooltip("The amount of time it takes to execute another attack")]
    private float attackDelay;
    private bool isBeginningOfFight;
    private int attackIndex;
    private int cachedIndex = -1;
    private float startingTimer;
    private Animator birdAnimator;
    private string animName;

    private void Start()
    {
        birdAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(IsDead)
        {
            return;
        }
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
        cachedIndex = attackIndex;

        StartCoroutine(StartAttack(attackIndex));
    }

    IEnumerator StartAttack(int attackIndex)
    {
        if(isSecondPhase) //Phoenix
        {
            if (attackIndex == 0) animName = "Phoenix_Idle";
            if (attackIndex == 1) animName = "Phoenix_Charge";
        }
        else //Peacock
        {
            if (attackIndex == 0) animName = "Peacock_Bomb";
            if (attackIndex == 1) animName = "Peacock_Suck";
        }

        birdAnimator.Play(animName);
        yield return new WaitForSeconds(attackDelay);
        Attacks[attackIndex].Attack();
    }
}