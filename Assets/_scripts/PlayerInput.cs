using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField, Range(0, 12)] private float horizontalVelocity, horizontalVelocitySpeedUp;

    private Rigidbody2D _rigidbody;
    private float _minX, _maxX, _currentVelocity;


    private void Awake()
    {
        if (!(Camera.main is null))
        {
            _minX = Camera.main.gameObject.GetComponent<BoxCollider2D>().bounds.min.x;
            _maxX = Camera.main.gameObject.GetComponent<BoxCollider2D>().bounds.max.x;
        }


        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        _currentVelocity = Input.GetKey(KeyCode.LeftShift) ? horizontalVelocitySpeedUp : horizontalVelocity;
    }


    public void FixedUpdate()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;

        _rigidbody.MovePosition(new Vector2(Mathf.Clamp(_rigidbody.position.x +
                                                        Input.GetAxisRaw("Horizontal") * _currentVelocity *
                                                        Time.fixedDeltaTime, _minX, _maxX), _rigidbody.position.y));
    }
}