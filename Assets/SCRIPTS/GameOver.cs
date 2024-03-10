using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;




public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI winner;


    private string GameWinner;

    private void Start()
    {
        GameWinner = PlayerPrefs.GetString("Winner");//getting value associated with key Winner and setting it to GameWinner
        winner.text = "Winner: " + GameWinner;//display winner

       
    }

  



}
