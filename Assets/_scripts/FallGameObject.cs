using System;
using UnityEngine;

public class FallGameObject : MonoBehaviour
{
    private static float _fallVelocity;

    private void Awake()
    {
        if (_fallVelocity <= 0)
        {
            _fallVelocity = 3;
        }
    }

    private void Update()
    {
        if (GameManager.SingleInstance.GetCurrentGameState() != GameState.InGame) return;
        transform.Translate(new Vector3(0, -_fallVelocity * Time.deltaTime));
    }

    public static void ChangeVelocity(float newVelocity)
    {
        _fallVelocity = newVelocity;
    }
}