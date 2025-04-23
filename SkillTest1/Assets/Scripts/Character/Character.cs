using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Character : MonoBehaviour
{
    new public Rigidbody2D rigidbody2D { get; private set; }
    public Animator animator { get; private set; }
    new public SpriteRenderer renderer { get; private set;}

    [Header("Movement")]
    protected float movement;
    protected float movementSpeed;
    protected float jumpForce;
    protected LayerMask groundMask;

    [Header("Health")]
    protected Health health;

    [Header("Info")]
    protected bool shouldJump;
    protected bool isGrounded;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    protected abstract void PerformAttack();
}
