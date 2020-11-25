using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Game : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]
    private TMP_InputField txtUserName;

    public void OnClickButtonSave()
    {
        string userName = txtUserName.text;
        HighScoreManager.AddNewHighScore(userName, Random.Range(400, 1000));
    }
}
