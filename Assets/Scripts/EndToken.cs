using System.Collections;
using TMPro;
using UnityEngine;

public class EndToken : MonoBehaviour
{
    public LevelLayoutGenerator levelLayoutGenerator; // Reference to the LevelLayoutGenerator script
    public TextMeshProUGUI endTokenCountText; // Reference to the TextMeshPro UI element

    public GameObject collectEffect; // Reference to the collect effect GameObject
    public AudioClip collectSound; // Reference to the collect sound AudioClip

    public GameObject endChunkPrefab; // Reference to the EndPrefab GameObject
    public int requiredEndTokenCount = 10; // Number of EndTokens required to spawn the EndPrefab

    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with the EndToken
        if (other.CompareTag("Player"))
        {
            // Increment the end token count
            gameManager.endTokenCount++;

            // Update the end token count text
            endTokenCountText.text = gameManager.endTokenCount.ToString();

            // Play the collect effect
            Instantiate(collectEffect, transform.position, Quaternion.identity);

            // Play the collect sound
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // Destroy the EndToken
            Destroy(gameObject);
            // wait for 5 second
        }
    }

    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
