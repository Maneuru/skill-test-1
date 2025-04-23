using UnityEngine;

public class ChaseState : BaseState
{
    private Transform target;

    public ChaseState(Character character, Transform target) : base(character)
    {
        this.target = target;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdate()
    {
        // if (target.position.x < character.transform.position.x)
        // {
        //     character.rigidbody2D.linearVelocityX = -1 * character.MovementSpeed;
        // }
        // else
        // {
        //     character.rigidbody2D.linearVelocityX = character.MovementSpeed;
        // }

        // if (CheckForAttack())
        // {

        // }
    }

    public bool CheckForAttack()
    {
        // Vector2 direction =
        // if (Physics2D.Raycast(character.transform.position, ))
        return true;
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}