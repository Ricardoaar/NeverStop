using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Tooltip("Objeto base a spawnear (sus propiedades serán modificadas con ScriptableObjects)")]
    [SerializeField] private GameObject _collectableBase;
    [Tooltip("Número de objetos iniciales que tendrá el Object Pool")]
    [SerializeField] private int _maxCollectables = 10;
    private Queue<GameObject> _queue;
    private FactoryCollectable _factoryCollectable;

    void Awake()
    {
        _factoryCollectable = new FactoryCollectable(_collectableBase);
        _queue = new Queue<GameObject>(_maxCollectables);
    }

    void Start()
    {
        PopulateQueue();
    }

    void PopulateQueue()
    {
        for (int i = 0; i < _maxCollectables; i++)
        {
            CreateNewCollectable();
        }
    }

    private void CreateNewCollectable()
    {
        var collectable = _factoryCollectable.CreateCollectable();
        _queue.Enqueue(collectable);
    }

    public GameObject ExtractFromQueue()
    {
        // En caso de no tener ningún objeto que sacar de la Queue, se instancia uno nuevo.
        if (_queue.Count == 0)
            CreateNewCollectable();
        var collectable = _queue.Dequeue();
        collectable.SetActive(true);
        return collectable;
    }

    public void AddToQueue(GameObject collectable)
    {
        collectable.SetActive(false);
        _queue.Enqueue(collectable);
    }
}
