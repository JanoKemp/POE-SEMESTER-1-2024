using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    Score reset;
   public void Exit()
    {
        reset.ResetValues();

        Application.Quit();
    }
}
