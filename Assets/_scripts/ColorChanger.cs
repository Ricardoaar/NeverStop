using System;
using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    private Coroutine _cColorChange;

    private void Awake()
    {
        Debug.Log(background.color);
    }

    public void ChangeColor(Color newColor, float transitionTime)
    {
        if (_cColorChange == null)
            _cColorChange = StartCoroutine(ChangeColorC(newColor, transitionTime));
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