using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator; // Animator ������Ʈ�� ����

    private IWeaponState currentState;

    void Start()
    {
        // �ʱ� ���¸� DrawState�� ����
        SetState(new DrawState());
    }

    void Update()
    {
        // ���� ������ UpdateState �޼��� ȣ��
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void SetState(IWeaponState newState)
    {
        // ���� ���¿��� ���� �� ExitState ȣ��
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        // ���ο� ���·� ��ȯ
        currentState = newState;

        // ���ο� ���¿� �� �� EnterState ȣ��
        if (currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    public void PlayAnimation(string animationClip)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(animationClip))
        {
            animator.CrossFadeInFixedTime(animationClip, 0f);
        }
    }
}
