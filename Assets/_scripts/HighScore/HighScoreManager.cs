using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public static void AddNewHighScore(string username, float score)
    {
        _instance.StartCoroutine(_instance.AddScoreCoroutine(
            _instance.ClearText(username), score));
    }

    IEnumerator AddScoreCoroutine(string userName, float score)
    {
        WWW webRequest = new WWW(
            $"{_webURL}{_privateCode}/pipe-get/{WWW.EscapeURL(userName)}");
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            //Consultar si el userName está registrado
            if (!string.IsNullOrEmpty(webRequest.text))
            {
                FormatHighScores(webRequest.text);
                Score selectScore = _listScore.FirstOrDefault(
                    x => x.UserName.Equals(userName));
                if (score > selectScore.Value)
                {
                    //Debe eliminar el userName actual y agregar el nuevo
                    StartCoroutine(DeleteUserNameCoroutine(userName));
                    StartCoroutine(AddNewUserName(userName, score));
                }
            }
            else
                StartCoroutine(AddNewUserName(userName, score));
        }
    }

    //Corutina para agregar un nuevo userName
    IEnumerator AddNewUserName(string userName, double score)
    {
        WWW webRequest = new WWW(
            $"{_webURL}{_privateCode}/add/{WWW.EscapeURL(userName)}/{score}");
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            print($"{userName} upload successful");
            ReadScore();
        }
        else
            print("Error uploading: " + webRequest.error);
    }

    //Corutina para eliminar un userName
    IEnumerator DeleteUserNameCoroutine(string userName)
    {
        WWW webRequest = new WWW($"{_webURL}{_privateCode}/delete/");
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            Debug.Log($"{userName} deleted!");
            ReadScore();
        }
        else
        {
            Debug.LogError($"Error deleting: {webRequest.error}");
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
            _displayHighScores.OnHighScoresDownloaded(_listScore);
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
        
        //Ordenar la lista por puntaje
        _listScore.OrderBy(x => x.Value);
    }

    private string ClearText(string userName)
    {
        return userName
            .Replace("|", "")
            .Replace("@", "")
            .Replace(",", "")
            .Replace("=", "");
    }
}