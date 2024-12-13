using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float maxSpeed = 5f;

    private PlayerInputController playerActionsAsset; // Reference to input actions asset
    private bool isRunning = false; // Track running state

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerActionsAsset = new PlayerInputController();
    }

    private void OnEnable()
    {
        // Bind Run input action
        playerActionsAsset.Player.Run.started += OnRunStarted;
        playerActionsAsset.Player.Run.canceled += OnRunCanceled;

        // Enable input actions
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        // Unbind Run input action
        playerActionsAsset.Player.Run.started -= OnRunStarted;
        playerActionsAsset.Player.Run.canceled -= OnRunCanceled;

        // Disable input actions
        playerActionsAsset.Player.Disable();
    }

    private void Update()
    {
        // Calculate and set speed for blend tree
        float normalizedSpeed = rb.velocity.magnitude / maxSpeed;

        if (isRunning)
        {
            animator.SetFloat("speed", normalizedSpeed); // Full range for running
        }
        else
        {
            animator.SetFloat("speed", Mathf.Clamp(normalizedSpeed, 0f, 0.5f)); // Clamped for walking
        }
    }

    private void OnRunStarted(InputAction.CallbackContext context)
    {
        isRunning = true; // Player starts running
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        isRunning = false; // Player stops running
    }
}
