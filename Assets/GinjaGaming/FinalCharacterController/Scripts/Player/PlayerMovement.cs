using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputController playerActionsAsset;
    private InputAction move;

    private Rigidbody rb;

    [SerializeField]
    private float movementForce = 1f;

    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float maxSpeed = 5f;

    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private int maxJumps = 2; // Allow double jump
    private int currentJumps = 0; // Track the number of jumps

    [SerializeField]
    private Transform groundCheck; // Transform to check for ground collision
    [SerializeField]
    private float groundCheckRadius = 0.2f; // Radius for ground detection
    [SerializeField]
    private LayerMask groundLayer; // Layer representing the ground

    private Animator animator;

    [SerializeField]
    private float fallMultiplier = 2.5f; // Multiplier for faster falling

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing.");
        }

        playerActionsAsset = new PlayerInputController();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator component is missing.");
        }
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += DoJump;
        playerActionsAsset.Player.Fire.started += DoAttack;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Fire.started -= DoAttack;
        playerActionsAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        // Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jumps if grounded
        if (isGrounded)
        {
            currentJumps = 0;
        }

        // Calculate movement forces
        Vector2 input = move.ReadValue<Vector2>();
        forceDirection += input.x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += input.y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        // Modify falling behavior
        if (rb.velocity.y < 0f) // Apply fall multiplier when falling
        {
            rb.velocity += Vector3.down * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Cap horizontal velocity
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        if (isGrounded || currentJumps < maxJumps)
        {
            // Perform the jump
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset Y velocity for consistent jumps
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentJumps++;

            // Trigger jump animation if available
            if (animator != null)
            {
                animator.SetTrigger("jump");
            }
        }
    }

    private void DoAttack(InputAction.CallbackContext context)
    {
        if (animator != null)
        {
            animator.SetTrigger("attack");
        }
    }

    private void OnDrawGizmos()
    {
        // Draw ground check gizmo in the editor for debugging
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
