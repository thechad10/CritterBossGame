using UnityEngine;
using System.Collections.Generic;

public class SquirrelPrimary : MonoBehaviour
{
    [Header("Projectile Pooling")]
    [SerializeField] private GameObject squirrelProjectile;
    [SerializeField] private int spawnPoolAmount;
    private List<GameObject> projectilePool = new List<GameObject>();
    private int listIncrementor;

    [Header("Attacking Attributes")]
    [SerializeField] private float attackSpeed = 0.25f;
    private float attackCounter;
    private bool attackReady;

    [Header("Projectile Attributes")]
    [SerializeField] private float projectileThrowForce = 30f;
    [SerializeField] private float projectileActiveTime = 3f;
    [SerializeField] private float projectileArcForce = 4f;
    [SerializeField] private float projectileFalloff = 1.5f;

    public bool playerIsAttacking;

    private PlayerInput pInput;
    private PlayerMovement playerMovement;
    private Animator playerAnimator;
    private PlayerHealth playerHealth;

    void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        for (int i = 0; i < spawnPoolAmount; i++) // Instantiate and add projectiles to pool
        {
            GameObject spwanedProj = Instantiate(squirrelProjectile, new Vector2(-10, 0), Quaternion.identity);
            spwanedProj.GetComponent<SpriteRenderer>().enabled = false;
            projectilePool.Add(spwanedProj);
        }
    }

    private void OnDisable()
    {
        pInput.Disable();
    }

    void Update()
    {
        if (attackCounter <= attackSpeed - 0.1f)
            playerIsAttacking = false;

        if (attackCounter > 0) // Resets firing
            attackCounter -= Time.deltaTime;
        else
        {
            attackReady = true;
        }

        if (pInput.Player.Attack.IsInProgress() && attackReady) // Projectile Logic
        {
            playerIsAttacking = true;
            if (!playerMovement.playerIsJumping && !playerHealth.playerIsTakingDamage && !playerHealth.playerIsNowDead)
                playerAnimator.Play("Squirrel_Attack");
            attackReady = false;

            FireProjectile();

            listIncrementor++;

            if (listIncrementor >= projectilePool.Count)
                listIncrementor = 0;
        }
    }

    private void FireProjectile()
    {
        attackCounter = attackSpeed;
        projectilePool[listIncrementor].transform.position = transform.position;
        FindObjectOfType<SoundControl>().Play("PlayerKunai");

        if (playerMovement.bPlayerIsFacingRight) // Fire Right Logic
        {
            projectilePool[listIncrementor].GetComponent<EnablePlayerProjectile>().Kunai_EnableProjectileParameters(projectileActiveTime, projectileThrowForce, false, projectileArcForce, projectileFalloff);
        }
        else                                     // Fire Left Logic
        {
            projectilePool[listIncrementor].GetComponent<EnablePlayerProjectile>().Kunai_EnableProjectileParameters(projectileActiveTime, -projectileThrowForce, true, projectileArcForce, projectileFalloff);
        }
    }
}