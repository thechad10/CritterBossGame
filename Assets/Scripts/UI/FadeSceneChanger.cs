using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class FadeSceneChanger : MonoBehaviour
{
    [SerializeField] private Image black;
    public void ChangeScene(string sceneName)
    {
        black.DOFade(1, 1f);
        StartCoroutine(HoldLoad(sceneName));
    }
    IEnumerator HoldLoad(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
