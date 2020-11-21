using UnityEngine;
using Random = UnityEngine.Random;

public enum CollectableType
{
    Learning,
    Researching, //investigating or researching? xD
    Reading,
    Exercising,
    Laughing
}

public class Collectable : MonoBehaviour
{
    private static float _velocity;
    [SerializeField] private CollectableType type;
    [SerializeField] private new ParticleSystem particleSystem;
    private SpriteRenderer _renderer;
    private bool _canSum;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _velocity = 5;
        _canSum = true;
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var torque = Random.Range(-1.5f, 1.5f);
        _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
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
        particleSystem.Play();
        /* TODO: Las siguientes lineas no creo que sea necesaria, si el objeto es
        destuido, el object pool es inutil.*/
        //_renderer.enabled = false;
        //Destroy(gameObject, 2);
    }


    public static void ChangeVelocity(float newVelocity)
    {
        _velocity = newVelocity;
    }
}
