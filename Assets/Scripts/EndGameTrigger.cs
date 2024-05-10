using System.Collections;
using UnityEngine;
using Cinemachine;

public class EndGameTrigger : MonoBehaviour
{
    GameManager gameManager;
    public GameObject[] fireworksSpawnPositions; // Array of empty GameObjects as spawn positions
    public GameObject fireworksPrefab; // Prefab of the fireworks particle system
    public float fireworksDuration = 5f; // Duration of fireworks before transitioning to the end screen
    private InfiniteCarController infiniteCarController;
    public CinemachineVirtualCamera endCam; // Reference to the EndCam virtual camera

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        infiniteCarController = FindObjectOfType<InfiniteCarController>();
    }

    private IEnumerator StartFireworks()
    {
        // Trigger the end screen or perform any other end-game actions
        AudioManager.instance.PlaySFX("Fireworks"); // Play the fireworks sound effect
        AudioManager.instance.PlaySFX("Cheering"); // Play the Cheering sound effect

        // Spawn fireworks particle systems at each spawn position
        foreach (GameObject spawnPosition in fireworksSpawnPositions)
        {
            Instantiate(fireworksPrefab, spawnPosition.transform.position, Quaternion.identity);
            // Rotate the fireworks particle system to shoot upwards
            fireworksPrefab.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        // Wait for the fireworks duration
        yield return new WaitForSeconds(fireworksDuration);

        // Switch to the EndCam virtual camera
        endCam.Priority = 20;

        gameManager.EndGame(); // Trigger the end game
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            StartCoroutine(StartFireworks());
            infiniteCarController.enabled = false; // Disable the InfiniteCarController script
            // freeze the player's position
            infiniteCarController.rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
