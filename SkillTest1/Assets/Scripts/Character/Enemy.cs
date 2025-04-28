using UnityEngine;

public class Enemy : Character
{
    [Header("Attack options")]
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask targetMask;

    // Private fields
    private Transform target;

    // Readonly properties
    public bool isChasing => target != null;

    protected override void PerformAction()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (isChasing)
        {
            movement = target.position.x.CompareTo(transform.position.x);
        }
    }

    private void FixedUpdate()
    {
        DetectGround();

        if (isChasing)
        {
            FollowTarget();
            CheckForJump();
        }
        else
        {
            CheckForTarget();
        }

        if (shouldJump)
        {
            Jump();
        }
    }

    /// <summary>Check for a nearby target through RayCast</summary>
    private void CheckForTarget()
    {
        // Raycast to movement direction to check for target
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement * Vector3.right, viewDistance, targetMask);

        // Implicitly check if isn't null
        if (hit)
        {
            target = hit.transform;
        }
    }

    /// <summary>Moves in the direction of `target`</summary>
    private void FollowTarget()
    {
        rigidbody2D.linearVelocityX = movement * movementSpeed;
    }

    /// <summary>check for an obstacle or </summary>
    private void CheckForJump()
    {
        if (!isGrounded)
        {
            return;
        }

        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, movement * Vector3.right, collider2D.size.x);
        RaycastHit2D hitTerrain = Physics2D.Raycast(transform.position, (movement * Vector3.right + Vector3.down).normalized, collider2D.size.x * 1.5f);
        shouldJump = hitObstacle || !hitTerrain;
    }
}
