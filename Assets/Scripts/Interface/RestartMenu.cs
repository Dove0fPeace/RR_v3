using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartMenu : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject targetGUI;
    [SerializeField] private ScoreCounter scoreCounter;

    [SerializeField] private TMP_Text targetText;
    private int highScore;

    public void ShowRestartMenu()
    {
        if (highScore < scoreCounter.Score)
        {
            highScore = scoreCounter.Score;
        }
        targetText.text = "Best: " + highScore.ToString();
        targetGUI.SetActive(true);
        DataPersistenceManager.Instance.SaveGame();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadData(GameData gameData)
    {
        this.highScore = gameData.HighScore;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.HighScore = this.highScore;
    }
}
