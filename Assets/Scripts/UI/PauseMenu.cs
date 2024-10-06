using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput pInput;
    [SerializeField] private bool GamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private EventSystem UIButtonStart;
    [SerializeField] private GameObject pauseStart;

    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        pInput.Player.Pause.performed += Pause;
    }
    private void OnDisable()
    {
        pInput.Player.Pause.performed -= Pause;
        pInput.Disable();
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
    }
    public void Pause(InputAction.CallbackContext c)
    {
        pauseMenuUI.SetActive(true);
        UIButtonStart.firstSelectedGameObject = pauseStart;
        GameUI.SetActive(false);
        Time.timeScale = 0f;
    }
}
