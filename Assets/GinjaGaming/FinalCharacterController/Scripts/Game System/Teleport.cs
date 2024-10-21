using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform Destination;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // Get the CharacterController component
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                // Disable CharacterController to avoid collision issues during teleport
                controller.enabled = false;

                // Move the player to the destination
                other.transform.position = Destination.position;

                // Re-enable CharacterController after teleport
                controller.enabled = true;
            }
        }
    }
}
