using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3; // Maximum number of hearts the player has
    public int currentHearts; // Current number of hearts the player has
    public UIManager uiManager; // Reference to the UIManager script

    private void Start()
    {
        currentHearts = maxHearts; // Initialize current hearts to maximum hearts
        UpdateUI();
    }

    // Decrease the player's hearts
    public void TakeDamage()
    {
        currentHearts--;

        // Check if the player has run out of hearts
        if (currentHearts <= 0)
        {
            // Restart the level
            RestartLevel();
        }

        UpdateUI();
    }

    public void AddHealth()
    {
        // Increase the player's hearts
        currentHearts++;

        // Clamp the current hearts to the maximum hearts
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);

        UpdateUI();
    }

    // Restart the level
    private void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update the UI with the current player health
    private void UpdateUI()
    {
        // Update the UI manager with the current health
        uiManager.UpdateHearts(currentHearts);
    }

    private void LateUpdate()
    {
        // UPDATE UI
        UpdateUI();
    }
}
