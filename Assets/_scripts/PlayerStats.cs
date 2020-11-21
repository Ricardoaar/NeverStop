using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats SingleInstance;
    private float _currentScore;
    private float _currentEnergy;
    private CollectableType _currentCollectable;

    [SerializeField, Range(20.0f, 30.0f), Tooltip("Entre mas pequeño el crecimiento de ganancias de puntos es menor")]
    private float scoreDivider;

    private float _scoreMultiplier;
    [SerializeField] private float initialEnergy;
    [SerializeField, Range(5, 10)] private float energyPerObj, energyLostPerBadObj;
    [SerializeField, Range(0, 2.0f)] private float energyLostPerSecond;


    private void Awake()
    {
        if (SingleInstance == null)
            SingleInstance = this;

        _scoreMultiplier = 1;
    }

    private void Start()
    {
        RestartValues();
    }

    private void FixedUpdate()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;

        ChangeEnergy(energyLostPerSecond * Time.fixedDeltaTime, false);
        _currentScore += Time.deltaTime * _scoreMultiplier;
        _scoreMultiplier += Time.deltaTime / scoreDivider;
    }

//Collectables
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Collectable")) return;
        var collectable = other.GetComponent<Collectable>();
        collectable.BeCollected();
        if (collectable.GetCollectableType() == _currentCollectable)
            ChangeEnergy(energyPerObj);
        else
            ChangeEnergy(energyLostPerBadObj, false);
    }

    private void RestartValues()
    {
        _currentCollectable = CollectableType.Null;
        _currentEnergy = initialEnergy;
        _currentScore = 0;
    }

    public float GetCurrentScore()
    {
        return _currentScore;
    }

    public float GetCurrentEnergy()
    {
        return _currentEnergy;
    }

    public void ChangeCollectableType(CollectableType newCollectableTypeType)
    {
        _currentCollectable = newCollectableTypeType;
    }

    private void ChangeEnergy(float energyWin, bool win = true)
    {
        _currentEnergy = Mathf.Clamp(win ? _currentEnergy + energyWin : _currentEnergy - energyWin, 0, initialEnergy);
    }
}