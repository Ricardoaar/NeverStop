using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PhaseManager : MonoBehaviour
{
    private float _currentTime, _currentGoalTime;
    private Coroutine _cChangeTarget;
    [SerializeField, Tooltip("Lista de fases")]
    private List<ScriptablePhase> _phases = new List<ScriptablePhase>();
    //La he serializado para testear y ver su valor ene l editor
    [SerializeField] private int _currentPhase;

    private float _scoreToChangePhase;
    [SerializeField, Range(0, 20)]
    private float minTimeBetweenCollectable, maxTimeBetweenCollectable; // Update is called once per frame
    private string _atmosphereLayer;
    private string _atmosphereLayerDescription;


    private void Awake()
    {
        _currentGoalTime = 0;
        _currentTime = 0;
    }

    void Start()
    {
        _currentPhase = -1;
        ChangePhase();
    }


    void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;

        if (PlayerStats.SingleInstance.GetCurrentScore() > _scoreToChangePhase)
            ChangePhase();

        if (_currentTime >= _currentGoalTime && _cChangeTarget == null)
            _cChangeTarget = StartCoroutine(ChangeTargetUI());
        else
            _currentTime += Time.deltaTime;
    }

    void ChangePhase()
    {
        _currentPhase = _currentPhase != _phases.Count - 1 ? ++_currentPhase : _currentPhase;
        var nextPhase = _phases[_currentPhase];
        _atmosphereLayer = nextPhase.atmosphereLayer;
        _atmosphereLayerDescription = nextPhase.atmosphereLayerDescription;
        minTimeBetweenCollectable = nextPhase.minTimeCollectableChange;
        maxTimeBetweenCollectable = nextPhase.maxTimeCollectableChange;
        _scoreToChangePhase = nextPhase.scoreToChangePhase;
        SpawnArea.ChangeSpawnTime(nextPhase.timeToSpawn);
        Collectable.ChangeVelocity(nextPhase.collectableVelocity);
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
        _currentGoalTime = Random.Range(minTimeBetweenCollectable, maxTimeBetweenCollectable);
        _currentTime = 0;
        _cChangeTarget = null;
    }
}
