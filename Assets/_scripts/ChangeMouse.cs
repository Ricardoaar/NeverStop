using System;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class ChangeMouse : MonoBehaviour
{
    [SerializeField] private Texture2D cursorUI, cursorShoot;
    private Camera _camera;
    private Texture2D _currentCursor;


    private void Awake()
    {
        _camera = Camera.main;
    }


    private void Start()
    {
    }

    private void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Input.mousePosition.z));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame ||
            !other.CompareTag("MainCamera")) return;


        Cursor.SetCursor(cursorShoot, new Vector2(32, 32), CursorMode.Auto);
        _currentCursor = cursorShoot;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("MainCamera") || _currentCursor == cursorUI) return;

        Cursor.SetCursor(cursorUI, new Vector2(32, 32), CursorMode.Auto);
        _currentCursor = cursorUI;
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.SingleInstance.GetCurrentGameState() == GameState.InGame || _currentCursor == cursorUI) return;
        Cursor.SetCursor(cursorUI, new Vector2(32, 32), CursorMode.Auto);
        _currentCursor = cursorUI;
    }
}