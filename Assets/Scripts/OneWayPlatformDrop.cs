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
        float verticalInput = pInput.Player.Move.ReadValue<Vector2>().y;
        if (verticalInput < 0) // Flip Platforms
        {
            effector.rotationalOffset = 180f;
        }
        else // Reset Platforms
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