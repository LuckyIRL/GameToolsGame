using System.Collections;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    GameManager gameManager;
    public AudioClip endGameSound1; // Reference to the end game sound AudioClip
    public AudioClip endGameSound2;
    public GameObject[] fireworksSpawnPositions; // Array of empty GameObjects as spawn positions
    public GameObject fireworksPrefab; // Prefab of the fireworks particle system
    public float fireworksDuration = 5f; // Duration of fireworks before transitioning to the end screen
    private InfiniteCarController infiniteCarController;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        infiniteCarController = FindObjectOfType<InfiniteCarController>();
    }

    private IEnumerator StartFireworks()
    {
        // Trigger the end screen or perform any other end-game actions
        GameManager.instance.EndGame();
        AudioSource.PlayClipAtPoint(endGameSound1, transform.position); // Play the end game sound effect
        AudioSource.PlayClipAtPoint(endGameSound2, transform.position);

        // Spawn fireworks particle systems at each spawn position
        foreach (GameObject spawnPosition in fireworksSpawnPositions)
        {
            Instantiate(fireworksPrefab, spawnPosition.transform.position, Quaternion.identity);
        }

        // Wait for the fireworks duration
        yield return new WaitForSeconds(fireworksDuration);

        // Disable player control
        InfiniteCarController[] playerControllers = FindObjectsOfType<InfiniteCarController>();
        foreach (InfiniteCarController playerController in playerControllers)
        {
            playerController.SetCanControl(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            StartCoroutine(StartFireworks());
        }
    }
}
