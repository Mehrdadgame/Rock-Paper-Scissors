using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using static Helpers.GameEnums;

public class GameService : IGameService
{
    private const int WINNING_SCORE = 5;


    public bool IsGameComplete(GameState gameState)
    {
        return gameState.playerScore >= WINNING_SCORE || gameState.botScore >= WINNING_SCORE;
    }

    GameEnums.GameResult IGameService.EvaluateRound(GameChoice playerChoice, GameChoice botChoice)
    {
        if (playerChoice == botChoice)
            return GameEnums.GameResult.Draw;

        // Rock beats Scissors, Scissors beats Paper, Paper beats Rock
        bool playerWins = (playerChoice == GameChoice.Rock && botChoice == GameChoice.Scissors) ||
                         (playerChoice == GameChoice.Scissors && botChoice == GameChoice.Paper) ||
                         (playerChoice == GameChoice.Paper && botChoice == GameChoice.Rock);

        return playerWins ? GameEnums.GameResult.PlayerWin : GameEnums.GameResult.BotWin;
    }
}