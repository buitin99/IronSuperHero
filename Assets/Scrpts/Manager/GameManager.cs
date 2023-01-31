using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public bool isInit;
    public UnityEvent OnStartGame = new UnityEvent();

    public void StartGame()
    {
        OnStartGame?.Invoke();
        if (!isInit)
        {
            InitGame();
        }
    }

    public void InitGame()
    {
        isInit = true;
    }
    
}
