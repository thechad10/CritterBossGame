using UnityEngine;
using UnityEngine.EventSystems;

public class ReselectButton : MonoBehaviour
{
    public GameObject defaultButton;

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton);
        }
    }
}