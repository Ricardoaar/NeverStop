using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerStats : MonoBehaviour
{
    [Header("---------Test----------")] [SerializeField]
    private bool inmortal, die;


    [SerializeField, Range(0, 10)] [Header("---------Test----------")]
    private float timeScale;

    public static PlayerStats SingleInstance;
    private float _currentScore;
    private float _currentEnergy;
    private CollectableType _currentCollectable;

    [SerializeField, Range(20.0f, 30.0f), Tooltip("Entre mas pequeño el crecimiento de ganancias de puntos es menor")]
    private float scoreDivider;

    private float _scoreMultiplier;
    [SerializeField] private float initialEnergy;
    [Range(5, 10)] public float energyPerObj, energyLostPerBadObj;
    [SerializeField, Range(0, 2.0f)] private float energyLostPerSecond;
    [SerializeField, Range(1, 10)] private float timeBetweenShoot;
    private float _currentShootTime;
    private bool _canShoot;


    private Animator _animator;
    private static readonly int IsAlive = Animator.StringToHash("IsAlive");

    [SerializeField] private AudioClip _launchSfx;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canShoot = true;
        if (SingleInstance == null)
            SingleInstance = this;
    }

    private void Start()
    {
        RestartValues();
        AudioManager.SingleInstance.PlaySFX(_launchSfx);
    }

    private void Update()
    {
        Time.timeScale = timeScale;

        if (inmortal)
        {
            _currentEnergy = initialEnergy;
        }

        if (die)
        {
            _currentEnergy = 0;
        }

        if (_canShoot || GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        _currentShootTime += Time.deltaTime;
        if (_currentShootTime >= timeBetweenShoot)
        {
            RestartShoot();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        ChangeEnergy(energyLostPerSecond * Time.fixedDeltaTime, false);
        _currentScore += Time.deltaTime * _scoreMultiplier;
        _scoreMultiplier += Time.deltaTime / scoreDivider;

        if (!(_currentEnergy <= 0)) return;
        GameManager.SingleInstance.GameOver();
        _animator.SetBool(IsAlive, false);
    }

    //Collectables
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Collectable")) return;
        var collectable = other.GetComponent<Collectable>();
        collectable.BeCollected();
    }

    private void RestartValues()
    {
        _currentShootTime = 0;
        _currentCollectable = CollectableType.Null;
        _currentEnergy = initialEnergy;
        _currentScore = 0;
        _scoreMultiplier = 1;
    }

    public float GetCurrentScore()
    {
        return Mathf.Ceil(_currentScore);
    }

    public float GetCurrentEnergy()
    {
        return _currentEnergy;
    }

    public void ChangeCollectableType(int newCollectableTypeType)
    {
        _currentCollectable = GameManager.SingleInstance.collectables[newCollectableTypeType].type;
    }

    public void ChangeCollectableType(CollectableType newCollectableTypeType)
    {
        _currentCollectable = newCollectableTypeType;
    }

    public CollectableType GetCurrentCollectable()
    {
        return _currentCollectable;
    }

    public void ChangeEnergy(float energyWin, bool win = true)
    {
        _currentEnergy = Mathf.Clamp(win ? _currentEnergy + energyWin : _currentEnergy - energyWin, 0, initialEnergy);
    }

    public bool Shoot()
    {
        if (_canShoot)
        {
            _canShoot = false;
            return !_canShoot;
        }

        return _canShoot;
    }


    private void RestartShoot()
    {
        _currentShootTime = 0;
        _canShoot = true;
    }

    public float GetShootTime()
    {
        return timeBetweenShoot;
    }

    public float GetCurrentShootTime()
    {
        return _currentShootTime;
    }

    public bool GetLaserState()
    {
        return _canShoot;
    }


    public bool GetLasserStatus()
    {
        return _canShoot;
    }
}