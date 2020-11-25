using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HighScoreReader : MonoBehaviour
{
    public TMP_InputField inputField;
    private Score _currentScore;
    private bool _readValue;
    private TextInfo _textInfo;
    public List<String> forbiddenWords = new List<string>();
    public UnityEvent onNewMaxScore;


    public List<GameObject> showInMaxScore = new List<GameObject>();

    private void Awake()
    {
        _textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
        var lowerList = forbiddenWords.Select(word => word.ToLower()).ToList();
        forbiddenWords = lowerList;
        _currentScore = new Score();
        SetObjScoreEnable(false);
    }

    private bool CheckScore()
    {
        var scoreList = DisplayHighScores.GetScoreList();
        scoreList.Sort();
        var playerScore = PlayerStats.SingleInstance.GetCurrentScore();

        for (var i = 0; i < 3; i++)
        {
            _currentScore.Value = playerScore;
            Debug.Log(playerScore + " : " + scoreList[i]);
            if (!(playerScore > scoreList[i])) continue;
//Muestra esta ui en caso de que haya nuevo max score
            SetObjScoreEnable(true);
            return true;
        }

        return false;
    }

    public void OnGameOver()
    {
        Debug.Log("VAR");
        if (CheckScore())
        {
            StartCoroutine(ReadNewHighScore());
        }
    }

    public void ReadValue()
    {
        if (forbiddenWords.Contains(inputField.text.ToLower()) || inputField.text.Length == 0)
            _currentScore.UserName = "******";

        else
            _currentScore.UserName = _textInfo.ToTitleCase(inputField.text).Trim();

        _readValue = true;
    }

    private IEnumerator ReadNewHighScore()
    {
        yield return new WaitUntil(() => _readValue);
        Debug.Log(_currentScore.UserName + " " + _currentScore.Value);
        HighScoreManager.AddNewHighScore(_currentScore.UserName, _currentScore.Value * 100);
        onNewMaxScore.Invoke();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void SetObjScoreEnable(bool active)
    {
        foreach (var obj in showInMaxScore)
        {
            obj.SetActive(active);
        }
    }
}