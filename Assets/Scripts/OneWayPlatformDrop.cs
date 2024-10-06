using System.Collections;
using UnityEngine;

public class OneWayPlatformDrop : MonoBehaviour
{
    private PlayerInput pInput;
    private PlatformEffector2D effector;
    private GameObject playerObject;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        pInput = new PlayerInput();
        pInput.Enable();
        playerObject = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (pInput.Player.Fall.WasPerformedThisFrame()) // Flip Platforms
        {
            effector.rotationalOffset = 180f;
        }
        if (pInput.Player.Fall.WasReleasedThisFrame()) // Reset Platforms
        {
            StartCoroutine(nameof(ResetDelay));
        }
    }

    IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(0.16f);
        effector.rotationalOffset = 0f;
    }
}