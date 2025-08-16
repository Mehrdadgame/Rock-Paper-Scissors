using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Helpers.GameEnums;

// Main game controller using atomic design pattern
public class RockPaperScissorsGameController : MonoBehaviour, IGameObserver
{
    [Header("Game Sprites")]
    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite scissorsSprite;
    [SerializeField] private Sprite defaultSprite;

    [Header("Audio (Optional)")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip drawSound;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip gameEndSound;

    // Dependencies (SOLID - Dependency Inversion Principle)
    private IBotStrategy botStrategy;
    private IGameService gameService;
    private GameState gameState;

    // UI Components (Atomic Design)
    private UIDocument uiDocument;
    private GameUIOrganism gameUIComponent;
    private AudioSource audioSource;

    // Sprite mapping for choices
    private Dictionary<GameChoice, Sprite> spriteMap;

    private void Awake()
    {
        InitializeDependencies();
        InitializeSpriteMap();
        InitializeAudio();
    }

    private void Start()
    {
        InitializeUI();
        ResetGame();
    }

    // Initialize game dependencies
    private void InitializeDependencies()
    {
        botStrategy = new RandomBotStrategy();
        gameService = new GameService();
        gameState = new GameState();
    }

    // Initialize sprite mapping
    private void InitializeSpriteMap()
    {
        spriteMap = new Dictionary<GameChoice, Sprite>
        {
            { GameChoice.Rock, rockSprite },
            { GameChoice.Paper, paperSprite },
            { GameChoice.Scissors, scissorsSprite },
            { GameChoice.None, defaultSprite }
        };
    }

    // Initialize audio components
    private void InitializeAudio()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Initialize UI using atomic design
    private void InitializeUI()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component not found! Please add it to the GameObject.");
            return;
        }

        var root = uiDocument.rootVisualElement;

        // Create the main UI organism with sprite mapping
        gameUIComponent = new GameUIOrganism(root, spriteMap);

        // Wire up events
        gameUIComponent.OnPlayerChoice += HandlePlayerChoice;
        gameUIComponent.OnResetGame += () =>
        {
            Debug.Log("Reset game event received from UI");
            ResetGame();
        };
    }

    // Handle player choice input
    private void HandlePlayerChoice(GameChoice choice)
    {
        if (gameState.gameEnded) return;

        PlaySound(buttonClickSound);

        gameState.playerChoice = choice;
        gameState.botChoice = botStrategy.MakeChoice();

        PlayRound();
    }

    // Execute a single game round
    private void PlayRound()
    {
        // Evaluate round result
        gameState.lastResult = gameService.EvaluateRound(gameState.playerChoice, gameState.botChoice);

        // Update scores
        UpdateScores();

        // Check for game completion BEFORE updating UI
        if (gameService.IsGameComplete(gameState))
        {
            gameState.gameEnded = true;
            PlaySound(gameEndSound);
            // Update UI after setting gameEnded to true
            UpdateUI();
            OnGameEnd(gameState);
        }
        else
        {
            // Update UI for regular rounds
            UpdateUI();
            // Play result sound for regular rounds
            PlayResultSound(gameState.lastResult);
            OnRoundComplete(gameState);
        }
    }

    // Update game scores based on round result
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

    // Update UI components with current game state
    private void UpdateUI()
    {
        if (gameUIComponent == null) return;

        Sprite playerSprite = GetSpriteForChoice(gameState.playerChoice);
        Sprite botSprite = GetSpriteForChoice(gameState.botChoice);

        // If game state has no choices, use null/default sprites
        if (gameState.playerChoice == GameChoice.None)
            playerSprite = defaultSprite;
        if (gameState.botChoice == GameChoice.None)
            botSprite = defaultSprite;

        gameUIComponent.UpdateGameState(gameState, playerSprite, botSprite);
    }

    // Get sprite for game choice
    private Sprite GetSpriteForChoice(GameChoice choice)
    {
        return spriteMap.TryGetValue(choice, out Sprite sprite) ? sprite : defaultSprite;
    }

    // Play result-specific sound
    private void PlayResultSound(GameResult result)
    {
        AudioClip clipToPlay = result switch
        {
            GameResult.PlayerWin => winSound,
            GameResult.BotWin => loseSound,
            GameResult.Draw => drawSound,
            _ => null
        };

        PlaySound(clipToPlay);
    }

    // Play audio clip
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Reset game to initial state
    private void ResetGame()
    {
        Debug.Log("ResetGame called");

        // Create new game state
        gameState = new GameState();

        // Force hide popup and reset UI
        if (gameUIComponent != null)
        {
            // Hide popup first
            gameUIComponent.HideGameEndPopup();
            // Reset battle display to hide VS label
            gameUIComponent.ResetBattleDisplay();
            // Then update UI with new state
            UpdateUI();
        }

        Debug.Log("Game reset - New game started!");
    }

    // IGameObserver implementation
    public void OnRoundComplete(GameState gameState)
    {
        string resultText = gameState.lastResult switch
        {
            GameResult.PlayerWin => "Player wins round",
            GameResult.BotWin => "Bot wins round",
            GameResult.Draw => "Round draw",
            _ => "Round complete"
        };

        Debug.Log($"{resultText} - Score: Player {gameState.playerScore} - Bot {gameState.botScore}");
    }

    public void OnGameEnd(GameState gameState)
    {
        string winner = gameState.playerScore > gameState.botScore ? "Player" : "Bot";
        Debug.Log($"🎉 Game Complete! Winner: {winner} (Final Score: Player {gameState.playerScore} - Bot {gameState.botScore})");

        // Optional: Add game end effects here
        StartGameEndEffect();
    }

    // Optional game end visual effect
    private void StartGameEndEffect()
    {
        // You can add particle effects, screen shake, or other visual feedback here
        Debug.Log(" Game end celebration effect!");
    }

    // Public methods for external control (if needed)
    public void SetBotStrategy(IBotStrategy newStrategy)
    {
        botStrategy = newStrategy ?? new RandomBotStrategy();
    }

    public GameState GetCurrentGameState()
    {
        return gameState;
    }

    // Update sprite mapping if sprites change at runtime
    public void UpdateSpriteMap(GameChoice choice, Sprite newSprite)
    {
        if (spriteMap.ContainsKey(choice))
        {
            spriteMap[choice] = newSprite;
            gameUIComponent?.UpdateButtonSprites(spriteMap);
        }
    }

    // Validation method for development
    private void OnValidate()
    {
        if (rockSprite == null) Debug.LogWarning("Rock sprite not assigned!");
        if (paperSprite == null) Debug.LogWarning("Paper sprite not assigned!");
        if (scissorsSprite == null) Debug.LogWarning("Scissors sprite not assigned!");
    }
}