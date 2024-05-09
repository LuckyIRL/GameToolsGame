using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    // Reference to the enemy car
    public GameObject enemyCar;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Activate the movement on the enemy car
            if (enemyCar != null)
            {
                EnemyAI enemyMovement = enemyCar.GetComponent<EnemyAI>();
                if (enemyMovement != null)
                {
                    enemyMovement.enabled = true;
                }
            }
        }
    }
}
