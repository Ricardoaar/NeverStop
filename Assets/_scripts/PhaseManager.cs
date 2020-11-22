using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class PhaseManager : MonoBehaviour
{
    [SerializeField, Range(0, 20)]
    private float minTimeBetweenPhase, maxTimeBetweenPhase; // Update is called once per frame

    private float _currentTime, _currentGoalTime;
    private int _collectTypeNum;
    private Coroutine _cChangeTarget;

    private void Awake()
    {
        _currentGoalTime = 0;
        _currentTime = 0;
        _collectTypeNum = Enum.GetValues(typeof(CollectableType)).Length - 1;
    }


    void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        if (_currentTime >= _currentGoalTime && _cChangeTarget == null)
        {
            _cChangeTarget = StartCoroutine(ChangeTargetUI());
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }


    private IEnumerator ChangeTargetUI()
    {
        var randomTimes = Random.Range(10, 20);

        for (var i = 0; i < randomTimes; i++)
        {
            InGameGUI.SingleInstace.ChangeObj(true);

            yield return new WaitForSeconds(0.2f);

            if (i == 0)
                PlayerStats.SingleInstance.ChangeCollectableType(CollectableType.Null);
        }

        //Change UI and final item
        var currentCollectable = Random.Range(0, GameManager.SingleInstance.collectables.Count);
        InGameGUI.SingleInstace.ChangeObj(false, currentCollectable);
        PlayerStats.SingleInstance.ChangeCollectableType(currentCollectable);
        //ResetTimeAndCoroutine
        _currentGoalTime = Random.Range(minTimeBetweenPhase, maxTimeBetweenPhase);
        _currentTime = 0;
        _cChangeTarget = null;
    }
}