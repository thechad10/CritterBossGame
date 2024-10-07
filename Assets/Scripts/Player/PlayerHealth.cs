using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats pStats;
    private int MaxHealth => PlayerStats.maxHealth;
    private int curHealth = PlayerStats.currentHealth; //not sure if this sets up a reference to it or copies the value into it

    [SerializeField]
    private float deathWaitTime;
    public List<GameAction> deathAction = new List<GameAction>();
    [SerializeField]
    private float hitWaitTime;
    public List<GameAction> hitAction = new List<GameAction>();

    private bool isImmune = false;
    [SerializeField]
    private float iFrames = 2f;
    [HideInInspector] public TextMeshProUGUI playerHPText;

    private Animator playerAnimator;
    public bool playerIsTakingDamage, playerIsNowDead;
    public int publicCurrentHealthPlayer;
    public bool publicIsImmunePlayer;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerHPText = GameObject.FindWithTag("PlayerHP_Text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        publicCurrentHealthPlayer = curHealth;
        publicIsImmunePlayer = isImmune;
    }

    private void OnEnable()
    {
        curHealth = MaxHealth;
        if (playerHPText)
            playerHPText.text = ("HP: " + curHealth);
    }
    public void ModHealth(int modifier)
    {
        if (!isImmune) 
        {
            curHealth = (int)Mathf.Clamp(curHealth + modifier, 0, MaxHealth);
            playerHPText.text = ("HP: " + curHealth);
            ;
            if(curHealth <= 0)
            {
                StartCoroutine(nameof(DeathSeq));
            }
            else if (modifier < 0)
            {
                playerIsTakingDamage = true;
                playerAnimator.Play("Squirrel_Damaged");
                playerIsTakingDamage = false; // this needs its own timer shit
                isImmune = true;
                StartCoroutine(nameof(HitSeq));
                StartCoroutine(nameof(ImmuneDelay));
            }
        }
    }
    IEnumerator ImmuneDelay()
    {
        int immuneTickCount = 0;
        while (isImmune)
        {
            yield return new WaitForSeconds(1);
            {
                if (immuneTickCount < iFrames)
                {
                    immuneTickCount += 1;
                }
                else
                {
                    isImmune = false;
                }
            }
        }
    }

    IEnumerator DeathSeq()
    {
        playerIsNowDead = true;
        //playerAnimator.Play("Squirrel_Dead");

        for (int x = 0; x < deathAction.Count; x++)
        {
            yield return new WaitForSeconds(deathAction[x].delay);
            deathAction[x].Action();
        }
    }
    IEnumerator HitSeq()
    {
        for (int x = 0; x < hitAction.Count; x++)
        {
            yield return new WaitForSeconds(hitWaitTime);
            hitAction[x].Action();
        }
    }
}
