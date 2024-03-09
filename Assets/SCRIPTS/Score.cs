using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI enemyScoreText;
    public TextMeshProUGUI playerScoreText;
    public static Score instance;
    private int enemyCurrentScore = 0;
    private int playerCurrentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadUpdatedScores();
        ScoreDisplay();

    }
    private void Awake()
    {
        instance = this;
    }

    public void addPlayerScore()
    {
        playerCurrentScore += 1;
        playerScoreText.text = "Player score: " + playerCurrentScore.ToString();
        Save();

    }
    public void addEnemyScore()
    {
        enemyCurrentScore += 1;
        enemyScoreText.text = "Enemy score: " + enemyCurrentScore.ToString();
        Save();
    }
    private void LoadUpdatedScores()
    {
        playerCurrentScore = PlayerPrefs.GetInt("Player score", 0);
        enemyCurrentScore = PlayerPrefs.GetInt("Enemy Scores", 0);
    }
    public void Save()
    {
        PlayerPrefs.SetInt("PlayerScore", playerCurrentScore);
        PlayerPrefs.SetInt("EnemyScore", enemyCurrentScore);
        PlayerPrefs.Save();
    }
    private void ScoreDisplay()
    {
        playerScoreText.text = "Player score: " + playerCurrentScore.ToString();
        enemyScoreText.text = "Enemy score: " + enemyCurrentScore.ToString();

    }
}
