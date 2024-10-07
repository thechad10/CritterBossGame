using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class FireBalls_EA : EnemyAttack
{
    [SerializeField]
    private float arc = 45;
    [SerializeField, Tooltip("Speed this move attacks")]
    private float attackSpeed;
    [SerializeField, Tooltip("Speed this moves")]
    private float moveSpeed;
    [SerializeField, Tooltip("Used for the y-position")]
    private AnimationCurve xPosition;
    [SerializeField, Tooltip("Used for the y AnimationCurve's calculation")]
    private Transform yMaxPos;
    [SerializeField, Tooltip("Used for the y AnimationCurve's calculation")]
    private Transform yMinPos;
    [SerializeField, Tooltip("Used for the x pos of Bird")]
    private Transform xPos;
    private float moveRate;
    private float attackRate;
    [SerializeField]
    private Bird bird;
    [SerializeField]
    private int ballCount;
    private Vector3 maxPos;
    private Vector3 minPos;
    private List<GameObject> fireballs = new List<GameObject>();

    [SerializeField]
    private GameObject fireballPrefab;
    private Animator birdAnimator;

    private void OnEnable()
    {
        maxPos = yMaxPos.position;
        minPos = yMinPos.position;
    }
    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        birdAnimator = GetComponent<Animator>();
        attackRate = 0;
        moveRate = 0;
        hitbox.gameObject.SetActive(true);
        IsAttacking = true;
    }
    private void Update()
    {
        if (GetComponent<Boss>().IsDead) return;
        if (!IsAttacking) return;
        moveRate += Time.deltaTime * moveSpeed;
        attackRate += Time.deltaTime * attackSpeed;
        float range = maxPos.y - minPos.y;
        transform.position =
            new Vector3(xPos.position.x, Mathf.Clamp((range * xPosition.Evaluate(moveRate)) + minPos.y, minPos.y, maxPos.y), 0);
        //Debug.Log(Mathf.Clamp((range * xPosition.Evaluate(moveRate)) + minPos.y, minPos.y, maxPos.y));
        //Debug.Log((range * -xPosition.Evaluate(moveRate)));
        if (attackRate >= 10)
        {
            attackRate = 0;
            StartCoroutine(FIREBALL());
        }
        if (moveRate >= 1)
        {
            moveRate = 0;
            IsAttacking = false;
            //birdAnimator.Play("Phoenix_Idle");
            bird.FinishAttack();
        }
    }

    IEnumerator FIREBALL()
    {
        birdAnimator.Play("Phoenix_Fireball");
        FindObjectOfType<SoundControl>().Play("BirdFireball");
        Quaternion temp = transform.rotation;
        transform.rotation = Quaternion.Euler(Vector3.left);
        for(int i = 0; i < ballCount; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90 + (arc * ((i / ((float)ballCount - 1)) - 0.5f)));
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0,0, transform.eulerAngles.z + (90 - 45)), Quaternion.Euler(0, 0, transform.eulerAngles.z + (90 + 45)), i / ballCount);
            fireballs.Add(Instantiate(fireballPrefab, (transform.forward * 1) + transform.position, transform.rotation));
            fireballs[fireballs.Count - 1].layer = 14;
        }
        transform.rotation = temp;
        yield return new WaitForSeconds(0.4f);
        birdAnimator.Play("Phoenix_Idle");
    }
}

