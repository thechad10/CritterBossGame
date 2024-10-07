using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteHandler : MonoBehaviour
{
    [SerializeField] private Vignette hotWub;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        float intensity = Mathf.PingPong(timer * 2, 0.2f) + 0.4f;

        hotWub.intensity.Override(intensity);
    }
}