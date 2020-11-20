using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats SingleInstance;
    private float _currentScore;
    private float _currentEnergy;
    private CollectableType _currentCollectable;
    [SerializeField, Range(1, 2f)] private float initialScoreMultiplier;
    private float _scoreMultiplier;
    [SerializeField] private float initialEnergy;
    [SerializeField, Range(5, 10)] private float energyPerObj, energyLostPerBadObj;
    [SerializeField, Range(0, 2.0f)] private float energyLostPerSecond;

    private void Awake()
    {
        if (SingleInstance == null)
        {
            SingleInstance = this;
        }

        _scoreMultiplier = initialScoreMultiplier;
    }

    private void Start()
    {
        RestartValues();
    }

    public void ChangeCollectableType(CollectableType newCollectableTypeType)
    {
        _currentCollectable = newCollectableTypeType;
    }

    private void FixedUpdate()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        ChangeEnergy(energyLostPerSecond * Time.fixedDeltaTime, false);
        _currentScore += Time.deltaTime * _scoreMultiplier;
    }


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

    public void RestartValues()
    {
        _currentEnergy = initialEnergy;
        _currentScore = 0;
    }

    public float GetCurrentDistance()
    {
        return _currentScore;
    }

    public float GetCurrentEnergy()
    {
        return _currentEnergy;
    }

    private void ChangeEnergy(float energyWin, bool win = true)
    {
        _currentEnergy = win ? _currentEnergy + energyWin : _currentEnergy - energyWin;
    }
}