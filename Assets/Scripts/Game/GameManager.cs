using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public enum GameState
{
    InMainMenu,
    InPlay,
    InPause,
    InQuit,
    InGameOver
}

public class GameManager : PersistentSingleton<GameManager>
{
    public GameState State;

    private void Start()
    {
        State = GameState.InMainMenu;
    }

    public void Pause(bool pause)
    {
        if(pause)
            State = GameState.InPlay;

        else
            State = GameState.InPause;

        Time.timeScale = 0;
    }
}
