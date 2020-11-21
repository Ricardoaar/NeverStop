using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(1000)]
public class InGameGUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI layerText;
    public TextMeshProUGUI currentObjText;
    public Image currentObject;
    public Image energyFill;
    private List<Sprite> _objects = new List<Sprite>();
    public static InGameGUI SingleInstace;

    public Sprite learning, researching, reading, exercising, laughing;

    private float _energyDivider;


    private List<String> _objectsNames = new List<string>();

    private void Awake()
    {
        if (SingleInstace == null)
        {
            SingleInstace = this;
        }

        _objectsNames = Enum.GetNames(typeof(CollectableType)).ToList();
        _objects.Add(learning);
        _objects.Add(researching);
        _objects.Add(reading);
        _objects.Add(exercising);
        _objects.Add(laughing);
    }

    private void Start()
    {
        _energyDivider = 1 / PlayerStats.SingleInstance.GetCurrentEnergy();
        Debug.Log(_energyDivider);
    }

    private void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        energyFill.fillAmount = _energyDivider * PlayerStats.SingleInstance.GetCurrentEnergy();
        scoreText.text = $"{PlayerStats.SingleInstance.GetCurrentScore()}";
    }

//Refactorizar esto al tener el phase manager


    public void ChangeObj(bool random, int i = 0)
    {
        if (!random)
        {
            currentObject.sprite = _objects[i];
            currentObjText.text = _objectsNames[i];
        }
        else
        {
            currentObject.sprite = _objects[Random.Range(0, _objects.Count)];
            currentObjText.text = _objectsNames[Random.Range(0, _objects.Count)];
        }
    }
}