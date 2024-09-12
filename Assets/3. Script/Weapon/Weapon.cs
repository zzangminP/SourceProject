using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

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
    /// view model �ϰ� world model�ϰ� ��� �����Ҳ��� �����غ�����..
    /// 
    /// 
    /// 
    /// 

    /// 0909 �������� �����ϱ����� Update���ϰ� Shoot ��� �ּ�ó����

    //public IWeaponState currentState;
    //
    //public Animator animator { get; set; }
    //
    //
    //
    //
    ///// <summary>
    ///// Physics
    ///// </summary>
    //
    //public int   damage             { get; set; }
    //public float range              { get; set; }
    //public float impactForce        { get; set; }
    ////public bool isAuto              { get; set; }
    //public float fireRate           { get; set; }
    //public float lastFireTime       { get; set; }
    //
    //
    ///// <summary>
    ///// Ammo
    ///// </summary>
    //
    //public int   maxMag             { get; set; }
    //public int   maxAmmo            { get; set; }
    //public int   currentAmmo        { get; set; }
    //
    //
    //
    //public int   cost               { get; set; }
    //
    //
    //public Camera fpsCam            { get; set; }



    public TMP_Text Ammo_ui { get; set; }

    public GameObject v_model;
    public GameObject w_model;


    // strategy
    public ILeftClick       leftClick;
    public IRightClick      rightClick;
    public IReloadClick     reloadClick;
    public IWASDMove        wasdMove;


    void Start()
    {
        // �ʱ� ���¸� DrawState�� ����     

    //    animator = GetComponent<Animator>();
    //    fpsCam = GetComponentInParent<Camera>();
    //    GameObject.Find("Ammo").TryGetComponent<TMP_Text>(out TMP_Text Ammo_ui);
        SetState(new DrawState());
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
