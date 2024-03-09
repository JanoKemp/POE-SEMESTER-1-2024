using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;




public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI winner;
    public string GameWinner;

    private void Start()
    {
        GameWinner = PlayerPrefs.GetString("Winner");
        winner.text = "Winner: " + GameWinner;
    }



}
