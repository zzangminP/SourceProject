using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public BaseState activeState;
    public PatrolState patrolState;
    public bool isStay = false;

    public void Initialise()
    {   if(isStay)
        {
            ChangeState(new StayState());
        }
        else
        {
            ChangeState(new PatrolState());

        }
    }

    private void Update()
    {
        if(activeState != null)
        {
            CheckAnyStateTransition();
            activeState.Execute();

        }
    }

    public void ChangeState(BaseState newState)
    {

        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = newState;

        if (activeState != null)
        {
            //Setup New State
            activeState.stateMachine = this;
            activeState.dummy = GetComponent<Dummy>();
            activeState.Enter();
        }

    }


    public void CheckAnyStateTransition()
    {
        //if(activeState.dummy.hp <= 0)
        //{
        //    activeState.dummy.Die();
        //}

    }

    #region past stateMachine
    //public void Setup(T owner, State<T> entryState)
    //{
    //    ownerEntity = owner;
    //    currentState = null;
    //    previousState = null;
    //    globalState = null;
    //
    //    ChangeState(entryState);
    //}
    //
    //public void Execute()
    //{
    //    if (globalState != null)
    //    {
    //        globalState.Execute(ownerEntity);
    //    }
    //
    //    if (currentState != null)
    //    {
    //        currentState.Execute(ownerEntity);
    //    }
    //
    //}
    //public void ChangeState(State<T> newState)
    //{
    //    if (newState == null) return;
    //
    //    if(currentState != null)
    //    {
    //        previousState = currentState;
    //
    //        currentState.Exit(ownerEntity);
    //
    //    }
    //
    //    currentState = newState;
    //    currentState.Enter(ownerEntity);
    //
    //}
    //
    //public void SetGlobalState(State<T> newState)
    //{
    //    globalState = newState;
    //}


    //public void RevertToPreviousState()
    //{
    //    ChangeState(previousState);
    //}
    #endregion 
}
