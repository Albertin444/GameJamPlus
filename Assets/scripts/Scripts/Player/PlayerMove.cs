using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerController playerAsset;

    private InputAction move;
    private InputAction run;

    private Vector3 forceDirection;

    private Rigidbody rb;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private float movementForce = 1f;

    [SerializeField]
    private float maxSpeed = 5f;



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
        run = playerAsset.PlayerControls.Run; // Asigna la acción de correr del Input System
        playerAsset.PlayerControls.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 input = move.ReadValue<Vector2>();

        // Verifica si el jugador está corriendo
        bool isRunning = run.IsPressed();

        // Calcula la dirección de movimiento basada en la cámara
        float currentMovementForce = isRunning ? movementForce * 2f : movementForce; // Duplica la fuerza si corre
        forceDirection += input.x * GetCameraRight(playerCamera) * currentMovementForce;
        forceDirection += input.y * GetCameraForward(playerCamera) * currentMovementForce;

        // Agrega fuerza al Rigidbody
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        // Limita la velocidad horizontal
        float currentMaxSpeed = isRunning ? maxSpeed * 2f : maxSpeed; // Duplica la velocidad máxima si corre
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.sqrMagnitude > currentMaxSpeed * currentMaxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * currentMaxSpeed + Vector3.up * rb.velocity.y;
        }

        // Actualiza las animaciones y la rotación
        UpdateAnimations(input, isRunning);
        LookAt();
    }

    private void UpdateAnimations(Vector2 input, bool isRunning)
    {
        float speed = (input.sqrMagnitude > 0.1f) ? (isRunning ? 1f : 0.5f) : 0f;

        if (playerAnimations != null)
        {
            playerAnimations.move(speed); // Correct method call
        }

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

    // Method to snap the player's body to a specific position and/or rotation
    public void SnapTo(Vector3 targetPosition, Quaternion targetRotation)
    {
        rb.position = targetPosition;
        rb.rotation = targetRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (playerAnimations != null)
        {
            playerAnimations.move(0); // Use the PlayerAnimations method
        }
    }
}
