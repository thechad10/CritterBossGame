using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private RectTransform ui_Element;
    [SerializeField] private GameObject go;
    [SerializeField] private float startPos, endPos;
    private void OnEnable()
    {
        ui_Element.DOAnchorPosY(endPos, 1);
        go.SetActive(false);
    }
}
