using UnityEngine;

public class Player : Character
{
    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (isGrounded && !shouldJump)
        {
            shouldJump = Input.GetButton("Jump");
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocityX = movement * movementSpeed;
        if (!isGrounded)
        {
            DetectGround();
        }
        if (shouldJump)
        {
            Jump();
        }
    }

    private void DetectGround()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundMask);
    }

    private void Jump()
    {
        isGrounded = false;
        shouldJump = false;
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    protected override void PerformAttack()
    {

    }
}