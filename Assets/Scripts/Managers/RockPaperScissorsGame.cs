using UnityEngine;
using UnityEngine.UIElements;
using static Helpers.EnumsHelper;
using static Helpers.GameEnums;
using GameResult = Helpers.GameEnums.GameResult;

public class RockPaperScissorsGame : MonoBehaviour, IGameObserver
{
    [Header("Sprites")]
    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite scissorsSprite;
    [SerializeField] private Sprite defaultSprite;

    // Dependencies (Dependency Inversion)
    private IBotStrategy botStrategy;
    private IGameService gameService;
    private GameState gameState;

    // UI Elements
    private UIDocument uiDocument;
    private Label playerScoreLabel;
    private Label botScoreLabel;
    private VisualElement playerChoiceImage;
    private VisualElement botChoiceImage;
    private Button rockButton;
    private Button paperButton;
    private Button scissorsButton;
    private Label resultLabel;
    private Button resetButton;

    private void Awake()
    {
        // Initialize dependencies
        botStrategy = new RandomBotStrategy();
        gameService = new GameService();
        gameState = new GameState();

        InitializeUI();
    }

    private void Start()
    {
        ResetGame();
    }

    // Initialize UI components (Atomic Design approach)
    private void InitializeUI()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Get UI elements
        playerScoreLabel = root.Q<Label>("player-score");
        botScoreLabel = root.Q<Label>("bot-score");
        playerChoiceImage = root.Q<VisualElement>("player-choice");
        botChoiceImage = root.Q<VisualElement>("bot-choice");
        rockButton = root.Q<Button>("rock-button");
        paperButton = root.Q<Button>("paper-button");
        scissorsButton = root.Q<Button>("scissors-button");
        resultLabel = root.Q<Label>("result-label");
        resetButton = root.Q<Button>("reset-button");

        // Setup button events
        rockButton.clicked += () => PlayerMakeChoice(GameChoice.Rock);
        paperButton.clicked += () => PlayerMakeChoice(GameChoice.Paper);
        scissorsButton.clicked += () => PlayerMakeChoice(GameChoice.Scissors);
        resetButton.clicked += ResetGame;
    }

    // Player choice handler
    private void PlayerMakeChoice(GameChoice choice)
    {
        if (gameState.gameEnded) return;

        gameState.playerChoice = choice;
        gameState.botChoice = botStrategy.MakeChoice();

        PlayRound();
    }

    // Execute a single round
    private void PlayRound()
    {
        gameState.lastResult = gameService.EvaluateRound(gameState.playerChoice, gameState.botChoice);

        UpdateScores();
        UpdateUI();

        if (gameService.IsGameComplete(gameState))
        {
            gameState.gameEnded = true;
            OnGameEnd(gameState);
        }
        else
        {
            OnRoundComplete(gameState);
        }
    }

    // Update game scores
    private void UpdateScores()
    {
        switch (gameState.lastResult)
        {
            case GameResult.PlayerWin:
                gameState.playerScore++;
                break;
            case GameResult.BotWin:
                gameState.botScore++;
                break;
        }
    }

    // Update UI elements
    private void UpdateUI()
    {
        // Update scores
        playerScoreLabel.text = gameState.playerScore.ToString();
        botScoreLabel.text = gameState.botScore.ToString();

        // Update choice images
        UpdateChoiceImage(playerChoiceImage, gameState.playerChoice);
        UpdateChoiceImage(botChoiceImage, gameState.botChoice);

        // Update result text
        UpdateResultText();

        // Toggle buttons based on game state
        ToggleGameButtons(!gameState.gameEnded);
    }

    // Update choice image sprites
    private void UpdateChoiceImage(VisualElement imageElement, GameChoice choice)
    {
        Sprite spriteToUse = choice switch
        {
            GameChoice.Rock => rockSprite,
            GameChoice.Paper => paperSprite,
            GameChoice.Scissors => scissorsSprite,
            _ => defaultSprite
        };

        if (spriteToUse != null)
        {
            imageElement.style.backgroundImage = new StyleBackground(spriteToUse);
        }
    }

    // Update result text based on game state
    private void UpdateResultText()
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

    // Toggle game buttons enabled state
    private void ToggleGameButtons(bool enabled)
    {
        rockButton.SetEnabled(enabled);
        paperButton.SetEnabled(enabled);
        scissorsButton.SetEnabled(enabled);
    }

    // Reset game to initial state
    private void ResetGame()
    {
        gameState = new GameState();

        // Reset choice images to default
        UpdateChoiceImage(playerChoiceImage, GameChoice.None);
        UpdateChoiceImage(botChoiceImage, GameChoice.None);

        UpdateUI();
        resultLabel.text = "Make your choice!";
        resultLabel.style.color = Color.white;
    }

    // IGameObserver implementation
    public void OnRoundComplete(GameState gameState)
    {
        Debug.Log($"Round complete: Player {gameState.playerScore} - Bot {gameState.botScore}");
    }

    public void OnGameEnd(GameState gameState)
    {
        string winner = gameState.playerScore > gameState.botScore ? "Player" : "Bot";
        Debug.Log($"Game ended! Winner: {winner}");
    }
}