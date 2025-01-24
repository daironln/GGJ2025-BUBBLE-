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
    public GameState gameState;


    private void Start()
    {
        gameState = GameState.None;
    }   


    public void Pause(bool paused)
    {
        if(paused)
        {
            gameState = GameState.InPlay;
        }
        else
        {
            gameState = GameState.InPause;

            Time.timeScale = 0;
        }
    }
}
