using UnityEngine;

/// <summary>Monobehaviour to kill character on contact</summary>
public class Trap : MonoBehaviour
{
    [Header("Trap options")]
    [SerializeField] private int damageDealt;
    [SerializeField] private float knockbackForce;
    [SerializeField] private bool applyKnockback = true;
    [SerializeField] private bool killOnContact;
    [SerializeField] private bool ignoreInvulnerability;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.TryGetComponent(out Character character))
        {
            return;
        }

        if (killOnContact)
        {
            Kill(character);
        }
        else
        {
            DealDamage(character, collision);
        }
    }

    private void Kill(Character character)
    {
        character.Health.Die(ignoreInvulnerability);
    }

    private void DealDamage(Character character, Collision2D collision)
    {
        character.Health.TakeDamage(damageDealt, ignoreInvulnerability);
        if (applyKnockback)
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 inDirection = (transform.position - collision.transform.position).normalized;
            Vector3 knokbackDirection = Vector3.Reflect(inDirection, normal);
            character.Knokback(knockbackForce, knokbackDirection, ignoreInvulnerability);
        }
    }
}
