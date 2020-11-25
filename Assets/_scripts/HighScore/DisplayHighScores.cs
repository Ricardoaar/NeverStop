using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> highScoreFields;
    private HighScoreManager _scoreManager;
    private static List<float> _scores = new List<float>();
    private static Dictionary<string, float> _scoresString = new Dictionary<string, float>();
    private Coroutine _cScores;

    private void Start()
    {
        int i = 0;
        foreach (TextMeshProUGUI scoreField in highScoreFields)
        {
            i++;
            scoreField.text = $"{i}. Cargando...";
        }

        _scoreManager = GetComponent<HighScoreManager>();

        // Refresca el puntaje cada 30 segundos
        _cScores = StartCoroutine("RefreshHighScores");
    }

    public void OnHighScoresDownloaded(List<Score> listScores)
    {
        var currentScores = new List<float>();
        var currentDict = listScores.ToDictionary(score => score.UserName, score => score.Value);

        int i = 0;
        foreach (TextMeshProUGUI scoreField in highScoreFields)
        {
            scoreField.text = $"{i + 1}. ";
            if (i < listScores.Count)
            {
                scoreField.text += $"{listScores[i].UserName} - {listScores[i].Value / 100}";
                currentScores.Add(listScores[i].Value);
            }

            i++;
        }

        _scoresString = currentDict;
    }

    public static List<float> GetScoreList()
    {
        return _scores;
    }

    public static Dictionary<string, float> GetScoreDict()
    {
        return _scoresString;
    }


    public void NewMaxScoreRefresh()
    {
        StopCoroutine(_cScores);
        _cScores = StartCoroutine(RefreshHighScores());
    }

    IEnumerator RefreshHighScores()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            _scoreManager.ReadScore();
            yield return new WaitForSeconds(25);
        }
    }
}