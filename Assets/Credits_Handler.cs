using UnityEditor.Animations;
using UnityEngine;

public class Credits_Handler : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField] private Animator animator;
    private bool creditssActive;

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
        if (creditssActive)
        {
            if (pInput.Player.Pause.WasPerformedThisFrame() || pInput.Player.Back.WasPerformedThisFrame())
            {
                creditssActive = false;
                animator.Play("Credits_Up");
            }
        }
    }

    public void ActivateCredits()
    {
        animator.Play("Credits_Drop");
        creditssActive = true;
    }
}