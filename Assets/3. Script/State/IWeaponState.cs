using UnityEditor.IMGUI.Controls;
using UnityEngine;
public interface IWeaponState<T> where T : Weapon<T>
{
    void EnterState(T weapon);
    
    void UpdateState(T weapon);

    void ExitState(T weapon);
    
}


///
/// #####################################################################
///                                 Draw
/// #####################################################################
/// 


///<summary>
/// Draw
///</summary>
public class DrawState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {
        weapon.PlayAnimation("Draw");
        //Debug.Log("Weapon is drawing");
    }

    public void ExitState(T weapon)
    {

    }

    public void UpdateState(T weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Draw"))
        {
            weapon.SetState(new IdleState<T>());
        }
    }
}

/// 
/// #####################################################################
///                                 Idle
/// #####################################################################
/// 

///<summary>
/// Idle
///</summary>

public class IdleState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {
        //Debug.Log("Weapon is Idle");
        weapon.PlayAnimation("Idle");
    }
    public void UpdateState(T weapon)
    {

    }

    public void ExitState(T weapon)
    {

    }

}


/// 
/// #####################################################################
///                                 Fire
/// #####################################################################
/// 
public class SingleFiringState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {
            weapon.PlayAnimation("Fire");        
    }
    public void UpdateState(T weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Fire") || stateInfo.normalizedTime >= 0.9f )
        {
            
            weapon.SetState(new IdleState<T>());
        }

    }

    public void ExitState(T weapon)
    {
        //weapon.SetState(new DropState());
            
    }


}


/// <summary>
/// For AutoShot
/// Fire One, Two, Three
/// </summary>
public class FireOneState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {
        weapon.PlayAnimationAuto("Fire1");
    }
    public void UpdateState(T weapon)
    {

        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    weapon.SetState(new FireTwoState<T>());
        //
        //}

        if (!stateInfo.IsName("Fire1") || stateInfo.normalizedTime >= 0.9f)
        {

            weapon.SetState(new IdleState<T>());
        }

    }

    public void ExitState(T weapon)
    {

    }

}




/// 
/// #####################################################################
///                                 Reload
/// #####################################################################
/// 

/// <summary>
/// Mag Reload
/// </summary>
public class ReloadingState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {

        weapon.PlayAnimation("Reload");
    }

    public void ExitState(T weapon)
    {
        
    }

    public void UpdateState(T weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0); 
        if (!stateInfo.IsName("Reload") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new IdleState<T>());
        }
    }
}


/// <summary>
/// Single Reload
/// StartReload -> Insert -> AfterReload
/// Fire state can be entered at any state
/// </summary>
public class SingleStartReloadState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {

        weapon.PlayAnimation("StartReload");
    }

    public void ExitState(T weapon)
    {

    }

    public void UpdateState(T weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("StartReload") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new IdleState<T>());
        }
    }
}

public class SingleInsertReloadState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {
        weapon.PlayAnimation("Insert");
    }

    public void ExitState(T weapon)
    {
       
    }

    public void UpdateState(T weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("InsertReload") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new SingleAfterReloadState<T>());
        }
    }
}

public class SingleAfterReloadState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {
        
    }

    public void ExitState(T weapon)
    {
        
    }

    public void UpdateState(T weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("AfterReload") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new IdleState<T>());
        }
    }
}


public class DropState<T> : IWeaponState<T> where T : Weapon<T>
{
    public void EnterState(T weapon)
    {
        weapon.PlayAnimation("Drop");
    }

    public void ExitState(T weapon)
    {

    }

    public void UpdateState(T weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Drop") || stateInfo.normalizedTime >= 0.1f)
        {
            weapon.SetState(new IdleState<T>());
        }
    }
}