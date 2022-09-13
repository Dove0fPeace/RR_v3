using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
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
