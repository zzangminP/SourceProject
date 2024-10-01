using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{

    public int waypointIndex;
    public float waitTimer;

    public override void Enter()
    {

    }

    public override void Execute()
    {
        PatrolCycle();

    }

    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        if(dummy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {


                if (waypointIndex < dummy.path.wayPoints.Count - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
                dummy.Agent.SetDestination(dummy.path.wayPoints[waypointIndex].position);
            }
        }
    }
}
