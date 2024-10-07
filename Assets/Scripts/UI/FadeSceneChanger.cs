using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class FadeSceneChanger : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private Image black;
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1;
        black.DOFade(1, 1f);
        m_AudioSource.DOFade(0, 1f);
        StartCoroutine(HoldLoad(sceneName));
    }
    IEnumerator HoldLoad(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
