using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public enum EWeaponState
    //{
    //    Draw = 0,
    //    Idle,
    //    Fire,
    //    Reload
    //
    //}

    /// <summary>
    /// 
    /// 
    /// view model 하고 world model하고 어떻게 관리할껀지 생각해봐야함..
    /// 
    /// 
    /// </summary>
    
    public Animator animator;

    private IWeaponState currentState;
    //private EWeaponState eCurrentState;


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
        Debug.Log(currentState);
    }

    public void SetState(IWeaponState newState)
    {
         
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        
        currentState = newState;

        
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
