using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float horizontalVelocity;
    private Rigidbody2D _rigidbody;
    private float _minX, _maxX;
    private bool _moving;

    private void Awake()
    {
        if (!(Camera.main is null))
        {
            _minX = Camera.main.gameObject.GetComponent<BoxCollider2D>().bounds.min.x;
            _maxX = Camera.main.gameObject.GetComponent<BoxCollider2D>().bounds.max.x;
        }

        _moving = Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 0;


        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame)
            return; //Mantener en caso de agregar cosas al update, si no borrar
    }

    public void FixedUpdate()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        _rigidbody.position = new Vector2(Mathf.Clamp(_rigidbody.position.x, _minX, _maxX), _rigidbody.position.y);
        _rigidbody.MovePosition(_rigidbody.position + new Vector2(
            Input.GetAxisRaw("Horizontal") * horizontalVelocity * Time.fixedDeltaTime, 0));
    }
}