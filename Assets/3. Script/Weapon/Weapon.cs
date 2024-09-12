using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class Weapon<T> : WeaponSetting where T : Weapon<T>
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
    /// view model �ϰ� world model�ϰ� ��� �����Ҳ��� �����غ�����..
    /// 
    /// 
    /// 
    /// 

    /// 0909 �������� �����ϱ����� Update���ϰ� Shoot ��� �ּ�ó����




    public TMP_Text Ammo_ui { get; set; }

    public GameObject v_model;
    public GameObject w_model;


    // strategy
    public ILeftClick<T>            leftClick;
    public IRightClick<T>           rightClick;
    public IReloadClick<T>          reloadClick;
    public IWASDMove                wasdMove;


    void Start()
    {
        // �ʱ� ���¸� DrawState�� ����     

    //    animator = GetComponent<Animator>();
    //    fpsCam = GetComponentInParent<Camera>();
    //    GameObject.Find("Ammo").TryGetComponent<TMP_Text>(out TMP_Text Ammo_ui);
        SetState(new DrawState<T>());
        UIInit();
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
        UIInit();
    }

    private void UIInit()
    {
        Ammo_ui.text = $"{currentAmmo} || {maxAmmo}";
    }


    public void Left(T weapon)     { leftClick.OnLeftClick(weapon); }
    public void Right(T weapon)    { rightClick.OnRigtClick(weapon); }
    public void Reload(T weapon)   { reloadClick.OnReloadClick(weapon); }
    public void WASD(T weapon)     { wasdMove.DefaultWASDMove(weapon); }


    public void SetState(IWeaponState<T> newState)
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
