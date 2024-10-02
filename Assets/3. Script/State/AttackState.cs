using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;


    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (dummy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(3, 7))
            {
                dummy.Agent.SetDestination(dummy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }


        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());

            }
        }
    }

    public override void Exit()
    {
    }


}
