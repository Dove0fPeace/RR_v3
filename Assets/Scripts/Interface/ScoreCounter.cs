using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;

    private int _score;
    public int Score => _score;

    private void Start()
    {
        _score = 0;
        Player.Instance.EnemyKilled += UpdateScore;
    }

    private void OnDestroy()
    {
        Player.Instance.EnemyKilled -= UpdateScore;
    }
    private void UpdateScore()
    {
        _score++;
        m_Text.text = "Score " + _score.ToString();
    }
}
