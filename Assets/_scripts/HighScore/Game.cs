using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HighScoreManager.AddNewHighscore("Pedro", 51);
            Debug.Log("Listo");
        }
    }
}
