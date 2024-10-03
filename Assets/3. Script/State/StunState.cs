using UnityEngine;

public class StunState : BaseState
{
    private float stunDuration = 7f; // 스턴 지속 시간
    private float timer;

    public override void Enter()
    {
        // 스턴 상태에 진입할 때 타이머를 초기화하고, NavMeshAgent를 멈춤
        timer = stunDuration;
        dummy.Agent.isStopped = true; // NavMeshAgent 멈춤
        Debug.Log("Entered Stun State");
    }

    public override void Execute()
    {
        // 스턴 타이머가 끝날 때까지 대기
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (stateMachine.isStay)
            {
                stateMachine.ChangeState(new StayState());
            }
            else
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {
        // 스턴 상태에서 벗어나면 다시 NavMeshAgent를 작동
        dummy.Agent.isStopped = false;
        Debug.Log("Exited Stun State");
    }
}
