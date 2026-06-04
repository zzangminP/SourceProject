public abstract class State<T> where T : class
{

    //public Dummy dummy;
    //public StateMachine stateMachine;

    public abstract void Enter(T entity);

    public abstract void Execute(T entity);

    public abstract void Exit(T entity);

}
