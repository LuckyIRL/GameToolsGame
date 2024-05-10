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
    public int endTokenCountNeeded = 1; // Number of end tokens needed to spawn the end chunk
    public bool endChunkSpawned = false; // Flag to track if the end chunk has been spawned
    public int remainingJumps = 0;

    // Awake is always called before any Start functions
    public void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
            levelLayoutGenerator = FindObjectOfType<LevelLayoutGenerator>();
            // Check if levelLayoutGenerator is null and handle it if needed
        }
    }

    public void Update()
    {
        // Check if the player has collected all end tokens
        CheckEndTokens();

        // Check if all end tokens have been collected and the end chunk hasn't been spawned yet
        if (hasAllEndTokens && !endChunkSpawned)
        {
            // Spawn the end chunk
            levelLayoutGenerator.SpawnEndChunk();
            // Set the flag to true
            endChunkSpawned = true;
        }
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

        // freeze the game when paused
        Time.timeScale = isPaused ? 0 : 1;

    }

    // Resume the game
    public void ResumeGame()
    {
        // Set the pause state to false
        isPaused = false;

        // Unfreeze the game when resumed
        Time.timeScale = 1;

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
        if (endTokenCount >= endTokenCountNeeded)
        {
            // Set the flag to true
            hasAllEndTokens = true;
        }
    }

    // Increment the Jump count
    public void IncrementJumpCount()
    {
        // Increment the jump count
        remainingJumps++;
    }

    // End the game
    public void EndGame()
    {
        // Set the game over flag to true
        isGameOver = true;

        // Show the end screen or perform any other end-game actions
        SceneManager.LoadScene(2); // Load the end screen scene
    }

    // add health when player collects health pickup
    public void AddHealth()
    {
        // Check if the player's health is less than the maximum health
        if (currentHealth < maxHealth)
        {
            // Increment the player's health
            currentHealth++;
        }
    }



}



