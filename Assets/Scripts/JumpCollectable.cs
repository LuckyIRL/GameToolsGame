using TMPro;
using UnityEngine;

public class JumpCollectible : MonoBehaviour
{
    private InfiniteCarController carController; // Reference to the InfiniteCarController script
    public GameObject spawnEffect;
    public AudioClip jumpCollectable;
    private TextMeshProUGUI jumpCountText; // Reference to the TextMeshPro UI element
    private GameManager gameManager;

    private void Start()
    {
        carController = FindObjectOfType<InfiniteCarController>(); // Find the InfiniteCarController script in the scene
        jumpCountText = GameObject.FindGameObjectWithTag("NumberOfJumps").GetComponent<TextMeshProUGUI>(); // Find the TextMeshPro UI element with the tag "NumberOfJumps"
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager script in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player has collided with the jump collectible
        {
            // Increment the remaining jumps in the InfiniteCarController script
            carController.remainingJumps++;

            // Increment the jump count in the GameManager
            gameManager.IncrementJumpCount();

            // Update the jump count text
            jumpCountText.text = carController.remainingJumps.ToString();

            // Instantiate the spawn effect
            Instantiate(spawnEffect, transform.position, Quaternion.identity);

            // Play the jump collectable sound effect
            AudioSource.PlayClipAtPoint(jumpCollectable, transform.position);

            // Destroy the jump collectible object
            Destroy(gameObject);
        }
    }
}
