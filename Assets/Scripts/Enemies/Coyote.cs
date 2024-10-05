using UnityEngine;
using System.Collections;
public class Coyote : Boss
{
    private void Start()
    {
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(3);
        Attacks[0].Attack();
    }
}