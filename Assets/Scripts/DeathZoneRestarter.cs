using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneRestarter : MonoBehaviour
{  
    [SerializeField] private GameObject _scoreCounter;
    private ScoreCounter _scoreCounterScript;
    public GameObject restartMenuUI;
    public GameObject pauseButton;

    private void Awake() {
        _scoreCounterScript = _scoreCounter.GetComponent<ScoreCounter>();
    }
   private void OnTriggerEnter2D(Collider2D other) {
       restartMenuUI.SetActive(true);
       pauseButton.SetActive(false);
       Time.timeScale = 0f;
       _scoreCounterScript.SaveHighScore();
      // Debug.Log();

       
   }

   public void RestartGame() {
       SceneManager.LoadScene(0);
        Time.timeScale = 1f;
   }
}
