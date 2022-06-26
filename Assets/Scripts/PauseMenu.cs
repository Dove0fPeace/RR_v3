using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsGamePaused) {
                Resume();
            } else {
                Pause();
            }
        }  
    }

    private void Resume() {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;

    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ResumeGame() {
        Resume();
    }
    public void QuitGame() {
        Application.Quit();
    }

    // public void RestartMenu() {

    // }

    public void PauseGame() {
        Pause();
    }
}
