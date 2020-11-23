using System;
using System.Collections.Generic;
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

    private float _energyDivider;

    private List<String> _objectsNames = new List<string>();

    [Header("Leds")] public Image redButtonImg;
    public Image greenButtonImg;
    public Sprite redButton, greenButton, buttonDown;


    private void Awake()
    {
        if (SingleInstace == null)
        {
            SingleInstace = this;
        }
    }

    private void Start()
    {
        _energyDivider = 1 / PlayerStats.SingleInstance.GetCurrentEnergy();

        foreach (var item in GameManager.SingleInstance.collectables)
        {
            _objects.Add(item.sprite);
            _objectsNames.Add(item.elementName);
        }
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

    public void SwitchSprite(ref Image current, Sprite newSprite)
    {
        current.sprite = newSprite;
    }
}