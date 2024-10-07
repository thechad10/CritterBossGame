using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    //private Animator bearAnimator;
    //private string animName;

    private void Start()
    {
        //bearAnimator = GetComponentInChildren<Animator>();
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
        if (IsDead) return;
        if (HealthPercentage <= 0.5 && !isSecondPhase)
        {
            StartSecondPhase();
        }

        if (IsAttacking) return;
        StartAttack();
        int attackIndex;

        if (!isSecondPhase) //first phase
        {
            attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
            
            //{
                attackIndex = UnityEngine.Random.Range(0, Attacks.Count);
                chosenAttack = Attacks[attackIndex];
            //} while (cachedAttack == chosenAttack);*/
        }
        else //second phase
        {
            attackIndex = UnityEngine.Random.Range(0, secondPhaseAttacks.Count);
            /*do
            {
                attackIndex = UnityEngine.Random.Range(0, secondPhaseAttacks.Count);
                chosenAttack = secondPhaseAttacks[attackIndex];
            } while (cachedAttack == chosenAttack);*/
        }
        cachedAttack = chosenAttack;

        StartCoroutine(Attack(chosenAttack, attackIndex));
    }

    IEnumerator Attack(EnemyAttack eAttack, int attackAnim)
    {
        /*if (attackAnim == 0) animName = "Coyote_Swipe_High";
        if (attackAnim == 1) animName = "Coyote_Pounce";
        if (attackAnim == 2) animName = "Coyote_Swipe_Low";
        if (attackAnim == 3) animName = "Coyote_Sling";

        bearAnimator.Play(animName);*/
        yield return new WaitForSeconds(attackDelay);
        eAttack.Attack();
    }

    public void StartSecondPhase()
    {
        isSecondPhase = true;
        Debug.Log("BEAR HAS ENTERED SECOND PHASE!");
    }
}