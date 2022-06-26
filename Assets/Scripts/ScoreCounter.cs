using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;
    private int _money = 0;
    private int _score = 0;
    private int _highScore = 0;

    public Transform EnemyActivePool;
   

    public void UpdateScore() {

    }
    public void IncreaseScore(int Combo) {
        _score+= Combo;
        _scoreText.text = $"{_score}";
    }

    public void IncreaseMoney(){
        _money += Random.Range(0,6);
    }
    public void SaveHighScore() {
        _highScore = _score;
        if (PlayerPrefs.GetInt("HighScore") < _highScore) 
        PlayerPrefs.SetInt("HighScore", _highScore);
        _highScoreText.text = $"Best: {PlayerPrefs.GetInt("HighScore")} \n Money: {_money}";
        //PlayerPrefs.DeleteAll();
        //
    }
    // Update is called once per frame
    // void Update(){
        
    //     //Debug.Log($"Money: {_money}");
    //     // if (EnemyActivePool.childCount > 1) {
    //     //     if (EnemyActivePool.childCount == 2) {
    //     //         if (Mathf.Abs(EnemyActivePool.GetChild(0).transform.position.x) < 0.2f & 
    //     //             Mathf.Abs(EnemyActivePool.GetChild(1).transform.position.x) < 0.2f) {
    //     //                 Debug.Log("sas");
    //     //             }
    //     //     }
    //     //     else if(EnemyActivePool.childCount == 3) {
    //     //         if (Mathf.Abs(EnemyActivePool.GetChild(0).transform.position.x) < 0.2f & 
    //     //             Mathf.Abs(EnemyActivePool.GetChild(1).transform.position.x) < 0.2f &
    //     //             Mathf.Abs(EnemyActivePool.GetChild(2).transform.position.x) < 0.2f) {
                        
    //     //             }
    //     //     }
    //         // foreach(Transform Enemy in EnemyActivePool) {
    //         // Debug.Log(Enemy.position.x);
    //     }
}
