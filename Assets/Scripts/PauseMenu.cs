using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuUI; // Reference to the pause menu UI

    private bool isPaused = false; // Whether the game is currently paused

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Toggle pause menu when the Escape key is pressed
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void ResumeGame() {
        // Hide the pause menu UI and resume game time
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame() {
        // Display the pause menu UI and pause game time
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame() {
        // Quit the game and return to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the actual scene name for your main menu
    }
}