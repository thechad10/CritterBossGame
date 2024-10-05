using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamageAnimGA : GameAction
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    public override void Action()
    {
        StartCoroutine(nameof(EndAnimation));
    }

    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        playerHealth.playerIsTakingDamage = false;

    }
}