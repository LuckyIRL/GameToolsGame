using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// GameManager class

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; // Static instance of GameManager which allows it to be accessed by any other script
    private UIManager uiManager; // Reference to the UIManager script
    private LevelLayoutGenerator levelLayoutGenerator; // Reference to the LevelLayoutGenerator script
    private InfiniteCarController player; // Reference to the InfiniteCarController script
    public int currentLevel = 1; // Current level of the game
    public int maxLevel = 3; // Maximum level of the game
    public int currentHealth = 3; // Current health of the player
    public int maxHealth = 3; // Maximum health of the player
    public bool isGameOver = false; // Flag to track if the game is over
    public bool isPaused = false; // Flag to track if the game is paused
    public int endTokenCount = 0; // Number of end tokens collected by the player
    public bool hasAllEndTokens = false; // Flag to track if the player has collected all end tokens

    // Awake is always called before any Start functions
    public void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        // Initialize the game
        // InitGame();
    }

    // Initialize the game
    public void InitGame()
    {
        // Initialize the UI manager
        // uiManager.Init();

        // Update the UI to show the current level
        // uiManager.UpdateLevel(currentLevel);
    }

    // Restart the game
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Pause the game
    public void PauseGame()
    {
        // Toggle the pause state
        isPaused = !isPaused;

        // Show or hide the pause menu based on the pause state
        uiManager.TogglePauseMenu(isPaused);
    }

    // Start Menu Button
    public void StartGame()
    {
        // Load the first level
        SceneManager.LoadScene(1);
    }

    // Check if the player has collected all end tokens
    public void CheckEndTokens()
    {
        // Check if the player has collected all end tokens
        if (endTokenCount >= 1)
        {
            hasAllEndTokens = true;

            // Spawn the end chunk
            levelLayoutGenerator.SpawnEndPrefab(gameObject);
        }
    }
    
}



