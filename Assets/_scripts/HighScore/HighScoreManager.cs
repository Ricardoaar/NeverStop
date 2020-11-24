using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HighScoreManager : MonoBehaviour
{
    private const string _privateCode = "tE9Lqpepy06QUg4-g7nhwwMAsmbuDQwEOvgaSt4hyB9w";
    private const string _publicCode = "5fbc3e8deb36fd2714e51817";
    private const string _webURL = "http://dreamlo.com/lb/";
    private static HighScoreManager _instance;
    private List<Score> _listScore;
    private DisplayHighScores _displayHighScores;

    private void Awake()
    {
        _displayHighScores = GetComponent<DisplayHighScores>();
        _instance = this;
    }
    
    // Añadir nuevo Score
    public static void AddNewHighscore(string username, int score) {
        _instance.StartCoroutine(_instance.AddScoreCoroutine(username,score));
    }
    
    IEnumerator AddScoreCoroutine(string username, int score) {
        WWW www = new WWW(_webURL + _privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error)) {
            print ("Upload Successful");
            ReadScore();
        }
        else {
            print ("Error uploading: " + www.error);
        }
    }
    
    //Leer los scores almacenados en el programa.
    public void ReadScore()
    {
        StartCoroutine("ReadScoreCoroutine");
    }

    private IEnumerator ReadScoreCoroutine()
    {
        WWW webRequest = new WWW($"{_webURL}{_publicCode}/pipe/");
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            FormatHighScores(webRequest.text);
            _displayHighScores.OnHighScoresDownloades(_listScore);
        }
        else
            Debug.LogError($"Error downloading: {webRequest.error}");
    }

    //Formatear el texto separado por pipes "|"
    private void FormatHighScores(string textStream)
    {
        _listScore = new List<Score>();
        string[] entries = textStream.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
        foreach (var t in entries)
        {
            string[] entryInfo = t.Split('|');
            _listScore.Add(new Score
            {
                UserName = entryInfo[0],
                Value = float.Parse(entryInfo[1])
            });
        }
    }
}
