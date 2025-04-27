using System;
using System.Collections;
using UnityEngine;

[Serializable]
/// <summary>Struct to store Health and handle healing, damage and invulnerability</summary>
public struct Health
{
    // Private fields
    private Character character; // The asociated character
    private int maxHealth; // The maximun amount of health
    private int currentHealth; // The current amount of health
    private bool isInvulnerable; // Whether the character is invulnerable or not
    private Coroutine invulnerabilityCoroutine; // Reference to invulnerability timer

    // Readonly attributes
    public readonly int CurrentHealth => currentHealth;
    public readonly int MaxHealth => maxHealth;
    public readonly bool IsInvulnerable => isInvulnerable;

    public Health(Character character, int maxHealth)
    {
        this.character = character;
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
    public bool TakeDamage(int damage, bool ignoreInvulnerability = false)
    {
        // Ensure the target is vulnerable
        if (!ignoreInvulnerability && isInvulnerable)
        {
            return false;
        }

        // Send Message
        character.animator.SetTrigger("Hit");

        // Ensure to not deal a negative amount of damage
        if (damage > 0)
        {
            // Apply `damage` to current health ensuring to not go below 0
            currentHealth = Math.Max(currentHealth - damage, 0);

            // Check if character is still alive
            if (currentHealth == 0)
            {
                GameEvents.playerDeath.Invoke();
            }
        }

        return true;
    }

    /// <summary>Instantly deals fatal famage to character ignoring invulnerability</summary>
    public void Die(bool ignoreInvulnerability = true)
    {
        TakeDamage(currentHealth, ignoreInvulnerability);
    }

    /// <summary>Activate invulnerability</summary>
    /// <param name="caller">The MonoBehavior which calls the method</param>
    /// <param name="duration">The duration in seconds of the invulnerability</param>
    public void ActivateInvulnerability(float duration)
    {
        // Set invulnerability to true
        isInvulnerable = true;

        // Ensure there isn't another timer
        if (invulnerabilityCoroutine != null)
        {
            character.StopCoroutine(invulnerabilityCoroutine);
        }

        // Start timer for invulnerability desactivation
        invulnerabilityCoroutine = character.StartCoroutine(WaitInvulnerabilityEnd(duration));
    }

    private IEnumerator WaitInvulnerabilityEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }
}
