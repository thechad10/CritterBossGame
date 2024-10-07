using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bear : Boss
{
    [Header("Coyote Specific")]
    [SerializeField, Tooltip("The amount of time it takes to execute the first attack")]
    private float firstAttackDelay;
    [SerializeField, Tooltip("The amount of time it takes to execute another attack")]
    private float attackDelay;
    [SerializeField, Tooltip("Attacks that are available in second phase")]
    private List<EnemyAttack> secondPhaseAttacks = new();
    private bool isSecondPhase = false;
    private EnemyAttack chosenAttack;
    private EnemyAttack cachedAttack;
    private bool isBeginningOfFight = true;
    private float startingTimer;
    private Animator bearAnimator;
    //private string animName;
    private Animator animator;

    private void Start()
    {
        bearAnimator = GetComponent<Animator>();
        animator = GetComponent<Animator>();
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
        if (IsDead)
        {
            return;
        }

        if (HealthPercentage <= 0.5 && !isSecondPhase && !IsDead)
        {
            StartSecondPhase();
        }


        if (IsAttacking) return;
        StartAttack();
        int attackIndex;

        if (!isSecondPhase) //first phase
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
            
            do{
                if (IsDead) break;
                attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
                chosenAttack = Attacks[attackIndex];
            } while (cachedAttack == chosenAttack && !IsDead);
        }
        else //second phase
        {
            attackIndex = UnityEngine.Random.Range(0, secondPhaseAttacks.Count);
            do
            {
                if (IsDead) break;
                attackIndex = UnityEngine.Random.Range(0, secondPhaseAttacks.Count);
                chosenAttack = secondPhaseAttacks[attackIndex];
            } while (cachedAttack == chosenAttack);
        }
        cachedAttack = chosenAttack;

        if (!IsDead)
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
        Debug.Log("BEAR HAS ENTERED SECOND PHASE!");
    }
}