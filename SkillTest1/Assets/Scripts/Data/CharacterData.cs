using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed;
    public float jumpForce;
    public LayerMask groundMask;

    [Header("Health")]
    public int maxHealth;
}
