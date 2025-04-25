using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectibleData", menuName = "Game/Collectible Data")]
public class CollectibleData : ScriptableObject
{
    [Header("Basic Properties")]
    new public string name;
    public int points;

    [Header("Special Effect")]
    public EffectType specialEffect;
    public int effectValue;
    public float effectDuration;
}
