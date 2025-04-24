using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerController playerAsset;

    private InputAction move;
    private InputAction run;
    private InputAction jump;

    private Vector3 forceDirection;
    private Rigidbody rb;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private float movementForce = 1f;

    [SerializeField]
    private float maxSpeed = 5f; 

    [SerializeField]
    private float jumpForce = 5f;

    private bool isGrounded = true;
    private bool wasGrounded = true;

    private characterAnimations playerAnimations;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing.");
        }
        playerAsset = new PlayerController();
        playerAnimations = GetComponentInChildren<characterAnimations>();
    }

    private void OnEnable()
    {
        move = playerAsset.PlayerControls.Move;
        run = playerAsset.PlayerControls.Run;
        jump = playerAsset.PlayerControls.Jump;

        playerAsset.PlayerControls.Enable();
        jump.performed += ctx => TryJump();
    }

    private void OnDisable()
    {
        playerAsset.PlayerControls.Disable();
        jump.performed -= ctx => TryJump();
    }

    private void FixedUpdate()
    {
        Vector2 input = move.ReadValue<Vector2>();
        bool isRunning = run.IsPressed();

        float currentMovementForce = isRunning ? movementForce * 2f : movementForce;
        forceDirection += input.x * GetCameraRight(playerCamera) * currentMovementForce;
        forceDirection += input.y * GetCameraForward(playerCamera) * currentMovementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        float currentMaxSpeed = isRunning ? maxSpeed * 2f : maxSpeed;
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.sqrMagnitude > currentMaxSpeed * currentMaxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * currentMaxSpeed + Vector3.up * rb.velocity.y;
        }

        UpdateAnimations(input, isRunning);
        LookAt();
    }

    private void UpdateAnimations(Vector2 input, bool isRunning)
    {
        if (!isGrounded)
        {
            // Si el jugador está en el aire, desactivamos BlendTree y activamos la animación de salto
            playerAnimations?.SetJumping(true);
            playerAnimations?.move(0);  // Para que el personaje no haga movimientos de caminata o carrera
            return;
        }

        // En el suelo, desactivamos la animación de salto y activamos el BlendTree
        playerAnimations?.SetJumping(false);

        float speed = (input.sqrMagnitude > 0.1f) ? (isRunning ? 1f : 0.5f) : 0f;
        playerAnimations?.move(speed);
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

    public void SnapTo(Vector3 targetPosition, Quaternion targetRotation)
    {
        rb.position = targetPosition;
        rb.rotation = targetRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (playerAnimations != null)
        {
            playerAnimations.move(0);
        }
    }

    private void TryJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // El personaje pasa al estado aéreo
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true; // El personaje vuelve a estar en el suelo
        }
    }
}
