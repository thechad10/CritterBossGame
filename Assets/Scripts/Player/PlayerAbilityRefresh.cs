using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityRefresh : MonoBehaviour
{
    [HideInInspector] public bool playerSpecialAbilityReady;
    private float refreshTime = 15f;
    public float refreshCounter;
    private Image playerAbilityFill;

    private PlayerInput pInput;
    private SquirrelSpecial squirrelSpecial;

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        squirrelSpecial = GetComponent<SquirrelSpecial>();
        if (GameObject.FindWithTag("PlayerAbilityFill"))
            playerAbilityFill = GameObject.FindWithTag("PlayerAbilityFill").GetComponent<Image>();
        refreshCounter = refreshTime / 2;
    }

    private void OnDisable()
    {
        pInput.Disable();
    }

    private void Update()
    {
        if (playerAbilityFill)
            playerAbilityFill.fillAmount = refreshCounter / refreshTime;

        if (!playerSpecialAbilityReady)
        {
            GameObject.FindWithTag("PlayerAbilityFill").transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.FindWithTag("PlayerAbilityOut").transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.FindWithTag("PlayerAbilityBorder").transform.localScale = new Vector3(1f, 1f, 1f);

            refreshCounter += Time.deltaTime;

            if (refreshCounter >= refreshTime)
            {
                GameObject.FindWithTag("PlayerAbilityFill").transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                GameObject.FindWithTag("PlayerAbilityOut").transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                GameObject.FindWithTag("PlayerAbilityBorder").transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                playerSpecialAbilityReady = true;
            }
        }

        if (refreshCounter >= 0.1f) // animation check
        {
            squirrelSpecial.playerSpecialAttacking = false;
        }

        /*if (pInput.Player.Ability.WasPerformedThisFrame() && playerSpecialAbilityReady)
        {
        }*/
    }
}