using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
   
    GameManager gameManager;
    public AudioClip endGameSound1; // Reference to the end game sound AudioClip
    public AudioClip endGameSound2;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            // Trigger the end screen or perform any other end-game actions
            GameManager.instance.EndGame();
            AudioSource.PlayClipAtPoint(endGameSound1, transform.position); // Play the end game sound effect
            AudioSource.PlayClipAtPoint(endGameSound2, transform.position);
        }
    }
}
