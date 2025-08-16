using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helpers.GameEnums;

public class Interfaces
{

}
public interface IBotStrategy
{
    GameChoice MakeChoice();
}
public interface IGameObserver
{
    void OnRoundComplete(GameState gameState);
    void OnGameEnd(GameState gameState);
}

// Service for game logic
public interface IGameService
{
    GameResult EvaluateRound(GameChoice playerChoice, GameChoice botChoice);
    bool IsGameComplete(GameState gameState);
}