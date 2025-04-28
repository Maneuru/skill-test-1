using UnityEngine;

/// <summary>Monobehaviour that handle collection of collectible items</summary>
public class Collector : MonoBehaviour
{
    // Private fields
    private Character character; // Character who collects

    private void Awake()
    {
        // Ensure there's a character componet in the GameObject
        if (!TryGetComponent(out character))
        {
            // Then throw an exception to interrupt Collector execution
            var msg = $"{GetType().Name} needs a {nameof(Character)} component. In gameObject: {name}";
            throw new MissingComponentException(msg);
        }
    }

    public void CollectItem(CollectibleData data)
    {
        GameManager.Instance.AddPoints(data.points);
        switch (data.specialEffect)
        {
            case EffectType.None:
                break;
            case EffectType.Heal:
                character.Health.Heal(data.effectValue);
                break;
            case EffectType.Invulnerability:
                character.Health.ActivateInvulnerability(data.effectDuration);
                break;
        }
    }
}