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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment the end token count
            levelLayoutGenerator.endTokenCount++;

            // Update the end token count text
            endTokenCountText.text = levelLayoutGenerator.endTokenCount.ToString();

            // Play the collect effect and sound
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Check if the required number of EndTokens has been collected
            if (levelLayoutGenerator.endTokenCount >= requiredEndTokenCount)
            {
                // Spawn the EndPrefab at the appropriate position
                levelLayoutGenerator.SpawnEndPrefab(endChunkPrefab);
            }

            // Destroy the end token
            Destroy(gameObject);
        }
    }
}
