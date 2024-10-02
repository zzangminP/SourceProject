using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        dummy.Agent.SetDestination(dummy.LastKnowPos);
    }

    public override void Execute()
    {
        if(dummy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());

        }

        if (dummy.Agent.remainingDistance < dummy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;

            if (moveTimer > Random.Range(3, 5))
            {
                dummy.Agent.SetDestination(dummy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }


            if (searchTimer > 10)
            {

                stateMachine.ChangeState(new PatrolState());

            }
        }
    }

    public override void Exit()
    {

    }
}
