using System;
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
    public Health Health => health;
    public bool isMoving => movement != 0;
    public float distanceToGround => collider2D.size.y / 2 + 0.02f;

    private void Awake()
    {
        // Get all references
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CapsuleCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Ensure there's the character data
        if (data == null)
        {
            // Then throw an exception to interrupt Character execution
            var msg = $"{GetType().Name} needs a {nameof(CharacterData)} reference. In gameObject: {name}";
            throw new MissingReferenceException(msg);
        }

        // Retrieve all character data
        movementSpeed = data.movementSpeed;
        jumpForce = data.jumpForce;
        groundMask = data.groundMask;
        health = new(this, data.maxHealth);
    }

    /// <summary>Update animator parameters</summary>
    protected void UpdateAnimator()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rigidbody2D.linearVelocity.y);
        if (movement > 0)
        {
            renderer.flipX = false;
        }
        else if (movement < 0)
        {
            renderer.flipX = true;
        }
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
        isGrounded = false;
        shouldJump = false;

        // Apply the force
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>The default action performed by the character</summary>
    protected abstract void PerformAction();

    /// <summary>Apply a force to knockback the character</summary>
    /// <param name="force">The module of the force</param>
    /// <param name="direction">The direction of the force</param>
    /// <param name="ignoreInvulnerability">If the knockback should ignore invulnerability or not</param>
    public void Knokback(float force, Vector3 direction, bool ignoreInvulnerability)
    {
        // Check for invulnerability considering `ignoreInvulnerability`
        if (!ignoreInvulnerability && health.IsInvulnerable)
        {
            return;
        }

        // Apply the force to the rigidbody
        rigidbody2D.AddForce(force * direction, ForceMode2D.Impulse);
    }

    protected void OnDrawGizmosSelected()
    {
        // Check if collider is assigned
        if (collider2D == null)
        {
            collider2D = GetComponent<CapsuleCollider2D>();
        }

        // Draw the ray used by DetectGround()
        Gizmos.DrawRay(transform.position, Vector3.down * distanceToGround);
    }
}
