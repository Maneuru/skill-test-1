using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.HasComponent<Player>())
        {
            GameEvents.playerWin.Invoke();
        }
    }
}
