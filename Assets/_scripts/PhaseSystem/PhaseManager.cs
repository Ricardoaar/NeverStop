using System.Collections.Generic;
using UnityEngine;


public class PhaseManager : MonoBehaviour
{
    [SerializeField, Tooltip("Lista de fases")]
    private List<ScriptablePhase> _phases = new List<ScriptablePhase>();

    //La he serializado para testear y ver su valor ene l editor
    [SerializeField] private int _currentPhase;
    private float _scoreToChangePhase;

    private float _minTimeBetweenCollectable, _maxTimeBetweenCollectable; // Update is called once per frame

    [SerializeField] private ColorChanger colorChanger;

    [SerializeField] private string _atmosphereLayer;
    private string _atmosphereLayerDescription;


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
    }

    void ChangePhase()
    {
        _currentPhase = _currentPhase != _phases.Count - 1 ? ++_currentPhase : _currentPhase;
        var nextPhase = _phases[_currentPhase];
        SpriteGenerator.ChangeSpriteList(nextPhase.phaseDecElements);
        InGameGUI.SingleInstace.ChangeLayerText(nextPhase.atmosphereLayer, nextPhase.atmosphereLayerDescription);
        _atmosphereLayer = nextPhase.atmosphereLayer;
        _atmosphereLayerDescription = nextPhase.atmosphereLayerDescription;
        _minTimeBetweenCollectable = nextPhase.minTimeCollectableChange;
        _maxTimeBetweenCollectable = nextPhase.maxTimeCollectableChange;
        colorChanger.ChangeColor(nextPhase.color, nextPhase.transitionTime);
        _scoreToChangePhase = nextPhase.scoreToChangePhase;
        SpawnArea.ChangeSpawnTime(nextPhase.timeToSpawn);
        Collectable.ChangeVelocity(nextPhase.collectableVelocity);
    }

    public float GetMinTimeBetweenCollectable()
    {
        return _minTimeBetweenCollectable;
    }

    public float GetMaxTimeBetweenCollectable()
    {
        return _maxTimeBetweenCollectable;
    }
}