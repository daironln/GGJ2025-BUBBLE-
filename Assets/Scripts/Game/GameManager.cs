using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public enum GameState{
    None,
    InMainMenu,
    InPause,
    InPlay,
    InQuit,
    InGameOver
}
public class GameManager : PersistentSingleton<GameManager>
{

    private bool _isPaused;
    public GameState gameState;


    private void Start()
    {
        _isPaused = false;
        gameState = GameState.None;
    }   


    public void Pause()
    {
        if(_isPaused)
        {
            gameState = GameState.InPlay;
        }
        else
        {
            gameState = GameState.InPause;

            Time.timeScale = 0;
        }

        _isPaused = !_isPaused;
    }
}
