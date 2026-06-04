public abstract class BaseState
{
    public Dummy dummy;
    public StateMachine stateMachine;

    public abstract void Enter();

    public abstract void Execute();

    public abstract void Exit();

}
