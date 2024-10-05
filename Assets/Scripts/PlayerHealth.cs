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

    public void modHealth(int modifier)
    {
        pStats.currentHealth += modifier;
        if(curHealth <= 0)
        {
            StartCoroutine(nameof(DeathSeq));
        }
        else if (modifier < 0)
        {
            StartCoroutine(nameof(HitSeq));
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
