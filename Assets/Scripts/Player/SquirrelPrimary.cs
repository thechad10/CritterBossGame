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
    [SerializeField] private float projectileThrowForce = 20f;
    [SerializeField] private float projectileActiveTime = 3f;

    private PlayerInput pInput;
    private PlayerMovement playerMovement;

    void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        playerMovement = GetComponent<PlayerMovement>();

        for (int i = 0; i < spawnPoolAmount; i++) // Instantiate and add projectiles to pool
        {
            GameObject spwanedProj = Instantiate(squirrelProjectile);
            spwanedProj.GetComponent<SpriteRenderer>().enabled = false;
            projectilePool.Add(spwanedProj);
        }
    }

    void Update()
    {
        if (attackCounter > 0) // Resets firing
            attackCounter -= Time.deltaTime;
        else
            attackReady = true;

        if (pInput.Player.Attack.IsInProgress() && attackReady) // Projectile Logic
        {
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

        if (GetComponent<Rigidbody2D>().linearVelocity.x != 0) // Normalise Projectile Speed to Player Speed
        {
            projectileThrowForce += GetComponent<Rigidbody2D>().linearVelocity.x;
        }

        if (playerMovement.bPlayerIsFacingRight) // Fire Right Logic
        {
            projectilePool[listIncrementor].GetComponent<EnablePlayerProjectile>().Kunai_EnableProjectileParameters(projectileActiveTime, projectileThrowForce, false);
        }
        else                                     // Fire Left Logic
        {
            projectilePool[listIncrementor].GetComponent<EnablePlayerProjectile>().Kunai_EnableProjectileParameters(projectileActiveTime, -projectileThrowForce, true);
        }
    }
}