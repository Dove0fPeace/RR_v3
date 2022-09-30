using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour, IDataPersistence
{
    // public string _newGameLevel;
    // private string levelToLoad;
    [SerializeField] private TMP_Text _highScoreText;

    public void PlayButton() 
    {
        DataPersistenceManager.Instance.LoadGame();
        SceneManager.LoadScene(1);
    }
   
    public void ExitButton() 
    {
        Application.Quit();
    }

    public void LoadData(GameData gameData)
    {
        _highScoreText.text = gameData.HighScore.ToString();
    }

    public void SaveData(ref GameData gameData)
    {

    }
}
