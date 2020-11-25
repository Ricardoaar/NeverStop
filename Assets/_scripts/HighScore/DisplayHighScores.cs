using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DisplayHighScores : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> highScoreFields;
    private HighScoreManager _scoreManager;
    private static List<int> _scores = new List<int>();
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
        var currentScores = new List<int>();

        int i = 0;
        foreach (TextMeshProUGUI scoreField in highScoreFields)
        {
            scoreField.text = $"{i + 1}. ";
            if (i < listScores.Count)
            {
                scoreField.text += $"{listScores[i].UserName} - {listScores[i].Value}";
                currentScores.Add(listScores[i].Value);
            }

            i++;
        }

        _scores = currentScores;
    }

    public static List<int> GetScoreList()
    {
        return _scores;
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