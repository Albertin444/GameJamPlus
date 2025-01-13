using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public CharacterController controller; // Use CharacterController instead of Rigidbody

    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Vector3 moveDirection;

    private void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing) ClimbingMovement();
    }

    private void StateMachine()
    {
        // State 1 - Climbing
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            if (!climbing && climbTimer > 0)
                StartClimbing();

            // Timer
            if (climbTimer > 0)
                climbTimer -= Time.deltaTime;

            if (climbTimer < 0)
                StopClimbing();
        }
        else
        {
            if (climbing)
                StopClimbing();
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        if (controller.isGrounded) // Use CharacterController's isGrounded
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        // Camera FOV change (optional)
    }

    private void ClimbingMovement()
    {
        moveDirection = new Vector3(0, climbSpeed, 0);
        controller.Move(moveDirection * Time.deltaTime); // Apply movement
    }

    private void StopClimbing()
    {
        climbing = false;
        // Particle effect (optional)
    }
}
