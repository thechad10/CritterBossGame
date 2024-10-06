using UnityEngine;
using System.Collections;

public class PounceEA : EnemyAttack
{
    [SerializeField, Tooltip("Time to pause in the air at the top of the jump")]
    private float pauseDuration = 0.5f;

    [SerializeField, Tooltip("The height of the jump")]
    private float jumpHeight = 5f;

    [SerializeField, Tooltip("The speed of movement to the target")]
    private float moveSpeed = 5f;

    [SerializeField, Tooltip("First target point on one side of the map")]
    private Transform target1;

    [SerializeField, Tooltip("Second target point on the opposite side of the map")]
    private Transform target2;

    private Rigidbody2D rb;
    private Transform currentTarget;
    private bool movingToTarget1;
    private Vector3 originalScale;

    private Animator coyoteAnimator;


    private void Start()
    {
        coyoteAnimator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody2D>();

        if (target1 == null || target2 == null)
        {
            Debug.LogError("Targets not assigned.");
            return;
        }

        originalScale = transform.localScale;

        // Decide initial target based on the enemy's position
        currentTarget = target1.position.x > transform.position.x ? target1 : target2;
        movingToTarget1 = currentTarget == target1;
    }

    public override void Attack()
    {
        if (!IsAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        IsAttacking = true;

        // Enable the hitbox for the attack duration
        hitbox.enabled = true;

        // Step 1: Jump straight up
        rb.linearVelocity = new Vector2(0, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));

        // Wait until the enemy reaches the peak of the jump
        yield return new WaitUntil(() => rb.linearVelocity.y <= 0);

        // Step 2: Pause at the top of the jump
        rb.linearVelocity = Vector2.zero; // Stop vertical movement
        yield return new WaitForSeconds(pauseDuration);

        // Step 3: Move horizontally to the opposite target
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = movingToTarget1 ? target2.position : target1.position;
        movingToTarget1 = !movingToTarget1;

        // Calculate the time it will take to move to the target
        float distance = Vector2.Distance(startPosition, targetPosition);
        float moveTime = distance / moveSpeed;

        float elapsedTime = 0f;

        // Move towards the target in a straight line
        while (elapsedTime < moveTime)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to the exact target position
        transform.position = targetPosition;

        // Step 4: Flip the enemy
        FlipEnemy();

        // Disable the hitbox after the attack
        hitbox.enabled = false;

        IsAttacking = false;
        coyoteAnimator.Play("Coyote_Idle");

        GetComponent<Boss>().FinishAttack();
    }

    private void FlipEnemy()
    {
        // Flip the enemy by inverting the x scale
        transform.localScale = new Vector3(-transform.localScale.x, originalScale.y, originalScale.z);
    }
}
