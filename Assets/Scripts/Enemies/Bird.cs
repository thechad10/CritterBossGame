using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Bird : Boss
{
    [SerializeField, Tooltip("Attacks that are available in second phase")]
    private List<EnemyAttack> secondPhaseAttacks = new();
    [SerializeField, Tooltip("The amount of time it takes to execute another attack")]
    private float attackDelay;
    public bool bIsAttacking;
    [SerializeField]
    private float idleTime = 1;
    private float timer = 0;
    [SerializeField, Tooltip("Whether or not this boss is in second phase")]
    private bool isSecondPhase;

    private void Start()
    {

    }

    private void Update()
    {
        if (bIsAttacking) return;

        int attackIndex;

        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            if (isSecondPhase)
            {
                attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
            }
            else //isn't second phase
            {
                attackIndex = UnityEngine.Random.Range(0, Attacks.Count - 1);
            }
            StartCoroutine(nameof(StartAttack),(attackIndex));
        }

        
        
    }

    IEnumerator StartAttack(int attack)
    {
        bIsAttacking = true;
        Attacks[attack].Attack();
        yield return new WaitForSeconds(attackDelay);

        timer = 0;
        bIsAttacking = false;
    }

    public void StartSecondPhase()
    {
        isSecondPhase = true;
    }
}