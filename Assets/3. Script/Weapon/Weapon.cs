using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class Weapon : MonoBehaviour
{
    public enum EWeaponState
    {
        Draw = 0,
        Idle,
        Fire,
        Reload
    
    }

    /// <summary>
    /// 
    /// 
    /// view model 하고 world model하고 어떻게 관리할껀지 생각해봐야함..
    /// 
    /// 
    /// </summary>
    /// 

    /// 0909 전략패턴 적용하기위해 Update문하고 Shoot 잠깐 주석처리함
    
    public Animator animator { get; set; }

    public IWeaponState currentState;

    

    /// <summary>
    /// Physics
    /// </summary>
    
    public int   damage             { get; set; }
    public float range              { get; set; }
    public float impactForce        { get; set; }


    /// <summary>
    /// Ammo
    /// </summary>

    public int   maxMag             { get; set; }
    public int   maxAmmo            { get; set; }
    public int   currentAmmo        { get; set; }



    public int   cost               { get; set; }


    public Camera fpsCam            { get; set; }

    public TMP_Text Ammo_ui         { get; set; }

    public GameObject v_model;
    public GameObject w_model;


    // strategy
    public ILeftClick       leftClick;
    public IRightClick      rightClick;
    public IReloadClick     reloadClick;
    public IWASDMove        wasdMove;


    void Start()
    {
        // 초기 상태를 DrawState로 설정     
        // SetState(new DrawState());
        // animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
    }
    private void Update()
    {
        //if(currentState != null)
        //{
        //    currentState.UpdateState(this);
        //}
    }


    public void Left(Weapon weapon)     { leftClick.OnLeftClick(weapon); }
    public void Right(Weapon weapon)    { rightClick.OnRigtClick(weapon); }
    public void Reload(Weapon weapon)   { reloadClick.OnReloadClick(weapon); }
    public void WASD(Weapon weapon)     { wasdMove.DefaultWASDMove(weapon); }


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
