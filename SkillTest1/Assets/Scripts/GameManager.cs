using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Private fields
    private int gamePoints; // Points collected in game

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

        GameEvents.playerDeath += GameLost;
        GameEvents.playerWin += GameWon;
        StartGame();
    }

    private void Update()
    {
        // Get ESC input
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // Check if there's an opened menu before opening pause menu
            if (!UIManager.Instance.isUsed)
            {
                UIManager.Instance.PauseMenu();
            }
            // If the menu opened is pause menu then close it
            else if (UIManager.Instance.isPaused)
            {
                UIManager.Instance.CloseCurrentMenu();
            }
        }
    }

    /// <summary>Prepare to play</summary>
    private void StartGame()
    {
        // Nothing at the moment
    }

    /// <summary>Notify game has ended with loose result</summary>
    private void GameLost()
    {
        UIManager.Instance.GameLooseMenu();
    }

    /// <summary>Notify game has ended with win result</summary>
    private void GameWon()
    {
        UIManager.Instance.GameWinMenu();
    }

    /// <summary>Increment points game points by amount (also negative)</summary>
    /// <param name="amount">The amount of point gained or lost</param>
    public void AddPoints(int amount)
    {
        // Add `amount` to `gamePoints`
        gamePoints += amount;
    }
}