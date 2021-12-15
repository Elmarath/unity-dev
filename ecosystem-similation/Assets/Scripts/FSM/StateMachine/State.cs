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
        // whenever we enter a new state start a coroutine to return to idle state
        animal.goIdle = false;
        animal.StartCoroutine("ReturnToIdleAfter15Sec");

    }

    public virtual void HandleInput()
    {
        // whenever we exit the state stop coroutines
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void Exit()
    {
        animal.StopCoroutine("ReturnToIdleAfter15Sec");
    }

}
