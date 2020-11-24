using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour
{
    [SerializeField] private List<Text> highScoreFields;
    private HighScoreManager _scoreManager;

    private void Start()
    {
        int i = 0;
        foreach (Text scoreField in highScoreFields)
        {
            i++;
            scoreField.text = $"{i}. Cargando...";
        }

        _scoreManager = GetComponent<HighScoreManager>();
        
        // Refresca el puntaje cada 30 segundos
        StartCoroutine("RefreshHighScores");
    }

    public void OnHighScoresDownloaded(List<Score> listScores)
    {
        int i = 0;
        foreach (Text scoreField in highScoreFields)
        {
            scoreField.text = $"{i + 1}. ";
            if (i < listScores.Count)
                scoreField.text += $"{listScores[i].UserName} - {listScores[i].Value}";
            i++;
        }
    }

    IEnumerator RefreshHighScores()
    {
        while (true)
        {
            _scoreManager.ReadScore();
            yield return new WaitForSeconds(30);
        }
    }
}
