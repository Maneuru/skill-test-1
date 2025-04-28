using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;

    private void Awake()
    {
        // Ensure gameScene is correctly assigned
        // if (!SceneManager.GetSceneByName(gameSceneName).IsValid())
        // {
        //     // Then throw an exception to interrupt MaiMenu execution
        //     var msg = $"{GetType().Name} needs a valid scene name for {nameof(gameSceneName)}. In gameObject: {name}";
        //     throw new(msg);
        // }
    }

    /// <summary>Navigate to gameScene</summary>
    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    /// <summary>Quit application</summary>
    public static void Quit()
    {
        Application.Quit();
    }
}
