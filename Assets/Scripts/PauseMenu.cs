using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool IsGamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }  
    }
    public void PauseGame()
    {
        Pause();
    }

    public void ResumeGame()
    {
        Resume();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    private void Resume() 
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
}
