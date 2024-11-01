using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CurrentInputMap : MonoBehaviour
{
    public bool isGamepad;
    public GameObject defaultButton;
    private bool resetter;


    public void OnInputDeviceChanged(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
        {
            isGamepad = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (!resetter)
            {
                EventSystem.current.SetSelectedGameObject(defaultButton);
                resetter = true;
            }
        }
        else
        {
            isGamepad = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            resetter = false;
        }
    }
}