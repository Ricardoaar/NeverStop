using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public List<ScriptableCollectable> collectables = new List<ScriptableCollectable>();
    public UnityEvent onPause;
    public UnityEvent onGameOver;

    private void Awake()
    {
        //Cambiar cuando al implementar la UI
        _currentGameState = GameState.InGame;
        if (SingleInstance == null)
            SingleInstance = this;
    }


    private void ChangeGameState(GameState newGameState)
    {
        _currentGameState = newGameState;
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }


    public void GameOver()
    {
        ChangeGameState(GameState.GameOver);
        onGameOver.Invoke();
    }
}