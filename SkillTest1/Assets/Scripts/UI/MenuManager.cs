using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Singleton that handles different UI</summary>
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Menu References")]
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private GameObject defaultMenu;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loose;

    // Readonly properties
    public bool isUsed => defaultMenu.activeSelf; // Whether there's already a menu opened
    public bool isPaused => pause.activeSelf; // Whether the opened menu is pauseMenu or not

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

        // Ensure maim menu scene is correctly assigned
        // if (!SceneManager.GetSceneByName(mainMenuSceneName).IsValid())
        // {
        //     // Then throw an exception to interrupt MenuManager execution
        //     var msg = $"{GetType().Name} needs a valid scene name for {nameof(mainMenuSceneName)}. In gameObject: {name}";
        //     throw new(msg);
        // }
    }

    private bool ActivateDefault()
    {
        if (isUsed)
        {
            return false;
        }

        Time.timeScale = 0;
        defaultMenu.SetActive(true);
        return true;
    }

    public void PauseMenu()
    {
        if (!ActivateDefault())
        {
            return;
        }

        pause.SetActive(true);
    }

    public void GameWinMenu()
    {
        if (!ActivateDefault())
        {
            return;
        }

        win.SetActive(true);
    }

    public void GameLooseMenu()
    {
        if (!ActivateDefault())
        {
            return;
        }

        loose.SetActive(true);
    }

    public void CloseCurrentMenu()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void Restart()
    {
        CloseCurrentMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainMenu()
    {
        CloseCurrentMenu();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void Resume()
    {
        CloseCurrentMenu();
    }
}
