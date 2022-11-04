using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameMode
{ 
    Exploration,
    Puzzle,
    Platforming,
    Creation,
    Menu
}

public class GameModeManager : Singleton<GameModeManager>
{
    [SerializeField]
    private static GameMode m_gameMode;

    public static GameMode GameMode
    {
        get { return m_gameMode; }
    }

    public static event Action<GameMode> ChangeMode;

    void Start()
    {
        OnModeChange();
    }

    public static void ToggleMode(GameMode mode)
    {
        m_gameMode = mode;
        OnModeChange();      
    }

    static void OnModeChange()
    {
        Debug.Log("Changing Mode: " + GameMode);
        ChangeMode?.Invoke(GameMode);
    }

    public void SwitchToExploration()
    {
        ToggleMode(GameMode.Exploration);
    }

    public void SwitchToPuzzle()
    {
        ToggleMode(GameMode.Puzzle);
    }

    public void SwitchToPlatforming()
    {
        ToggleMode(GameMode.Platforming);
    }

    public void SwitchToMenu()
    {
        ToggleMode(GameMode.Menu);
    }

    public void SwitchToCreation()
    {
        ToggleMode(GameMode.Creation);
    }
}
