public abstract class State
{
    protected Animal animal;
    protected StateMachine stateMachine;

    protected State(Animal animal, StateMachine stateMachine)
    {
        this.animal = animal;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void HandleInput()
    {

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
