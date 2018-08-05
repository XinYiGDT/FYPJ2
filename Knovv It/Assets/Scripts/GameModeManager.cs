using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Standard,
    SuddenDeath,
    Topic
}


public class GameModeManager : Singleton<GameModeManager> {

    public GameMode currentGameMode;

    //Some sort of gamemode base.

}
