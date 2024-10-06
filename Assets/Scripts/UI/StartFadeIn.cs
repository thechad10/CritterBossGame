using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartFadeIn : MonoBehaviour
{
    [SerializeField] private Image fadeBlack;
    [SerializeField] private float tweenDuration;

    private void Start()
    {
        fadeBlack.enabled = true;
        fadeBlack.DOFade(0, tweenDuration);
        StartCoroutine(DisableFadeToBlack());
    }
    IEnumerator DisableFadeToBlack()
    {
        yield return new WaitForSeconds(tweenDuration);
    }
}
