using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CollectableType
{
    Learning,
    Researching, //investigating or researching? xD
    Reading,
    Exercising,
    Laughing,
    Playing,
    Null
}

public class Collectable : MonoBehaviour
{
    public delegate void ONCollect();

    public static event ONCollect OnCollectBad;


    private static float _velocity;
    [SerializeField] private CollectableType type;
    [SerializeField] private new ParticleSystem particleSystem;
    private SpriteRenderer _renderer;
    private bool _canSum;
    private Rigidbody2D _rigidbody;
    [SerializeField] private AudioClip _rightCollectableSfx;
    [SerializeField] private AudioClip _badCollectableSfx;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _canSum = true;
        _renderer = GetComponent<SpriteRenderer>();
    }


    public CollectableType GetCollectableType()
    {
        return type;
    }

    private void FixedUpdate()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        _rigidbody.MovePosition(_rigidbody.position - new Vector2(0, _velocity * Time.deltaTime));
    }

    public void BeCollected()
    {
        if (!_canSum) return;
        _canSum = false;
        _renderer.enabled = false;
        particleSystem.Play();
        if (GetCollectableType() == PlayerStats.SingleInstance.GetCurrentCollectable())
        {
            PlayerStats.SingleInstance.ChangeEnergy(PlayerStats.SingleInstance.energyPerObj);
            AudioManager.SingleInstance.PlaySFX(_rightCollectableSfx);
        }
        else
        {
            OnCollectBad?.Invoke();
            PlayerStats.SingleInstance.ChangeEnergy(PlayerStats.SingleInstance.energyLostPerBadObj, false);
            AudioManager.SingleInstance.PlaySFX(_badCollectableSfx);
        }
    }

    /// <summary>
    /// Metodo estatico para cambiar la velocidad de todos los collectables al cambiar fases
    /// </summary>
    /// <param name="newVelocity"></param>
    public static void ChangeVelocity(float newVelocity)
    {
        _velocity = newVelocity;
    }

    private void OnEnable()
    {
        var torque = Random.Range(-1.5f, 1.5f);

        _rigidbody.AddTorque(torque, ForceMode2D.Impulse);

        var template = GetTemplate();

        _renderer.sprite = GameManager.SingleInstance.collectables[template].sprite;

        type = GameManager.SingleInstance.collectables[template].type;
        RestartedValues();
    }

    private int GetTemplate()
    {
        var temp = 0;
        // if (Random.Range(0.0f, 1.0f) < 0.1f)
        // {
        //     foreach (var collectable in GameManager.SingleInstance.collectables.Where(collectable =>
        //         collectable.type == PlayerStats.SingleInstance.GetCurrentCollectable()))
        //         temp = GameManager.SingleInstance.collectables.IndexOf(collectable);
        // }
        // else
        temp = Random.Range(0, GameManager.SingleInstance.collectables.Count);


        return temp;
    }


    private void RestartedValues()
    {
        _canSum = true;
        _renderer.enabled = true;
    }


    private void OnMouseDown()
    {
        if (!PlayerStats.SingleInstance.Shoot() ||
            GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        PlayerInput.singleInstance.Shoot();
        BeCollected();
    }

    private void OnDestroy()
    {
        OnCollectBad = null;
    }
}