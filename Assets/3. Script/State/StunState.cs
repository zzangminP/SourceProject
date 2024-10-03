using UnityEngine;

public class StunState : BaseState
{
    private float stunDuration = 7f; // ���� ���� �ð�
    private float timer;

    public override void Enter()
    {
        // ���� ���¿� ������ �� Ÿ�̸Ӹ� �ʱ�ȭ�ϰ�, NavMeshAgent�� ����
        timer = stunDuration;
        dummy.Agent.isStopped = true; // NavMeshAgent ����
        Debug.Log("Entered Stun State");
    }

    public override void Execute()
    {
        // ���� Ÿ�̸Ӱ� ���� ������ ���
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
        // ���� ���¿��� ����� �ٽ� NavMeshAgent�� �۵�
        dummy.Agent.isStopped = false;
        Debug.Log("Exited Stun State");
    }
}
