using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BombSplat : MonoBehaviour
{
    [SerializeField]
    private List<GameAction> gActions;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            PlaySequence();
        }

    }
    public void PlaySequence()
    {
        StartCoroutine(nameof(TriggerSequence));
    }
    IEnumerator TriggerSequence()
    {
        foreach (GameAction item in gActions)
        {
            yield return new WaitForSeconds(item.delay);
            item.Action();
        }
    }
}
