using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Coyote : Boss
{
    [SerializeField, Tooltip("Attacks that are available in second phase")]
    private List<EnemyAttack> secondPhaseAttacks = new();
    [SerializeField, Tooltip("The amount of time it takes to execute another attack")]
    private float attackDelay;
    private bool bIsAttacking;
    [SerializeField, Tooltip("Whether or not this boss is in second phase")]
    private bool isSecondPhase;

    private void Start()
    {
    
    }

    private void Update()
    {
        if (bIsAttacking) return;

        int attackIndex;

        if (isSecondPhase)
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
        }
        else //isn't second phase
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count-1);
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        Attacks[0].Attack();
    }

    public void StartSecondPhase()
    {
        isSecondPhase = true;
    }
}