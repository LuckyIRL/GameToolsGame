using UnityEngine;

public class Hazard : MonoBehaviour
{
    // OnTriggerEnter is called when the Collider other enters the trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component attached to the player GameObject
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // If the PlayerHealth component is found
            if (playerHealth != null)
            {
                // Decrease the player's hearts
                playerHealth.TakeDamage();
                Debug.Log("Player hit by hazard! Hearts remaining: " + playerHealth.currentHearts);
            }
        }
    }
}
