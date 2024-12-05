using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;

    private Vector2 moveInput;
    private bool isJumping;
    private bool isDashing;
    private bool isGrounded;
    public SPUM_Prefabs anim;
    private bool facingRight = true;


    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    public float moveSpeed = 5f; // Walking speed
    public float jumpForce = 10f; // Jumping force
    public float dashSpeed = 20f; // Speed during dash
    public float dashDuration = 0.2f; // How long the dash lasts
    public float dashCooldown = 0.5f; // Cooldown between dashes
    private float dashTime; // Time remaining for dash
    private Vector2 dashDirection; // Direction of dash
    private bool canDash = true; // Whether the player can dash

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        var gameplayActions = inputActions.FindActionMap("Player");
        moveAction = gameplayActions.FindAction("Move");
        jumpAction = gameplayActions.FindAction("Jump");
        dashAction = gameplayActions.FindAction("Dash");
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        dashAction.Enable();

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
        dashAction.performed += OnDash;
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        dashAction.Disable();

        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;
        dashAction.performed -= OnDash;
    }

    void OnMove(InputAction.CallbackContext context)
    {
         moveInput = context.ReadValue<Vector2>();
        // Play animation when there's movement input
        
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            isJumping = context.ReadValueAsButton();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnDash(InputAction.CallbackContext context)
    {
        if (canDash)  // Dash only if allowed (no cooldown and not dashing already)
        {
            canDash = false;
            isDashing = true;
            dashTime = dashDuration;

            // Determine dash direction based on input; if no input, dash in the last direction
            dashDirection = moveInput.normalized;

            // If no directional input, default to dashing right
            if (dashDirection == Vector2.zero)
            {
                dashDirection = Vector2.right;
            }

            // Apply dash velocity instantly
            rb.velocity = dashDirection * dashSpeed;
        }
    }

    void Update()
    {
        
        // Handle regular horizontal movement only if not dashing
        if (!isDashing)
        {
            Vector2 move = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
            rb.velocity = move;  // Apply regular movement using rigidbody's velocity

            // Play animation only if the player is moving
            if (moveInput.x != 0)
            {
                anim.PlayAnimation(1);
                // Flip the player based on movement direction
                if (moveInput.x > 0 && !facingRight)
                {
                    Flip();
                }
                else if (moveInput.x < 0 && facingRight)
                {
                    Flip();
                }
            }
            else
            {
                anim.PlayAnimation(0); // Stop or reset animation when no movement
            }
        }

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);

        // Reset jump state if grounded again
        if (isGrounded)
        {
            isJumping = false;
        }

        // Handle dash
        if (isDashing)
        {
            dashTime -= Time.deltaTime;

            // End dash when time is up
            if (dashTime <= 0)
            {
                isDashing = false;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);  // Reset velocity after dash

                // Start cooldown before next dash
                Invoke(nameof(ResetDash), dashCooldown);
            }
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight; // Toggle the facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the player by inverting the x scale
        transform.localScale = scale;
    }

    void ResetDash()
    {
        canDash = true;  // Allow the player to dash again after cooldown
    }
}
