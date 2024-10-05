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
        if (pInput.Player.Fall.WasPerformedThisFrame())
        {
            effector.rotationalOffset = 180f;
            playerObject.GetComponent<Rigidbody2D>().gravityScale = 4.5f;
        }
        if (pInput.Player.Jump.WasPerformedThisFrame())
        {
            effector.rotationalOffset = 0f;
        }
    }
}