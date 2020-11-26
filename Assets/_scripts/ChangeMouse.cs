using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class ChangeMouse : MonoBehaviour
{
    [SerializeField] private Texture2D cursorUI, cursorShootRed, cursorShotBlack;
    private Camera _camera;
    private Texture2D _currentCursor, _currentSight;
    private bool _in;

    private void Awake()
    {
        _camera = Camera.main;
        _currentSight = cursorShootRed;
        _in = false;
    }


    private void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Input.mousePosition.z));
        _currentSight = PlayerStats.SingleInstance.GetLaserState() ? cursorShootRed : cursorShotBlack;
        if (_in)
        {
            Cursor.SetCursor(_currentSight, new Vector2(32, 32), CursorMode.Auto);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame ||
            !other.CompareTag("MainCamera")) return;

        _in = true;

        Cursor.SetCursor(_currentSight, new Vector2(32, 32), CursorMode.Auto);
        _currentCursor = _currentSight;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("MainCamera") || _currentCursor == cursorUI) return;
        _in = false;
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