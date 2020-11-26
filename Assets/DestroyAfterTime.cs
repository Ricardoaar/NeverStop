using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    private float _currentTime;

    void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        _currentTime += Time.deltaTime;

        if (_currentTime >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}