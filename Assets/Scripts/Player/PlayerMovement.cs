using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpStrength = 7f;

    [Header("Cayote Time / Jump Buffering")]
    [SerializeField] private float cayoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.2f;

    private float cayoteCounter = 0f, jumpBufferCounter = 0f;
    private bool isGrounded, canJump, jumpQueued;
    private int sequence = 0;
    private Rigidbody2D rb;
    private PlayerInput pInput;
    private Collider2D groundCollider;

    [HideInInspector] public bool bPlayerIsFacingRight;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 4f;
        pInput = new PlayerInput();
        pInput.Enable();
        groundCollider = GetComponentInChildren<Collider2D>();
    }

    private void Update()
    {
        if (rb.linearVelocity.y < -30f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -30f);
        }
    }

    private void FixedUpdate()
    {
        MoveLR(); // Control LR Movement
        CayoteTime(); // Cayote Time Check
        JumpBuffering(); // Jump Buffer Check
        JumpFunction(); // Control Jump
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Platform") // Grounded Check
        {
            isGrounded = true;
            rb.gravityScale = 4f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Platform") // Grounded Check
            isGrounded = false;
    }

    private void MoveLR()
    {
        float horizontalInput = pInput.Player.Move.ReadValue<Vector2>().x;
        Vector2 movement = new Vector2(horizontalInput, 0f) * movementSpeed * Time.deltaTime;
        if (movement.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            bPlayerIsFacingRight = false;
        }
        if (movement.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            bPlayerIsFacingRight = true;
        }
        transform.Translate(movement);
    }

    private void CayoteTime()
    {
        if (!isGrounded && !pInput.Player.Jump.IsPressed()) // Fell without jumping
        {
            cayoteCounter += Time.deltaTime;
            if (cayoteCounter >= cayoteTime) // Check cayote time
                canJump = false;
        }
    }
    private void JumpBuffering()
    {
        if (!isGrounded && pInput.Player.Jump.IsPressed() && sequence != 2) // Initial Jump
            sequence = 1;
        if (!isGrounded && !pInput.Player.Jump.IsPressed() && sequence == 1) // Midair Release
            sequence = 2;
        if (!isGrounded && pInput.Player.Jump.IsPressed() && sequence == 2) // Queue a Return Jump
            jumpQueued = true;
        if (jumpQueued) // Count Queue Time
        {
            jumpBufferCounter += Time.deltaTime;
            if (jumpBufferCounter >= jumpBufferTime) // If jump queue time runs out
                jumpQueued = false;
        }
    }
    private void JumpFunction()
    {
        if (isGrounded && (!pInput.Player.Jump.IsPressed() || jumpQueued)) // Jump Check
        {
            cayoteCounter = 0f;
            jumpBufferCounter = 0f;
            sequence = 0;
            canJump = true;
        }
        if (pInput.Player.Jump.IsPressed() && canJump) // Initiate Jump
        {
            canJump = false;
            jumpQueued = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength * 3);
        }
        if (!isGrounded && !pInput.Player.Jump.IsPressed() && rb.linearVelocity.y > 0) // Jump Cancelled before climax, cut velocity and add falling force
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * 4.5f * Time.deltaTime;
        }
        if (!isGrounded && rb.linearVelocity.y < 0) // Add falling force
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * 4.5f * Time.deltaTime;
        }
    }
}