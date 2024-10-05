using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public bool reset = false;

    private void Update()
    {
        if (reset)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
