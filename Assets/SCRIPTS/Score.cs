using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI enemyScoreText;
    public TextMeshProUGUI playerScoreText;
    public static Score instance;
    public int enemyScore = 0;
    public int playerScore = 0;
    public int totalPlayerScore;
    public int totalEnemyScore;


    void Start()
    {
        
        Load();//saved scores

        
        Display();//display saved scores when scene reloads through text on screen
    }
   

    private void Awake()
    {
        instance = this;
    }

    public void PlayerScore()
    {
        playerScore += 1;//add 1pt to score when player enters blue base collider
        playerScoreText.text = "Player score: " + playerScore.ToString();//display current score through test
        Winner();
        Save();//calling save method to save player scores
    }

    public void EnemyScore()
    {
        enemyScore += 1;//add 1pt to score when enemy enters red base collider
        enemyScoreText.text = "Enemy score: " + enemyScore.ToString();//display current score through test
        Winner();
        Save();//calling save method to save enemy scores
    }

   
    public void Display()
    {
        playerScoreText.text = "Player score: " + playerScore.ToString(); 
        enemyScoreText.text = "Enemy score: " + enemyScore.ToString();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("PlayerScore", playerScore); //sets the value of playerScore to the key PlayerScore
        PlayerPrefs.SetInt("EnemyScore", enemyScore); //sets the value of enemyScore to the key EnemyScore
        PlayerPrefs.Save();//using a built in method to save the key_value pair
    }

    public void Load()
    {

        if (PlayerPrefs.HasKey("PlayerScore")) //checks if there is a value stored associated with this key and returns true if there is and false if there is not
        {
            playerScore = PlayerPrefs.GetInt("PlayerScore");//if it returns true which it does in this case it will set the value associated with this key to the cariable playerScore
            totalPlayerScore = playerScore;
        }

        if (PlayerPrefs.HasKey("EnemyScore"))
        {
            enemyScore = PlayerPrefs.GetInt("EnemyScore");
            totalEnemyScore = enemyScore;
        }
    }
    private void Winner()
    {
        if (playerScore == 2)
        {
            LoadScene("Player");
        }else if(enemyScore == 2)
        {
            LoadScene("Enemy");
        }
        
    }
    void LoadScene(string winner)
    {
        SceneManager.LoadScene("GameOver");
        PlayerPrefs.SetString("Winner", winner);
        Save();
    }



    private void OnApplicationQuit()
    {
       ResetValues();
    }
    public void ResetValues()
    {
        playerScore = 0;
        enemyScore = 0;//resets all the values when application is exited
        Save();
        Display(); ;
    }
}