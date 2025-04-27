using UnityEngine;

/// <summary>Monobehaviour to kill character on contact</summary>
public class CharacterKiller : MonoBehaviour
{
    [SerializeField] private bool ignoreInvulnerability = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Character character))
        {
            character.Health.Die(ignoreInvulnerability);
        }
    }
}
