using UnityEngine;

/// <summary>Monobehaviour to kill character on contact</summary>
public class Trap : MonoBehaviour
{
    [SerializeField] private int damageDealt;
    [SerializeField] private float knockbackForce;
    [SerializeField] private bool killOnContact;
    [SerializeField] private bool ignoreInvulnerability;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Character character))
        {
            if (killOnContact)
            {
                character.Health.Die(ignoreInvulnerability);
            }
            else
            {
                character.Health.TakeDamage(damageDealt, ignoreInvulnerability);
                Vector3 normal = collision.contacts[0].normal;
                Vector3 inDirection = (transform.position - collision.transform.position).normalized;
                Vector3 knokbackDirection = Vector3.Reflect(inDirection, normal);
                character.Knokback(knockbackForce, knokbackDirection, ignoreInvulnerability);
            }
        }
    }
}
