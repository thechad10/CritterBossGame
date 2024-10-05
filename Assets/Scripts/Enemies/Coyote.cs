using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Coyote : Boss
{
    [Header("Coyote Specific")]
    [SerializeField, Tooltip("The amount of time it takes to execute another attack")]
    private float attackDelay;
    [SerializeField, Tooltip("Attacks that are available in second phase")]
    private List<EnemyAttack> secondPhaseAttacks = new();
    private bool isSecondPhase = false;
    private EnemyAttack chosenAttack;

    private void Update()
    {
        if (IsDead) return;
        if(HealthPercentage <= 0.5 && !isSecondPhase)
        {
            StartSecondPhase();
        }

        if (IsAttacking) return;
        StartAttack();
        int attackIndex;

        if (!isSecondPhase) //first phase
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
            chosenAttack = Attacks[attackIndex];
        }
        else //second phase
        {
            attackIndex = UnityEngine.Random.Range(0, secondPhaseAttacks.Count);
            chosenAttack = secondPhaseAttacks[attackIndex];
        }

        StartCoroutine(Attack(chosenAttack));
    }

    IEnumerator Attack(EnemyAttack eAttack)
    {
        yield return new WaitForSeconds(attackDelay);
        eAttack.Attack();
    }

    public void StartSecondPhase()
    {
        isSecondPhase = true;
        Debug.Log("COYOTE HAS ENTERED SECOND PHASE!");
    }
}