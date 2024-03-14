using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    public InputAction moveRight;
    public InputAction moveLeft;
    public InputAction jump;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize the Input Actions
        moveRight = new InputAction(binding: "<Keyboard>/rightArrow");
        moveLeft = new InputAction(binding: "<Keyboard>/leftArrow");
        jump = new InputAction(binding: "<Keyboard>/upArrow", type: InputActionType.Button);

        // Add listeners for input actions
        moveRight.performed += ctx => moveInput.x = 1;
        moveRight.canceled += ctx => moveInput.x = 0;
        moveLeft.performed += ctx => moveInput.x = -1;
        moveLeft.canceled += ctx => moveInput.x = 0;
        jump.performed += ctx => TryJump();
    }

    void OnEnable()
    {
        moveRight.Enable();
        moveLeft.Enable();
        jump.Enable();
    }

    void OnDisable()
    {
        moveRight.Disable();
        moveLeft.Disable();
        jump.Disable();
    }

    void Update()
    {
        // Movement handled in FixedUpdate
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    void TryJump()
    {
        // Check if grounded before jumping
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // Prevent multiple jumps
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is touching the ground
        if (collision.gameObject.CompareTag("Ground")) // Make sure your ground has a "Ground" tag
        {
            isGrounded = true;
        }
    }
}
