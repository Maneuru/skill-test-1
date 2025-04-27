using UnityEngine;

/// <summary>A class that inherits Character and adapts to player input</summary>
public class Player : Character
{
    private void Update()
    {
        // Get horizontal input
        movement = Input.GetAxis("Horizontal");

        // Check if can jump then if should
        if (isGrounded && !shouldJump)
        {
            shouldJump = Input.GetButton("Jump");
        }

        UpdateAnimator();
    }

    protected void FixedUpdate()
    {
        // Apply horizontal movement to rigidbody
        rigidbody2D.linearVelocityX = movement * movementSpeed;

        DetectGround();
        // If should jump them jump
        if (shouldJump)
        {
            Jump();
        }
    }

    protected override void PerformAction() {}
}
