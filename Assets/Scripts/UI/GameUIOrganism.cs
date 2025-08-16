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
    private GameEndPopup gameEndPopup;

    public event Action<GameChoice> OnPlayerChoice;
    public event Action OnResetGame;

    public GameUIOrganism(VisualElement parent, Dictionary<GameChoice, Sprite> spriteMap)
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

        // Choice buttons (now with sprites and no reset button)
        choiceButtons = new ChoiceButtonsMolecule(container, spriteMap);

        // Wire up choice button events
        choiceButtons.OnChoiceSelected += (choice) => OnPlayerChoice?.Invoke(choice);

        // Create game end popup
        gameEndPopup = new GameEndPopup(parent);
        gameEndPopup.OnNewGameRequested += () =>
        {
            Debug.Log("New game requested from popup");
            OnResetGame?.Invoke();
        };

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

        // Show popup only if game ended and it's not already visible
        if (gameState.gameEnded)
        {
            Debug.Log($"Game ended - showing popup! Player: {gameState.playerScore}, Bot: {gameState.botScore}");
            gameEndPopup.Show(gameState);
        }
        else
        {
            // Make sure popup is hidden for active games
            gameEndPopup.Hide();
        }
    }

    private void UpdateResultText(GameState gameState)
    {
        if (gameState.gameEnded)
        {
            // Don't show result text when popup is visible
            resultLabel.style.display = DisplayStyle.None;
        }
        else
        {
            resultLabel.style.display = DisplayStyle.Flex;

            resultLabel.text = gameState.lastResult switch
            {
                GameResult.PlayerWin => "You Win!",
                GameResult.BotWin => "Bot Wins!",
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

    public void UpdateButtonSprites(Dictionary<GameChoice, Sprite> spriteMap)
    {
        choiceButtons.UpdateSprites(spriteMap);
    }

    // Method to manually hide the popup (used during reset)
    public void HideGameEndPopup()
    {
        gameEndPopup?.Hide();
    }

    // Method to reset the battle display
    public void ResetBattleDisplay()
    {
        battleDisplay?.Reset();
    }
}