using UnityEngine;

public class StunState : BaseState
{
    private float stunDuration = 7f; 
    private float timer;

    public override void Enter()
    {

        timer = stunDuration;
        dummy.Agent.isStopped = true; 
        Debug.Log("Entered Stun State");
    }

    public override void Execute()
    {
        
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (stateMachine.isStay)
            {
                stateMachine.ChangeState(new StayState());
            }
            else
            {
                dummy.Agent.isStopped = false;
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {

        dummy.Agent.isStopped = false;
        Debug.Log("Exited Stun State");
    }
}
