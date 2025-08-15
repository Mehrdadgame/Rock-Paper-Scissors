using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Helpers.GameEnums;

public class GameUIOrganism
{
    private ScoreDisplayMolecule scoreDisplay;
    private BattleDisplayMolecule battleDisplay;
    private ChoiceButtonsMolecule choiceButtons;
    private Label resultLabel;
    private Button resetButton;

    public event Action<GameChoice> OnPlayerChoice;
    public event Action OnResetGame;

    public GameUIOrganism(VisualElement parent)
    {
        var container = new VisualElement();
        container.style.flexDirection = FlexDirection.Column;
        container.style.alignItems = Align.Center;
        container.style.justifyContent = Justify.SpaceBetween;
        container.style.width = Length.Percent(100);
        container.style.height = Length.Percent(100);
        container.style.paddingLeft = 20;
        container.style.paddingRight = 20;
        container.style.paddingTop = 20;
        container.style.paddingBottom = 20;

        // Initialize molecules
        scoreDisplay = new ScoreDisplayMolecule(container);
        battleDisplay = new BattleDisplayMolecule(container);

        // Result label
        resultLabel = new Label("Make your choice!");
        resultLabel.style.fontSize = 20;
        resultLabel.style.color = Color.white;
        resultLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
        resultLabel.style.marginTop = 20;
        resultLabel.style.marginBottom = 20;
        resultLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        container.Add(resultLabel);

        choiceButtons = new ChoiceButtonsMolecule(container);

        // Reset button
        resetButton = new Button();
        resetButton.text = "New Game";
        resetButton.style.backgroundColor = new Color(0.3f, 0.69f, 0.31f); // #4CAF50
        resetButton.style.color = Color.white;
        resetButton.style.borderTopLeftRadius = 5;
        resetButton.style.borderTopRightRadius = 5;
        resetButton.style.borderBottomLeftRadius = 5;
        resetButton.style.borderBottomRightRadius = 5;
        resetButton.style.paddingLeft = 20;
        resetButton.style.paddingRight = 20;
        resetButton.style.paddingTop = 10;
        resetButton.style.paddingBottom = 10;
        resetButton.style.borderLeftWidth = 0;
        resetButton.style.borderRightWidth = 0;
        resetButton.style.borderTopWidth = 0;
        resetButton.style.borderBottomWidth = 0;
        resetButton.style.fontSize = 16;
        resetButton.style.marginTop = 20;
        container.Add(resetButton);

        // Wire up events
        choiceButtons.OnChoiceSelected += (choice) => OnPlayerChoice?.Invoke(choice);
        resetButton.clicked += () => OnResetGame?.Invoke();

        parent.Add(container);
    }

    public void UpdateGameState(GameState gameState, Sprite playerSprite, Sprite botSprite)
    {
        scoreDisplay.UpdateScores(gameState.playerScore, gameState.botScore);
        battleDisplay.UpdateChoices(playerSprite, botSprite);
        battleDisplay.SetBattleResult(gameState.lastResult);
        choiceButtons.SetButtonsEnabled(!gameState.gameEnded);
        choiceButtons.HighlightChoice(gameState.playerChoice);

        UpdateResultText(gameState);
    }

    private void UpdateResultText(GameState gameState)
    {
        if (gameState.gameEnded)
        {
            string winner = gameState.playerScore > gameState.botScore ? "Player" : "Bot";
            resultLabel.text = $"🎉 {winner} Wins the Game! 🎉";
            resultLabel.style.color = gameState.playerScore > gameState.botScore ? Color.green : Color.red;
        }
        else
        {
            resultLabel.text = gameState.lastResult switch
            {
                GameResult.PlayerWin => "You Win This Round!",
                GameResult.BotWin => "Bot Wins This Round!",
                GameResult.Draw => "It's a Draw!",
                _ => "Make your choice!"
            };

            resultLabel.style.color = gameState.lastResult switch
            {
                GameResult.PlayerWin => Color.green,
                GameResult.BotWin => Color.red,
                GameResult.Draw => Color.yellow,
                _ => Color.white
            };
        }
    }
}