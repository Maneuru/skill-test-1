using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Private fields
    [SerializeField] private int targetPoints; // The eamount of points needed to win
    public int gamePoints { get; private set; } // Points collected in game

    // Readonly properties
    public bool hasWinCondition => gamePoints >= targetPoints; // Whether the player has satisfied win condition
    public int TargetPoints => targetPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Bind events
        GameEvents.playerDeath += GameLost;
        GameEvents.playerWin += GameWon;

        // Start the game
        StartGame();
    }

    private void Update()
    {
        // Get ESC input
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // Check if there's an opened menu before opening pause menu
            if (!MenuManager.Instance.isUsed)
            {
                MenuManager.Instance.PauseMenu();
            }
            // If the menu opened is pause menu then close it
            else if (MenuManager.Instance.isPaused)
            {
                MenuManager.Instance.CloseCurrentMenu();
            }
        }
    }

    /// <summary>Prepare to play</summary>
    private void StartGame() {/* Nothing at the moment */}

    /// <summary>Notify game has ended with loose result</summary>
    private void GameLost()
    {
        MenuManager.Instance.GameLooseMenu();
    }

    /// <summary>Notify game has ended with win result</summary>
    private void GameWon()
    {
        MenuManager.Instance.GameWinMenu();
    }

    /// <summary>Increment points game points by amount (also negative)</summary>
    /// <param name="amount">The amount of point gained or lost</param>
    public void AddPoints(int amount)
    {
        // Add `amount` to `gamePoints`
        gamePoints += amount;

        // Notify points change
        GameEvents.pointsChange.Invoke();
    }
}