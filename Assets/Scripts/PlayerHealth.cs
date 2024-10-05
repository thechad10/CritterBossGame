using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats pStats;
    private int maxHealth => pStats.maxHealth;
    private int curHealth => pStats.currentHealth;

    [SerializeField]
    private float deathWaitTime;
    public List<GameAction> deathAction;
    [SerializeField]
    private float hitWaitTime;
    public List<GameAction> hitAction;

    private bool isImmune;
    [SerializeField]
    private float iFrames = 2f;

    public void modHealth(int modifier)
    {
        if (!isImmune) 
        { 
            pStats.currentHealth += modifier;
            if(curHealth <= 0)
            {
                StartCoroutine(nameof(DeathSeq));
            }
            else if (modifier < 0)
            {
                StartCoroutine(nameof(HitSeq));
                StartCoroutine(nameof(ImmuneDelay));
            }
        }
    }
    IEnumerator ImmuneDelay() // Perform a series of game actions upon spawning
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

        for (int x = 0; x < deathAction.Count; x++)
        {
            yield return new WaitForSeconds(deathWaitTime);
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
