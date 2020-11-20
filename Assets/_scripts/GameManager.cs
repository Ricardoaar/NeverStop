using System;
using UnityEngine;

public enum GameState
{
    InGame,
    GameOver,
    MainMenu
}

public class GameManager : MonoBehaviour
{
    private GameState _currentGameState;
    public static GameManager SingleInstance;

    private void Awake()
    {
        //Cambiar cuando al implementar la UI
        _currentGameState = GameState.InGame;
        if (SingleInstance == null)
            SingleInstance = this;
    }

    private void ChangeGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.InGame:
                break;
            case GameState.GameOver:
                break;
            case GameState.MainMenu:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}