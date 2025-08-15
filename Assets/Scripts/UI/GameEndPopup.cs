using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEndPopup
{
    private VisualElement popupOverlay;
    private VisualElement popupContainer;
    private Label winnerLabel;
    private Label scoreLabel;
    private Button newGameButton;

    public event Action OnNewGameRequested;

    public GameEndPopup(VisualElement parent)
    {
        CreatePopup(parent);
        Hide();
    }

    private void CreatePopup(VisualElement parent)
    {
        // Overlay background
        popupOverlay = new VisualElement();
        popupOverlay.style.position = Position.Absolute;
        popupOverlay.style.left = 0;
        popupOverlay.style.top = 0;
        popupOverlay.style.right = 0;
        popupOverlay.style.bottom = 0;
        popupOverlay.style.backgroundColor = new Color(0, 0, 0, 0.8f);
        popupOverlay.style.alignItems = Align.Center;
        popupOverlay.style.justifyContent = Justify.Center;
        popupOverlay.style.display = DisplayStyle.None;

        // Popup container
        popupContainer = new VisualElement();
        popupContainer.style.backgroundColor = new Color(0.2f, 0.2f, 0.3f, 0.95f);
        popupContainer.style.borderTopLeftRadius = 20;
        popupContainer.style.borderTopRightRadius = 20;
        popupContainer.style.borderBottomLeftRadius = 20;
        popupContainer.style.borderBottomRightRadius = 20;
        popupContainer.style.paddingLeft = 40;
        popupContainer.style.paddingRight = 40;
        popupContainer.style.paddingTop = 30;
        popupContainer.style.paddingBottom = 30;
        popupContainer.style.alignItems = Align.Center;
        popupContainer.style.minWidth = 300;
        popupContainer.style.maxWidth = 400;
        popupContainer.style.borderLeftWidth = 3;
        popupContainer.style.borderRightWidth = 3;
        popupContainer.style.borderTopWidth = 3;
        popupContainer.style.borderBottomWidth = 3;
        popupContainer.style.borderLeftColor = Color.white;
        popupContainer.style.borderRightColor = Color.white;
        popupContainer.style.borderTopColor = Color.white;
        popupContainer.style.borderBottomColor = Color.white;

        // Winner label
        winnerLabel = new Label();
        winnerLabel.style.fontSize = 28;
        winnerLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
        winnerLabel.style.color = Color.white;
        winnerLabel.style.marginBottom = 15;
        winnerLabel.style.unityTextAlign = TextAnchor.MiddleCenter;

        // Score label
        scoreLabel = new Label();
        scoreLabel.style.fontSize = 18;
        scoreLabel.style.color = new Color(0.8f, 0.8f, 0.8f);
        scoreLabel.style.marginBottom = 25;
        scoreLabel.style.unityTextAlign = TextAnchor.MiddleCenter;

        // New game button
        newGameButton = new Button();
        newGameButton.text = "New Game";
        newGameButton.style.backgroundColor = new Color(0.3f, 0.69f, 0.31f);
        newGameButton.style.color = Color.white;
        newGameButton.style.borderTopLeftRadius = 10;
        newGameButton.style.borderTopRightRadius = 10;
        newGameButton.style.borderBottomLeftRadius = 10;
        newGameButton.style.borderBottomRightRadius = 10;
        newGameButton.style.paddingLeft = 30;
        newGameButton.style.paddingRight = 30;
        newGameButton.style.paddingTop = 15;
        newGameButton.style.paddingBottom = 15;
        newGameButton.style.fontSize = 16;
        newGameButton.style.unityFontStyleAndWeight = FontStyle.Bold;
        newGameButton.style.borderLeftWidth = 0;
        newGameButton.style.borderRightWidth = 0;
        newGameButton.style.borderTopWidth = 0;
        newGameButton.style.borderBottomWidth = 0;

        newGameButton.clicked += () => OnNewGameRequested?.Invoke();

        // Add elements to popup
        popupContainer.Add(winnerLabel);
        popupContainer.Add(scoreLabel);
        popupContainer.Add(newGameButton);
        popupOverlay.Add(popupContainer);
        parent.Add(popupOverlay);
    }

    public void Show(GameState gameState)
    {
        string winner = gameState.playerScore > gameState.botScore ? "You Win" : "Bot Win!";
        Color winnerColor = gameState.playerScore > gameState.botScore ? Color.green : Color.red;

        winnerLabel.text = $"🎉 {winner} 🎉";
        winnerLabel.style.color = winnerColor;

        scoreLabel.text = $"Final Score :  {gameState.playerScore} - Bot {gameState.botScore}";

        popupOverlay.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        popupOverlay.style.display = DisplayStyle.None;
    }
}