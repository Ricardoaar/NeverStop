using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColors : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    [SerializeField] private SpriteRenderer background;
    private Coroutine _cColorChange;
    private int _currentColor;

    private void Awake()
    {
        Debug.Log(background.color);
        _currentColor = 0;
    }

    public void ChangeColor(Color newColor)
    {
        background.color = newColor;
    }


    private void Update()
    {
        if (_cColorChange == null && _currentColor < colors.Count)
        {
            _cColorChange = StartCoroutine(ChangeColorC(colors[_currentColor], 2));
            _currentColor++;
            Debug.Log(_currentColor);
        }
    }


    /// <summary>
    /// Cambiar el color del fondo de manera creciente
    /// </summary>
    /// <param name="NewColor">Color a cambiar</param>
    /// <param name="transitionTime">Tiempo que dura la transicion en segundos</param>
    /// <returns></returns>
    private IEnumerator ChangeColorC(Color NewColor, float transitionTime)
    {
        var current = background.color;
        var difR = current.r - NewColor.r;
        var difG = current.g - NewColor.g;
        var difB = current.b - NewColor.b;
        current.a = 1;


        var addR = difR / transitionTime / 60;
        var addG = difG / transitionTime / 60;
        var addB = difB / transitionTime / 60;


        while (Math.Abs(background.color.r - NewColor.r) > 0.01f)
        {
            current.r -= addR;
            current.g -= addG;
            current.b -= addB;

            yield return new WaitForSecondsRealtime(1 / 60f);
            yield return new WaitUntil(() => GameManager.SingleInstance.GetCurrentGameState() == GameState.InGame);
            background.color = current;
        }

        _cColorChange = null;
    }
}