using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneAsset gameScene;

    private void Awake()
    {
        // Ensure gameScene is assigned
        if (gameScene == null)
        {
            // Then log error and throw an exception to interrupt Collector execution
            Debug.LogError($"MainMenu needs a gameScene reference in {name}");
            throw new MissingReferenceException();
        }
    }

    /// <summary>Navigate to gameScene</summary>
    public void Play()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    /// <summary>Quin application</summary>
    public static void Quit()
    {
        Application.Quit();
    }
}
