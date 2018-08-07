using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    None,
    Standard,
    SuddenDeath,
    Topic,
    Count
}


public class GameModeManager : Singleton<GameModeManager> {

    public GameMode currentGameMode;

    //Gamemode that exist
    GameModeBase[] m_gamemodes;
    //Some sort of gamemode base.

    private void Awake()
    {
        FindGameModes();
        CheckCurrentGameMode();
        InitSelectedGameMode();
    }


    void FindGameModes()
    {
        m_gamemodes = new GameModeBase[(int)GameMode.Count];
        m_gamemodes[0] = GetComponent<GameModeStandard>();
    }

    void CheckCurrentGameMode()
    {
        if (currentGameMode == GameMode.None)
            currentGameMode = GameMode.Standard;

    }

    void InitSelectedGameMode()
    {
        for (int i = 0; i < m_gamemodes.Length; ++i)
        {
            if (i == (int)currentGameMode - 1)
            {
                m_gamemodes[i].SetUp();
            }
            else
            {
                m_gamemodes[i].TearDown();
            }
        }
    }
}

