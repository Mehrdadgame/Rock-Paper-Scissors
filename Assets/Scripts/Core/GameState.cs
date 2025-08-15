using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using static Helpers.GameEnums;
public class GameState
{
    public int playerScore;
    public int botScore;
    public GameChoice playerChoice;
    public GameChoice botChoice;
    public GameResult lastResult;
    public bool gameEnded;
}
