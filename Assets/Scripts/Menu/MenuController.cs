using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // public string _newGameLevel;
    // private string levelToLoad;

    public void PlayButton() {
        SceneManager.LoadScene(""); // артем, укажи тут название сцены с игрой плс
    }
   
    public void ExitButton() {
        Application.Quit();
    }
}
