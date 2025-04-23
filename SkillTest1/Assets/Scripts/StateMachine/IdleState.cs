using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Character character) : base(character) {}

    public override void Enter()
    {
        // character.animator.SetFloat()
    }

    public override void Update()
    {
        // if (character.rigidbody2D )
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}