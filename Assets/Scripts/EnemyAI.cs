using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f; // Range within which the enemy can detect the player
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player
    public float trailingDistance = 3f; // Distance behind the player to trail
    public float maxTurnAngle = 45f; // Maximum angle the enemy can turn in degrees
    public float boostSpeedMultiplier = 2f; // Speed multiplier when boosting
    public float boostCooldown = 5f; // Cooldown period between boosts
    public float bumpForce = 10f; // Force applied to the player upon collision

    public Transform player; // Reference to the player's transform

    private bool playerDetected = false;
    private bool isBoosting = false;

    void Update()
    {
        // Check if the player is within the detection range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        // If the player is detected, move towards a position behind the player
        if (playerDetected)
        {
            // Check if not currently boosting and a random chance to enter boost mode
            if (!isBoosting && Random.value < 0.1f)
            {
                StartBoost();
            }

            // Calculate a position behind the player
            Vector3 targetPosition = player.position - player.forward * trailingDistance;

            // Calculate direction to the target position
            Vector3 directionToTarget = (targetPosition - transform.position).normalized;

            // Calculate angle to rotate towards the target position
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxTurnAngle * Time.deltaTime);

            // Move at normal or boosted speed depending on the boost state
            float speed = isBoosting ? moveSpeed * boostSpeedMultiplier : moveSpeed;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void StartBoost()
    {
        // Set boost state to true
        isBoosting = true;

        // Set boost duration
        Invoke("EndBoost", boostCooldown);

        // Reset speed to normal after boost duration
        Invoke("ResetSpeed", boostCooldown + 2f);
    }

    void EndBoost()
    {
        // Reset boost state after cooldown period
        isBoosting = false;
    }

    void ResetSpeed()
    {
        // Reset speed to normal after boost duration
        moveSpeed = 5f;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player and the enemy is currently boosting
        if (collision.gameObject.CompareTag("Player") && isBoosting)
        {
            // Apply a force to the player to simulate the bump
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 bumpDirection = (collision.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(bumpDirection * bumpForce, ForceMode.Impulse);
            }
        }
    }
}
