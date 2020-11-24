using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteGenerator : MonoBehaviour
{
    private static List<Sprite> currentSprites = new List<Sprite>();

    private Queue<SpriteRenderer> containers = new Queue<SpriteRenderer>();
    [SerializeField, Range(1, 10)] private float minSpawnTime, maxSpawnTime, fallVelocity;
    [SerializeField] private GameObject template;
    private float _currentGoalTime, _currentTime;
    private int _currentSpriteIndex;
    public BoxCollider2D insZone;

    private void Start()
    {
        FallGameObject.ChangeVelocity(fallVelocity);
    }

    void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        if (_currentTime >= _currentGoalTime)
            GenerateSprite();
        else
            _currentTime += Time.deltaTime;
    }

    private void Awake()
    {
        PopulateQueue(10);
    }

    private void PopulateQueue(int queueItems)
    {
        for (int i = 0; i < queueItems; i++)
        {
            containers.Enqueue(GenerateNewContainer());
        }
    }

    private SpriteRenderer GenerateNewContainer()
    {
        var current = Instantiate(template, insZone.gameObject.transform.position, quaternion.Euler(Vector3.zero));
        current.SetActive(false);
        return current.GetComponent<SpriteRenderer>();
    }

    private void GenerateSprite()
    {
        var randomPos = new Vector3(Random.Range(insZone.bounds.max.x, insZone.bounds.min.x), insZone.bounds.max.y);
        var currentObj = containers.Dequeue();
        currentObj.transform.position = randomPos;
        currentObj.gameObject.SetActive(true);
        currentObj.sprite = GetSprite();

        _currentGoalTime = Random.Range(minSpawnTime, maxSpawnTime);
        _currentTime = 0;
    }


    private Sprite GetSprite()
    {
        if (_currentSpriteIndex >= currentSprites.Count)
        {
            _currentSpriteIndex = 0;
        }

        return currentSprites[_currentSpriteIndex++];
    }

    public void AddToQueue(SpriteRenderer container)
    {
        container.gameObject.SetActive(false);
        containers.Enqueue(container);
    }

    public static void ChangeSpriteList(List<Sprite> newListSprites)
    {
        currentSprites = newListSprites;
    }
}