using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
/// <summary>Abstract class to handle generic character behaviour. Also for Physics and animations</summary>
public abstract class Character : MonoBehaviour
{
    // Component references
    new public Rigidbody2D rigidbody2D { get; private set; } // Reference to RigidBody
    new public CapsuleCollider2D collider2D { get; private set;} // Reference to Collider
    new public SpriteRenderer renderer { get; private set;} // Reference to Renderer
    public Animator animator { get; private set; } // Reference to Animator

    [Header("Character Data")]
    [SerializeField] private CharacterData data; // Reference to character data

    // Private fields
    protected float movementSpeed; // The speed at which character moves
    protected float jumpForce; // The force applied at jump
    protected LayerMask groundMask; // The layer mask for ground check
    protected Health health;

    // Info
    protected float movement; // Horizontal movement intention(input) applied the next fixed update
    protected bool shouldJump; // Wheter the character should jump the next fixed updater
    protected bool isGrounded; // Whether the character is on the ground

    // Readonly properties
    public bool isMoving => movement != 0;
    public float distanceToGround => collider2D.size.y / 2 + 0.02f;

    private void Awake()
    {
        // Get all references
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CapsuleCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Retrieve all character data
        movementSpeed = data.movementSpeed;
        jumpForce = data.jumpForce;
        groundMask = data.groundMask;
        health = new(data.maxHealth);
    }

    /// <summary>Update animator parameters</summary>
    protected void UpdateAnimator()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rigidbody2D.linearVelocity.y);
        renderer.flipX = movement < 0;
    }

    /// <summary>Detect the ground below the character</summary>
    protected void DetectGround()
    {
        // Update `isGrounded` with the result of the raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, groundMask);
    }

    /// <summary>Apply the jump force to the rigid body</summary>
    protected void Jump()
    {
        // Reset the intention to jump and set is not grounded
        shouldJump = false;
        isGrounded = false;

        // Apply the force
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>The default action performed by the character</summary>
    protected abstract void PerformAction();

    protected void OnDrawGizmosSelected()
    {
        if (collider2D == null)
        {
            collider2D = GetComponent<CapsuleCollider2D>();
        }
        Gizmos.DrawRay(transform.position, Vector3.down * distanceToGround);
    }
}
