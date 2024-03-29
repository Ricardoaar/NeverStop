﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(1000)]
public class InGameGUI : MonoBehaviour
{
    private float _currentTime, _currentGoalTime;
    private Coroutine _cChangeTarget, _cToggleUI;

    [SerializeField] private Image shootUIImage;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI layerText;
    public TextMeshProUGUI layerMeters;
    public TextMeshProUGUI currentObjText;
    public Image currentObject;
    public Image energyFill;
    private List<Sprite> _objects = new List<Sprite>();
    public static InGameGUI SingleInstace;

    private float _energyDivider;

    private List<String> _objectsNames = new List<string>();

    public Filler GreenLedFiller;

    private PhaseManager _phaseManager;

    [SerializeField] private AudioClip _collectableChangeSfx;

    [SerializeField] private AudioClip _collectableSelectedSfx;

    private void Awake()
    {
        if (SingleInstace == null)
        {
            SingleInstace = this;
        }

        _currentGoalTime = 0;
        _currentTime = 0;
    }

    private void Start()
    {
        _energyDivider = 1 / PlayerStats.SingleInstance.GetCurrentEnergy();

        foreach (var item in GameManager.SingleInstance.collectables)
        {
            _objects.Add(item.sprite);
            _objectsNames.Add(item.elementName);
        }

        _phaseManager = FindObjectOfType<PhaseManager>();
    }

    private void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        energyFill.fillAmount = _energyDivider * PlayerStats.SingleInstance.GetCurrentEnergy();
        scoreText.text = $"{PlayerStats.SingleInstance.GetCurrentScore()}";


        if (_currentTime >= _currentGoalTime && _cChangeTarget == null)
            _cChangeTarget = StartCoroutine(ChangeTargetUI());
        else if (_cChangeTarget == null)
        {
            GreenLedFiller.SetFillValue(_currentGoalTime - _currentTime);
            _currentTime += Time.deltaTime;
        }
    }

    //Refactorizar esto al tener el phase manager


    private void ChangeObj(bool random, int i = 0)
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


    private IEnumerator ChangeTargetUI()
    {
        var randomTimes = Random.Range(10, 20);
        GreenLedFiller.SetMaxValue(randomTimes);
        GreenLedFiller.SetFillValue(0);

        if (_cToggleUI == null)
            _cToggleUI = StartCoroutine(ToggleImageColor(shootUIImage, 0.4f, 0.8f));


        yield return new WaitUntil(() => _cToggleUI == null);
        AudioManager.SingleInstance.PlaySFXLoop(_collectableChangeSfx);
        for (var i = 0; i < randomTimes; i++)
        {
            ChangeObj(true);
            GreenLedFiller.SetFillValue(i);
            yield return new WaitUntil(() => GameManager.SingleInstance.GetCurrentGameState() == GameState.InGame);
            yield return new WaitForSeconds(0.2f);
            if (i == 0)
                PlayerStats.SingleInstance.ChangeCollectableType(CollectableType.Null);
        }

        AudioManager.SingleInstance.StopSFXLoop();
        //Change UI and final item
        var currentCollectable = Random.Range(0, GameManager.SingleInstance.collectables.Count);

        ChangeObj(false, currentCollectable);
        PlayerStats.SingleInstance.ChangeCollectableType(currentCollectable);
        AudioManager.SingleInstance.PlaySFX(_collectableSelectedSfx);

        //ResetTimeAndCoroutine
        _currentGoalTime = Random.Range(_phaseManager.GetMinTimeBetweenCollectable(),
            _phaseManager.GetMaxTimeBetweenCollectable());

        GreenLedFiller.SetMaxValue(_currentGoalTime);
        _currentTime = 0;
        _cChangeTarget = null;
    }


    private IEnumerator ToggleImageColor(Image UI, float toggleAlpha1, float toggleAlpha2 = 1)
    {
        var initialAlpha = UI.color.a;
        for (var i = 0; i < 4; i++)
        {
            yield return new WaitUntil(() => GameManager.SingleInstance.GetCurrentGameState() == GameState.InGame);
            yield return new WaitForSeconds(0.25f);
            UI.color = new Color(UI.color.r, UI.color.g, UI.color.b, toggleAlpha1);
            yield return new WaitUntil(() => GameManager.SingleInstance.GetCurrentGameState() == GameState.InGame);
            yield return new WaitForSeconds(0.25f);
            UI.color = new Color(UI.color.r, UI.color.g, UI.color.b, toggleAlpha2);
        }

        UI.color = new Color(UI.color.r, UI.color.g, UI.color.b, initialAlpha);
        _cToggleUI = null;
    }


    public void ChangeLayerText(String newLayerText, String newLayerMeters)
    {
        layerText.text = newLayerText;
        layerMeters.text = newLayerMeters;
    }
}
