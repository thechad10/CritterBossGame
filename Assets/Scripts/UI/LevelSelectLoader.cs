using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class LevelSelectLoader : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Button firstButton;
    [SerializeField] EventSystem ui_Handler;

    [SerializeField] private float posStart, posEnd;
    [SerializeField] private float tweenDuration;
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private Character _playerCharacter;


    private void OnEnable()
    {
        ui_Handler.firstSelectedGameObject = button;
        firstButton.Select();
        rectTransform.DOAnchorPosY(posEnd, tweenDuration);
    }

    public void Kill()
    {
        rectTransform.DOAnchorPosY(posStart, tweenDuration);
        _playerCharacter.Speed = 10f;
        StartCoroutine(KillHold());
    }
    IEnumerator KillHold()
    {
        yield return new WaitForSeconds(tweenDuration);
        this.gameObject.SetActive(false);
    }
}
