using UnityEngine;
using System.Collections;

public class PounceEA : EnemyAttack
{
    [SerializeField, Tooltip("The vertical jump height")]
    private float jumpHeight = 5f;

    [SerializeField, Tooltip("The speed of the horizontal charge to the target")]
    private float chargeSpeed = 5f;

    [SerializeField, Tooltip("The time to pause at the top of the jump")]
    private float pauseDuration = 0.5f;

    [SerializeField, Tooltip("Time before attack starts after initialization")]
    private float attackDelay = 2f;

    [SerializeField, Tooltip("First target point for the enemy")]
    private Transform target1;

    [SerializeField, Tooltip("Second target point for the enemy")]
    private Transform target2;

    private Rigidbody2D rb;
    private Transform currentTarget;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on enemy.");
        }

        // Ensure the targets are assigned
        if (target1 == null || target2 == null)
        {
            Debug.LogError("Targets not assigned.");
            return;
        }

        // Start the attack after a delay
        StartCoroutine(StartAttackAfterDelay());
    }

    private IEnumerator StartAttackAfterDelay()
    {
        // Wait for the attackDelay time (e.g., 2 seconds)
        yield return new WaitForSeconds(attackDelay);

        // Decide which target is farther and jump towards it
        ChooseFarthestTarget();

        // Perform the attack
        Attack();
    }

    private void ChooseFarthestTarget()
    {
        // Calculate distances from the enemy to the two targets
        float distanceToTarget1 = Vector2.Distance(transform.position, target1.position);
        float distanceToTarget2 = Vector2.Distance(transform.position, target2.position);

        // Choose the target that is farther away
        if (distanceToTarget1 > distanceToTarget2)
        {
            currentTarget = target1;
        }
        else
        {
            currentTarget = target2;
        }
    }

    public override void Attack()
    {
        if (!isJumping)
        {
            StartCoroutine(JumpAndChargeToTarget());
        }
    }

    private IEnumerator JumpAndChargeToTarget()
    {
        isJumping = true;

        // Step 1: Jump straight up
        rb.linearVelocity = new Vector2(0, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y)));

        // Wait until we reach the top of the jump
        yield return new WaitUntil(() => rb.linearVelocity.y <= 0);

        // Step 2: Pause at the top
        rb.linearVelocity = Vector2.zero; // Stop movement
        yield return new WaitForSeconds(pauseDuration);

        // Step 3: Charge in a straight line horizontally toward the current target
        Vector2 targetPosition = currentTarget.position;
        float distanceToTarget = targetPosition.x - rb.position.x;
        Vector2 chargeDirection = new Vector2(Mathf.Sign(distanceToTarget), 0); // Horizontal direction only

        // Apply horizontal velocity for the charge
        rb.linearVelocity = chargeDirection * chargeSpeed;

        // Wait until we reach the target horizontally
        yield return new WaitUntil(() => Mathf.Abs(rb.position.x - targetPosition.x) <= 0.1f);

        // Snap the enemy to the target to correct any small positioning issues
        rb.position = new Vector2(targetPosition.x, rb.position.y);
        rb.linearVelocity = Vector2.zero; // Stop movement

        // Switch to the next target after completing the charge (optional for future attacks)
        currentTarget = (currentTarget == target1) ? target2 : target1;

        isJumping = false;

        // Start the next attack after a delay (optional)
        StartCoroutine(StartAttackAfterDelay());
    }
}
