using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityRefresh : MonoBehaviour
{
    [HideInInspector] public bool playerSpecialAbilityReady;
    private float refreshTime = 30f;
    private float refreshCounter;
    private Image playerAbilityFill;

    private PlayerInput pInput;

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        playerAbilityFill = GameObject.FindWithTag("PlayerAbilityFill").GetComponent<Image>();
        refreshCounter = refreshTime;
    }

    private void Update()
    {
        playerAbilityFill.fillAmount = refreshCounter / refreshTime;

        if (!playerSpecialAbilityReady)
        {
            refreshCounter += Time.deltaTime;

            if (refreshCounter >= refreshTime)
                playerSpecialAbilityReady = true;
        }

        if (pInput.Player.Ability.WasPerformedThisFrame() && playerSpecialAbilityReady)
        {
            refreshCounter = 0;
            playerSpecialAbilityReady = false;
        }
    }
}