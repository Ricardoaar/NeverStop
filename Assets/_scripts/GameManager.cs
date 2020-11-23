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
    public UnityEvent onGameOver;


    public GameObject panelPause;

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


    public void Pause()
    {
        if (_currentGameState == GameState.InGame)
        {
            ChangeGameState(GameState.MainMenu);
            panelPause.SetActive(true);
        }
        else
        {
            ChangeGameState(GameState.InGame);
            panelPause.SetActive(false);
        }
    }
}