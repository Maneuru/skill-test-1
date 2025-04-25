using System;
using System.Collections;
using UnityEngine;

[Serializable]
/// <summary>Struct to store Health and handle healing, damage and invulnerability</summary>
public struct Health
{
    // Private fields
    private int maxHealth; // The maximun amount of health
    private int currentHealth; // The current amount of health
    private bool isInvulnerable; // Whether the character is invulnerable or not
    private Coroutine invulnerabilityCoroutine; // Reference to invulnerability timer

    // Readonly attributes
    public readonly int CurrentHealth => currentHealth;
    public readonly bool IsInvulnerable => isInvulnerable;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        isInvulnerable = false;
        invulnerabilityCoroutine = null;
    }

    /// <summary>Recover a certain amount of health</summary>
    /// <param name="amount">The amount of health recovered</param>
    public void Heal(int amount)
    {
        // Ensure to not heal a negative amount of health
        if (amount < 0)
        {
            return;
        }

        // Apply `amount` to current health ensuring to not exceed `maxHealth`
        currentHealth = Math.Min(currentHealth + amount, maxHealth);
    }

    /// <summary>Deal damage to health</summary>
    /// <param name="damage"></param>
    /// <returns>True if target could take damage, false otherwise</returns>
    public bool TakeDamage(int damage)
    {
        // Ensure the target is vulnerable
        if (isInvulnerable)
        {
            return false;
        }

        // Ensure to not deal a negative amount of damage
        if (damage > 0)
        {
            // Apply `damage` to current health ensuring to not go below 0
            currentHealth = Math.Max(currentHealth - damage, 0);
        }

        return true;
    }

    /// <summary></summary>
    public void Die()
    {

    }

    /// <summary>Activate <param name="caller">as</param> invulnerability</summary>
    /// <param name="caller">The MonoBehavior which calls the method</param>
    /// <param name="duration">The duration in seconds of the invulnerability</param>
    public void ActivateInvulnerability(MonoBehaviour caller, float duration)
    {
        // Set invulnerability to true
        isInvulnerable = true;

        // Ensure there isn't another timer
        if (invulnerabilityCoroutine != null)
        {
            caller.StopCoroutine(invulnerabilityCoroutine);
        }

        // Start timer for invulnerability desactivation
        invulnerabilityCoroutine = caller.StartCoroutine(WaitInvulnerabilityEnd(duration));
    }

    private IEnumerator WaitInvulnerabilityEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }
}
