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
    [SerializeField, Tooltip("Attacks that are available in second phase")]
    private List<EnemyAttack> secondPhaseAttacks = new();
    [SerializeField, Tooltip("Whether or not this boss is in second phase")]
    private bool isSecondPhase;
    private bool isBeginningOfFight;
    private float startingTimer;
    private void Start()
    {

    }

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
        int attackIndex;

        if (isSecondPhase)
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
        }
        else //isn't second phase
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
        }
        StartCoroutine(nameof(StartAttack),(attackIndex));
    }

    IEnumerator StartAttack(int attack)
    {
        yield return new WaitForSeconds(attackDelay);
        Attacks[attack].Attack();
    }

    public void StartSecondPhase()
    {
        isSecondPhase = true;
    }
}