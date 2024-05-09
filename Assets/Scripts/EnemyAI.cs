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
    public float destroyDelay = 10f; // Time before the enemy self-destructs after losing sight of the player

    public Transform player; // Reference to the player's transform

    private bool playerDetected = false;
    private bool isBoosting = false;
    private bool shouldDestroy = false;
    private float timeSinceLostPlayer = 0f;

    void Update()
    {
        // Check if the player is within the detection range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
            timeSinceLostPlayer = 0f; // Reset the timer if player is detected
        }
        else
        {
            playerDetected = false;
            timeSinceLostPlayer += Time.deltaTime; // Increment the timer if player is not detected
            if (timeSinceLostPlayer >= destroyDelay)
            {
                shouldDestroy = true; // Set flag to destroy enemy after delay
            }
        }

        // If the player is detected, move towards a position behind the player
        if (playerDetected)
        {
            // Reset the destroy timer if player is detected
            timeSinceLostPlayer = 0f;

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
        else if (shouldDestroy)
        {
            Destroy(gameObject); // Destroy the enemy if it should be destroyed
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
