using System.Collections;
using UnityEngine;
using TMPro;


public class Weapon : WeaponSetting
{
    public enum EWeaponState
    {
        Draw = 0,
        Idle,
        Fire,
        Reload
    
    }



    /// 
    /// 
    /// 
    /// view model 하고 world model하고 어떻게 관리할껀지 생각해봐야함..
    /// 
    /// 
    /// 
    /// 

    /// 0909 전략패턴 적용하기위해 Update문하고 Shoot 잠깐 주석처리함




    public TMP_Text Ammo_ui { get; set; }

    public GameObject v_model;
    public GameObject w_model;

    public PlayerControl playerControl;

    public Transform weaponHolder;
    public GameObject weaponCamera;
    public Animator animator_w;

    // strategy
    public ILeftClick            leftClick;
    public IRightClick           rightClick;
    public IReloadClick          reloadClick;
    public IWASDMove                wasdMove;



    void Start()
    {
        Init();

        
    }
    private void Update()
    {

    }

    private void Init()
    {
        SetState(new DrawState());
        UIInit();
        playerControl = GetComponentInParent<PlayerControl>();

    }

    public virtual void DropInit(WeaponWorldDrop drop)
    {
        type = drop.type;
        maxAmmo = drop.maxAmmo;
        currentAmmo = drop.currentAmmo;
    }


    private void OnEnable()
    {
        UIInit();
    }

    private void UIInit()
    {
        Ammo_ui.text = $"{currentAmmo} || {maxAmmo}";
    }


    public void Left(Weapon weapon)     { leftClick.OnLeftClick(weapon); }
    public void Right(Weapon weapon)    { rightClick.OnRightClick(weapon); }
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
        Debug.Log(stateInfo);
        if (!stateInfo.IsName(animationClip))
        {
            animator.CrossFadeInFixedTime(animationClip, 0f);
        }
    }
    public void PlayAnimationAuto(string animationClip)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(animationClip))
        {
            animator.CrossFadeInFixedTime(animationClip, 0.15f,-1,0f);
        }

    }
}
