using System.Collections;
using UnityEngine;

public class SpeedBoostCollectable : MonoBehaviour
{
    public float boostDuration = 5f; // Duration of the speed boost in seconds
    public float boostMultiplier = 2f; // Multiplier for the speed boost

    // OnTriggerEnter is called when the Collider other enters the trigger. Checks if the player has collided with the speed boost collectable
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Get the InfiniteCarController component from the player
            InfiniteCarController carController = other.GetComponent<InfiniteCarController>();

            // Check if the InfiniteCarController component is not null
            if (carController != null)
            {
                // Apply the speed boost to the player
                StartCoroutine(ApplySpeedBoost(carController));

                // Play the speed boost sound effect
                AudioManager.instance.PlaySFX("SpeedBoost Pickup");

                // Destroy the speed boost collectable
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ApplySpeedBoost(InfiniteCarController carController)
    {
        // Increase the move speed temporarily
        carController.moveSpeed *= boostMultiplier;

        // Wait for the duration of the speed boost
        yield return new WaitForSeconds(boostDuration);

        // Restore the original move speed
        carController.moveSpeed /= boostMultiplier;
    }
}
