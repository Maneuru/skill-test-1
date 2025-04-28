using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private Player player;

    private void Awake()
    {
        // Ensure reference are assigned
        if (healthText == null || pointsText == null || player == null)
        {
            // Then throw an exception to interrupt Collector execution
            var msg = $"{GetType().Name} is missing a reference. In gameObject: {name}";
            throw new MissingReferenceException(msg);
        }
    }

    private void Start()
    {
        OnHealthChange();
        OnPointsChange();

        GameEvents.healthChange += OnHealthChange;
        GameEvents.pointsChange += OnPointsChange;
    }

    /// <summary>Update UI for health</summary>
    private void OnHealthChange()
    {
        healthText.text = player.Health.CurrentHealth.ToString("D2");
    }

    /// <summary>Update UI for points</summary>
    private void OnPointsChange()
    {
        pointsText.text = $"{GameManager.Instance.gamePoints} / {GameManager.Instance.TargetPoints}";
    }
}
