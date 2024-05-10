using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] heartIcons; // Array to store references to the heart icon images
    public Sprite fullHeartSprite; // Sprite for a full heart
    public Sprite emptyHeartSprite; // Sprite for an empty heart
    public Slider musicSlider, sfxSlider; // Reference to the music and SFX volume sliders


    // Initialize the UI manager
    public void Init()
    {
        // Set the initial heart icons based on the player's starting health
    }

    // Update the heart icons based on the player's current health
    public void UpdateHearts(int currentHealth)
    {
        // Iterate through each heart icon
        for (int i = 0; i < heartIcons.Length; i++)
        {
            // Check if the index is less than the current health
            if (i < currentHealth)
            {
                // Set the sprite to represent a full heart
                heartIcons[i].sprite = fullHeartSprite;
            }
            else
            {
                // Set the sprite to represent an empty heart
                heartIcons[i].sprite = emptyHeartSprite;
            }
        }
    }


    public void ToggleMusic()
    {
        // Toggle the music volume based on the music slider value
        AudioManager.instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        // Toggle the SFX volume based on the SFX slider value
        AudioManager.instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        // Update the music volume based on the music slider value
        AudioManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        // Update the SFX volume based on the SFX slider value
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }


    // Restart the game when the restart button is clicked
    public void RestartGame()
    {
        // Call the RestartGame method in the GameManager instance
        GameManager.instance.RestartGame();
    }

    // Pause the game when the pause button is clicked
    public void PauseGame()
    {
        // Call the PauseGame method in the GameManager instance
        GameManager.instance.PauseGame();
    }

    // Update Level UI
    public void UpdateLevel(int level)
    {
        // Update the level text with the specified level
    }

    public void UpdateTokenCount(int token)
    {
        // Update the token count text with the current token count

    }

    public void UpdateScore(int score)
    {
        // Update the score text with the current score
    }

    public void UpdateHighScore(int highscore)
    {
        // Update the high score text with the current high score
    }

    public void TogglePauseMenu(bool isPaused)
    {
        // Set the pause menu active state based on the isPaused parameter
    }


}
