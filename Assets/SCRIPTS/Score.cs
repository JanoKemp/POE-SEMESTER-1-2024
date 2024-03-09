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
    private int enemyScore = 0;
    private int playerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyScoreText.text = "Enemy score: " + enemyScore.ToString();
        playerScoreText.text = "Player score: " + playerScore.ToString();

    }
    private void Awake()
    {
        instance = this;
    }

    public void addPlayerScore()
    {
        playerScore += 1;
        playerScoreText.text = "Player score: " + playerScore.ToString();

    }
    public void addEnemyScore()
    {
        enemyScore += 1;
        enemyScoreText.text = "Enemy score: " + enemyScore.ToString();
    }
}
