using UnityEngine;
using System.Collections.Generic;

public class SquirrelSpecial : MonoBehaviour
{
    [Header("Projectile Pooling")]
    [SerializeField] private GameObject squirrelSpecialProjectile;
    private int spawnPoolAmount = 3;
    private List<GameObject> projectilePool = new List<GameObject>();

    [Header("Projectile Attributes")]
    [SerializeField] private float projectileThrowForce = 30f;
    [SerializeField] private float projectileActiveTime = 3f;
    private float projectileAngleForce;

    private PlayerInput pInput;
    private PlayerMovement playerMovement;
    private PlayerAbilityRefresh playerAbilityRefresh;
    private Animator playerAnimator;
    private PlayerHealth playerHealth;

    public bool playerSpecialAttacking;

    void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        playerMovement = GetComponent<PlayerMovement>();
        playerAbilityRefresh = GetComponent<PlayerAbilityRefresh>();
        playerAnimator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        for (int i = 0; i < spawnPoolAmount; i++) // Instantiate and add projectiles to pool
        {
            GameObject spwanedProj = Instantiate(squirrelSpecialProjectile, new Vector2(-10, 0), Quaternion.identity);
            spwanedProj.GetComponent<SpriteRenderer>().enabled = false;
            projectilePool.Add(spwanedProj);
        }

        projectileAngleForce = projectileThrowForce / 6; // 15 degree rise/fall
    }

    private void OnDisable()
    {
        pInput.Disable();
    }

    void Update()
    {
        if (pInput.Player.Ability.WasPerformedThisFrame() && playerAbilityRefresh.playerSpecialAbilityReady) // Projectile Logic
        {
            playerAbilityRefresh.playerSpecialAbilityReady = false;
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        int incrementor = 0;
        FindObjectOfType<SoundControl>().Play("PlayerShuriken");

        foreach (GameObject projectile in projectilePool)
        {
            playerSpecialAttacking = true;
            if (!playerMovement.playerIsJumping && !playerHealth.playerIsTakingDamage && !playerHealth.playerIsNowDead)
                playerAnimator.Play("Squirrel_Attack");

            projectile.transform.position = transform.position;

            if (incrementor == 0) // Fly Straight
            {
                if (playerMovement.bPlayerIsFacingRight) // Fire Right Logic
                {
                    projectile.GetComponent<EnablePlayerProjectile>().NinjaStar_EnableProjectileParameters(projectileActiveTime, projectileThrowForce, false, 0);
                }
                else                                     // Fire Left Logic
                {
                    projectile.GetComponent<EnablePlayerProjectile>().NinjaStar_EnableProjectileParameters(projectileActiveTime, -projectileThrowForce, true, 0);
                }
            }
            if (incrementor == 1) // Fly Up
            {
                if (playerMovement.bPlayerIsFacingRight)
                {
                    projectile.GetComponent<EnablePlayerProjectile>().NinjaStar_EnableProjectileParameters(projectileActiveTime, projectileThrowForce, false, projectileAngleForce);
                }
                else
                {
                    projectile.GetComponent<EnablePlayerProjectile>().NinjaStar_EnableProjectileParameters(projectileActiveTime, -projectileThrowForce, true, projectileAngleForce);
                }
            }
            if (incrementor == 2) // Fly Down
            {
                if (playerMovement.bPlayerIsFacingRight)
                {
                    projectile.GetComponent<EnablePlayerProjectile>().NinjaStar_EnableProjectileParameters(projectileActiveTime, projectileThrowForce, false, -projectileAngleForce);
                }
                else
                {
                    projectile.GetComponent<EnablePlayerProjectile>().NinjaStar_EnableProjectileParameters(projectileActiveTime, -projectileThrowForce, true, -projectileAngleForce);
                }
                break;
            }

            incrementor++;
        }
    }
}