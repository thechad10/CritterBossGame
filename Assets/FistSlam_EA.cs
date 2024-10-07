using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistSlam_EA : EnemyAttack
{
    private Animator animator;

    [SerializeField]
    private ShakeData worldShake;
    [SerializeField] GameObject acornPrefab;

    private List<GameObject> acorns = new List<GameObject>();
    [SerializeField] private Transform spawnA;
    [SerializeField] private Transform spawnB;
    private bool keepSpawning, happenOnce;
    private float acornTimer = 0.15f, acornStart = 1.17f;
    private Bear bear;


    private void Start()
    {
        animator = GetComponent<Animator>();
        bear = GetComponent<Bear>();
    }

    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        StartCoroutine(nameof(FinishAnimsAttack));
    }

    private IEnumerator FinishAnimsAttack()
    {
        if (!bear.IsDead)

            animator.Play("Bear_Slam");
        keepSpawning = true;
        acornStart = 1.1f;
        happenOnce = false;
        float animationDuration = GetAnimationDuration("Bear_Slam");
        FindObjectOfType<SoundControl>().Play("BearSlam");

        yield return new WaitForSeconds(animationDuration);
        keepSpawning = false;
        CameraShakerHandler.Stop();

        if (!bear.IsDead)
            animator.Play("Bear_Idle");
        GetComponent<Boss>().FinishAttack();
    }

    private float GetAnimationDuration(string animationName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        Debug.LogError($"Animation {animationName} not found!");
        return 0f;
    }

    private void Update()
    {
        if (acornStart > 0)
            acornStart -= Time.deltaTime;

        if (keepSpawning && acornStart <=0)
        {
            if (acornTimer > 0f)
                acornTimer -= Time.deltaTime;
            if (acornTimer <= 0f)
            {
                acornSpawn();
                acornTimer = 0.1f;
                if (!happenOnce)
                {
                    happenOnce = true;
                    CameraShakerHandler.Shake(worldShake);
                }
            }
        }
    }
    private void acornSpawn()
    {
        acorns.Add(Instantiate(acornPrefab, new Vector2(Random.Range(spawnA.position.x, spawnB.position.x), spawnA.position.y), spawnB.rotation));
        acorns[acorns.Count - 1].GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.95f, 1.05f);
        acorns[acorns.Count - 1].transform.localScale = Vector3.one * Random.Range(0.75f, 1.05f);
        FindObjectOfType<SoundControl>().Play("BearRocks");
    }
}