using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class PhaseManager : MonoBehaviour
{
    [SerializeField, Range(0, 20)]
    private float minTimeBetweenPhase, maxTimeBetweenPhase; // Update is called once per frame

    private float _currentTime, _currentGoalTime;
    private Coroutine _cChangeTarget;

    private void Awake()
    {
        _currentGoalTime = 0;
        _currentTime = 0;
    }


    void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;

        if (_currentTime >= _currentGoalTime && _cChangeTarget == null)
            _cChangeTarget = StartCoroutine(ChangeTargetUI());

        else
            _currentTime += Time.deltaTime;
    }


    private IEnumerator ChangeTargetUI()
    {
        var randomTimes = Random.Range(10, 20);
        var guiScript = InGameGUI.SingleInstace;
        guiScript.SwitchSprite(ref InGameGUI.SingleInstace.greenButtonImg,
            guiScript.buttonDown);
        for (var i = 0; i < randomTimes; i++)
        {
            guiScript.ChangeObj(true);
            yield return new WaitUntil(() => GameManager.SingleInstance.GetCurrentGameState() == GameState.InGame);
            yield return new WaitForSeconds(0.2f);

            if (i == 0)
                PlayerStats.SingleInstance.ChangeCollectableType(CollectableType.Null);
        }

        guiScript.SwitchSprite(ref InGameGUI.SingleInstace.greenButtonImg,
            guiScript.greenButton);
        //Change UI and final item
        var currentCollectable = Random.Range(0, GameManager.SingleInstance.collectables.Count);

        guiScript.ChangeObj(false, currentCollectable);
        PlayerStats.SingleInstance.ChangeCollectableType(currentCollectable);

        //ResetTimeAndCoroutine
        _currentGoalTime = Random.Range(minTimeBetweenPhase, maxTimeBetweenPhase);
        _currentTime = 0;
        _cChangeTarget = null;
    }
}