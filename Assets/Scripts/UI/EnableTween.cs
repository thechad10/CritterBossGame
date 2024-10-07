using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Button firstButton;
    [SerializeField] EventSystem ui_Handler;
    [SerializeField] private RectTransform ui_Element;
    [SerializeField] private GameObject go;
    [SerializeField] private float startPos, endPos;
    private void OnEnable()
    {
        ui_Handler.firstSelectedGameObject = button;
        firstButton.Select();
        ui_Element.DOAnchorPosY(endPos, 1);
        go.SetActive(false);
    }
}
