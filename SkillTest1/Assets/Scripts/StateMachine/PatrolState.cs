using UnityEngine;

public class PatrolState : BaseState
{
    private Vector2 destination;

    public PatrolState(Character character, Vector2 destination) : base(character)
    {
        this.destination = destination;
    }

    public override void Enter() {}

    public override void Update()
    {
        float destinationDistance = destination.x - character.transform.position.x;
        if (Mathf.Abs(destinationDistance) > 0.2f)
        {
            destinationDistance = Mathf.Clamp(destinationDistance, -1, 1);

        }
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}