using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Quit : MonoBehaviour
{
    public GameObject scoreKeeper;
    Score reset;


    private void Start()
    {
       
    }
    public void Exit()
    {
        //reset.ResetValues();
        SceneManager.LoadScene("SampleScene");
       
    }
    
}
