using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput pInput;
    [SerializeField] private bool GamePaused = false;
    [SerializeField] private GameObject pausePanelUI;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private EventSystem UIButtonStart;
    [SerializeField] private GameObject pauseStart;

    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        pInput.Player.Pause.performed += TryPause;
    }
    private void OnDisable()
    {
        pInput.Player.Pause.performed -= TryPause;
        pInput.Disable();
    }

    public void TryPause(InputAction.CallbackContext c)
    {
        if(GamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pausePanelUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        pInput.Player.Pause.performed -= TryPause;
        pInput.Disable();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelSelect(string sceneName)
    {
        pInput.Player.Pause.performed -= TryPause;
        pInput.Disable();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void Pause()
    {
        pausePanelUI.SetActive(true);
        UIButtonStart.firstSelectedGameObject = pauseStart;
        GameUI.SetActive(false);
        Time.timeScale = 0f;
    }
}
