using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트를 참조

    private IWeaponState currentState;

    void Start()
    {
        // 초기 상태를 DrawState로 설정
        SetState(new DrawState());
    }

    void Update()
    {
        // 현재 상태의 UpdateState 메서드 호출
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void SetState(IWeaponState newState)
    {
        // 현재 상태에서 나갈 때 ExitState 호출
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        // 새로운 상태로 전환
        currentState = newState;

        // 새로운 상태에 들어갈 때 EnterState 호출
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
