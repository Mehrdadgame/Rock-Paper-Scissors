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
    private VisualElement rootParent;

    public event Action OnNewGameRequested;

    public GameEndPopup(VisualElement parent)
    {
        rootParent = parent;
        CreatePopup();
        Hide();
    }

    private void CreatePopup()
    {
        // Create overlay as the topmost element
        popupOverlay = new VisualElement();
        popupOverlay.name = "popup-overlay";
        popupOverlay.style.position = Position.Absolute;
        popupOverlay.style.left = 0;
        popupOverlay.style.top = 0;
        popupOverlay.style.right = 0;
        popupOverlay.style.bottom = 0;
        popupOverlay.style.backgroundColor = new Color(0, 0, 0, 0.85f);
        popupOverlay.style.alignItems = Align.Center;
        popupOverlay.style.justifyContent = Justify.Center;
        popupOverlay.style.display = DisplayStyle.None;

        // Ensure overlay is on top
        popupOverlay.pickingMode = PickingMode.Position;
        popupOverlay.focusable = true;

        // Popup container
        popupContainer = new VisualElement();
        popupContainer.name = "popup-container";
        popupContainer.style.backgroundColor = new Color(0.15f, 0.15f, 0.25f, 0.98f);
        popupContainer.style.borderTopLeftRadius = 25;
        popupContainer.style.borderTopRightRadius = 25;
        popupContainer.style.borderBottomLeftRadius = 25;
        popupContainer.style.borderBottomRightRadius = 25;
        popupContainer.style.paddingLeft = 50;
        popupContainer.style.paddingRight = 50;
        popupContainer.style.paddingTop = 40;
        popupContainer.style.paddingBottom = 40;
        popupContainer.style.alignItems = Align.Center;
        popupContainer.style.minWidth = 350;
        popupContainer.style.borderLeftWidth = 4;
        popupContainer.style.borderRightWidth = 4;
        popupContainer.style.borderTopWidth = 4;
        popupContainer.style.borderBottomWidth = 4;
        popupContainer.style.borderLeftColor = new Color(1f, 1f, 1f, 0.8f);
        popupContainer.style.borderRightColor = new Color(1f, 1f, 1f, 0.8f);
        popupContainer.style.borderTopColor = new Color(1f, 1f, 1f, 0.8f);
        popupContainer.style.borderBottomColor = new Color(1f, 1f, 1f, 0.8f);

        // Ensure container is clickable
        popupContainer.pickingMode = PickingMode.Position;

        // Winner label
        winnerLabel = new Label();
        winnerLabel.name = "winner-label";
        winnerLabel.style.fontSize = 32;
        winnerLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
        winnerLabel.style.color = Color.white;
        winnerLabel.style.marginBottom = 20;
        winnerLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        winnerLabel.style.whiteSpace = WhiteSpace.Normal;

        // Score label
        scoreLabel = new Label();
        scoreLabel.name = "score-label";
        scoreLabel.style.fontSize = 18;
        scoreLabel.style.color = new Color(0.9f, 0.9f, 0.9f);
        scoreLabel.style.marginBottom = 30;
        scoreLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        scoreLabel.style.whiteSpace = WhiteSpace.Normal;

        // New game button - make it very clickable
        newGameButton = new Button();
        newGameButton.name = "new-game-button";
        newGameButton.text = " New Game";
        newGameButton.style.backgroundColor = new Color(0.2f, 0.7f, 0.2f);
        newGameButton.style.color = Color.white;
        newGameButton.style.borderTopLeftRadius = 12;
        newGameButton.style.borderTopRightRadius = 12;
        newGameButton.style.borderBottomLeftRadius = 12;
        newGameButton.style.borderBottomRightRadius = 12;
        newGameButton.style.paddingLeft = 40;
        newGameButton.style.paddingRight = 40;
        newGameButton.style.paddingTop = 18;
        newGameButton.style.paddingBottom = 18;
        newGameButton.style.fontSize = 18;
        newGameButton.style.unityFontStyleAndWeight = FontStyle.Bold;
        newGameButton.style.borderLeftWidth = 0;
        newGameButton.style.borderRightWidth = 0;
        newGameButton.style.borderTopWidth = 0;
        newGameButton.style.borderBottomWidth = 0;
        newGameButton.style.minWidth = 150;
        newGameButton.style.minHeight = 50;

        // Ensure button is fully interactive
        newGameButton.pickingMode = PickingMode.Position;
        newGameButton.focusable = true;
        newGameButton.SetEnabled(true);

        // Add hover effect
        newGameButton.RegisterCallback<MouseEnterEvent>(evt =>
        {
            newGameButton.style.backgroundColor = new Color(0.1f, 0.6f, 0.1f);
            newGameButton.style.scale = new Scale(new Vector3(1.05f, 1.05f, 1f));
        });

        newGameButton.RegisterCallback<MouseLeaveEvent>(evt =>
        {
            newGameButton.style.backgroundColor = new Color(0.2f, 0.7f, 0.2f);
            newGameButton.style.scale = new Scale(Vector3.one);
        });

        // Button click handler
        newGameButton.clicked += HandleNewGameClick;

        // Add elements to popup
        popupContainer.Add(winnerLabel);
        popupContainer.Add(scoreLabel);
        popupContainer.Add(newGameButton);
        popupOverlay.Add(popupContainer);

        // Add to root parent as the last child (topmost)
        rootParent.Add(popupOverlay);
    }

    private void HandleNewGameClick()
    {
        Debug.Log("New Game button clicked successfully!");
        Hide();
        OnNewGameRequested?.Invoke();
    }

    public void Show(GameState gameState)
    {
        string winner = gameState.playerScore > gameState.botScore ? "You Win!" : "Bot Wins!";
        Color winnerColor = gameState.playerScore > gameState.botScore ?
            new Color(0.2f, 0.8f, 0.2f) : new Color(0.8f, 0.2f, 0.2f);

        winnerLabel.text = $" {winner}";
        winnerLabel.style.color = winnerColor;

        scoreLabel.text = $"Final Score\nPlayer {gameState.playerScore} - Bot {gameState.botScore}";

        // Show popup
        popupOverlay.style.display = DisplayStyle.Flex;
        popupOverlay.style.visibility = Visibility.Visible;

        // Bring to front
        popupOverlay.BringToFront();

        // Focus the button to ensure it's interactive
        newGameButton.Focus();

        Debug.Log($"Popup shown - Winner: {winner}");
    }

    public void Hide()
    {
        if (popupOverlay != null)
        {
            popupOverlay.style.display = DisplayStyle.None;
            popupOverlay.style.visibility = Visibility.Hidden;
        }
        Debug.Log("Popup hidden successfully");
    }

    public void Destroy()
    {
        if (popupOverlay != null && rootParent != null)
        {
            rootParent.Remove(popupOverlay);
        }
    }
}