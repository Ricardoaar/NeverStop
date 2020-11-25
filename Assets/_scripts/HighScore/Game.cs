using UnityEngine;

public class Game : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HighScoreManager.AddNewHighScore("Marta", 193);
            Debug.Log("Listo");
        }
    }
}