using UnityEngine;
using UnityEngine.UI;

public class JumpCollectible : MonoBehaviour
{
    private int remainingJumps; // Remaining number of jumps the car can perform
    public GameObject jumpIcon; // Reference to the jump icon GameObject
    public InfiniteCarController carController; // Reference to the InfiniteCarController script
    public GameObject spawnEffect;
    public AudioClip jumpCollectable;

    private void Start()
    {
        carController = FindObjectOfType<InfiniteCarController>(); // Find the InfiniteCarController script in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player has collided with the jump collectible
        {
            remainingJumps++; // Increment the remaining jumps
            carController.remainingJumps++; // Increment the remaining jumps in the InfiniteCarController script
            gameObject.SetActive(false); // Deactivate the jump collectible
            Instantiate(spawnEffect, transform.position, Quaternion.identity); // Instantiate the spawn effect
            AudioSource.PlayClipAtPoint(jumpCollectable, transform.position); // Play the jump collectable sound effect
            jumpIcon.SetActive(true); // Activate the jump icon

            Debug.Log("Jump Collected! Remaining Jumps: " + remainingJumps);

            Destroy(gameObject); // Destroy the jump collectible object
        }
    }


}