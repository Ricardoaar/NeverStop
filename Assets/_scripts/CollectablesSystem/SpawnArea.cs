using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [Tooltip("Tiempo entre cada spawn")]
    private static float _timeBetweenSpawn;

    [Tooltip("Object Pool encargada de gestionar los Collectables")] [SerializeField]
    private ObjectPool _pool;

    [Tooltip("Zona de spawn de los collectables")] [SerializeField]
    private Transform _spawnZone;

    [Tooltip("Zona para despawnear los Collectables")] [SerializeField]
    private Transform _killZone;

    private float _timeToSpawn = 0.0f;

    void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame)
            return;

        _timeToSpawn -= Time.deltaTime;
        if (_timeToSpawn <= 0.0f)
        {
            _timeToSpawn = _timeBetweenSpawn;
            SpawnCollectable();
        }
    }

    private void SpawnCollectable()
    {
        var collectable = _pool.ExtractFromQueue();
        collectable.transform.position = new Vector3(
            Random.Range(
                _spawnZone.position.x - (_spawnZone.localScale.x / 2),
                _spawnZone.position.x + (_spawnZone.localScale.x / 2)
            ),
            Random.Range(
                _spawnZone.position.y - (_spawnZone.localScale.y / 2),
                _spawnZone.position.y + (_spawnZone.localScale.y / 2)
            ),
            1
        );
    }

    public static void ChangeSpawnTime(float time)
    {
        _timeBetweenSpawn = time;
    }
}
