using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionTrigger : MonoBehaviour
{
    [SerializeField]
    private bool bDisableTrigger;
    [SerializeField]
    private List<GameAction> gActions;
    private void OnTriggerEnter(Collider other)
    {
        if (bDisableTrigger) return;
        PlaySequence();
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
