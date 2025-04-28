using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure win condition is satisfied
        if (!GameManager.Instance.hasWinCondition)
        {
            return;
        }

        // Ensure `other` is the player
        if (!other.HasComponent<Player>())
        {
            return;
        }

        // Notify win
        GameEvents.playerWin.Invoke();
    }
}
