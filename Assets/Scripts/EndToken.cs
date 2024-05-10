using System.Collections;
using TMPro;
using UnityEngine;

public class EndToken : MonoBehaviour
{
    private LevelLayoutGenerator levelLayoutGenerator; // Reference to the LevelLayoutGenerator script
    private TextMeshProUGUI endTokenCountText; // Reference to the TextMeshPro UI element

    public GameObject collectEffect; // Reference to the collect effect GameObject

    private GameManager gameManager;

    private void Start()
    {
        // Find the LevelLayoutGenerator script in the scene
        levelLayoutGenerator = FindObjectOfType<LevelLayoutGenerator>();

        // Find the TextMeshPro UI element with the tag "EndTokenCount"
        endTokenCountText = GameObject.FindGameObjectWithTag("EndTokenCount").GetComponent<TextMeshProUGUI>();

        // Find the GameManager script in the scene
        gameManager = FindObjectOfType<GameManager>();
    }


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
            AudioManager.instance.PlaySFX("EndToken Pickup");

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
