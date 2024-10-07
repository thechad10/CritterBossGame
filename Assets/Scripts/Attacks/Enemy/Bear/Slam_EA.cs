using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using Unity.VisualScripting;
using UnityEngine;

public class Slam_EA : EnemyAttack
{
    [SerializeField]
    private ShakeData worldShake;
    private bool bShake;
    private bool bShaking;
    private bool aTOb = false;
    private bool bTOc = false;
    [SerializeField] private float upTime = 1.5f;
    [SerializeField] private float downTime = 0.5f;
    private float Timer;
    private float acornTimer = 0;
    [SerializeField] private Collider2D handsCollider;
    [SerializeField] private Transform posA;
    [SerializeField] private Transform posB;
    [SerializeField] private Transform posC;
    private bool slam = false;
    private bool returnDown = false;
    [SerializeField] private float slamHoldTime = 1.5f;
    [SerializeField] private float returnTime = 1.5f;
    [SerializeField] private float AcornSpawnTime = 1.5f;
    [SerializeField] private float AcornSpawnRate = 0.05f;
    [SerializeField] GameObject acornPrefab;
    private List<GameObject> acorns = new List<GameObject>();
    [SerializeField] private Transform spawnA;
    [SerializeField] private Transform spawnB;
    private float spawnRange;
    [SerializeField] private Bear bear;

    private void OnEnable()
    {
        spawnRange = spawnB.position.x - spawnA.position.x;
    }
    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        IsAttacking = true;
        aTOb = true;
        Timer = 0;
    }
    private void Update()
    {
        if (IsAttacking)
        {
            if (aTOb)
            {
                Timer += Time.deltaTime;
                handsCollider.transform.position = Vector3.Lerp(posA.position, posB.position, Mathf.Clamp(Timer / upTime, 0, 1));
                if (Timer >= upTime)
                {
                    Timer = 0;
                    aTOb = false;
                    bTOc = true;
                    handsCollider.enabled = true;
                }
            }
            else if(bTOc)
            {
                Timer += Time.deltaTime;
                handsCollider.transform.position = Vector3.Lerp(posB.position, posC.position, Mathf.Clamp(Timer / downTime, 0, 1));
                if (Timer >= downTime)
                {
                    Timer = 0;
                    bTOc = false;
                    slam = true;
                    bShake = true;
                }
            }
            else if (slam)
            {
                Timer += Time.deltaTime;
                if (Timer >= slamHoldTime)
                {
                    Timer = 0;
                    slam = false;
                    returnDown = true;
                }

            }
            else if (returnDown)
            {
                Timer += Time.deltaTime;
                handsCollider.enabled = false;
                handsCollider.transform.position = Vector3.Lerp(posC.position, posA.position, Mathf.Clamp(Timer / returnTime, 0, 1));
                if (Timer >= slamHoldTime)
                {
                    Timer = 0;
                    slam = false;
                    returnDown = false;
                }
            }
            if (bShake)
            {
                bShake = false;
                bShaking = true;
                Shake();
            }
            if (bShaking)
            {
                Timer += Time.deltaTime;
                acornTimer += Time.deltaTime;
                if (acornTimer >= AcornSpawnRate)
                {
                    acornTimer = 0;
                    acornSpawn();
                }

                if (Timer >= AcornSpawnTime)
                {
                    bShaking = false;
                    acornTimer = 0;
                    Timer = 0;
                }
            }
            else
            {
                stopShake();
            }
        }
        
    }

    private void acornSpawn()
    {
        acorns.Add(Instantiate(acornPrefab, new Vector2(Random.Range(spawnA.position.x, spawnB.position.x), spawnA.position.y), spawnB.rotation));
    }
    private void Shake()
    {
        CameraShakerHandler.Shake(worldShake);
    }

    private void stopShake()
    {
        CameraShakerHandler.Stop();
        bear.FinishAttack();
        handsCollider.enabled = false;
    }

}
