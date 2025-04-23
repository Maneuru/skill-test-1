public abstract class BaseState
{
    protected Character character;

    public BaseState(Character character)
    {
        this.character = character;
    }

    public abstract void Enter();
    public virtual void Update() {}
    public virtual void FixedUpdate() {}
    public abstract void Exit();
}
