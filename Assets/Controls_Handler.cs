using UnityEngine;

public class Controls_Handler : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField] private Animator animator;
    private bool controlsActive;

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
    }

    private void OnDisable()
    {
        pInput.Disable();
    }

    private void Update()
    {
        if (controlsActive)
        {
            if (pInput.Player.Move.ReadValue<Vector2>().x > 0)
                animator.Play("Controls_Left");
            if (pInput.Player.Move.ReadValue<Vector2>().x < 0)
                animator.Play("Controls_Right");
            if (pInput.Player.Pause.WasPerformedThisFrame() || pInput.Player.Back.WasPerformedThisFrame())
            {
                controlsActive = false;
                animator.Play("Controls_Down");
            }
        }
    }

    public void ActivateControls()
    {
        animator.Play("Controls_Up");
        controlsActive = true;
    }
}