using UnityEngine;
public interface IWeaponState
{
    void EnterState(Weapon weapon);
    
    void UpdateState(Weapon weapon);

    void ExitState(Weapon weapon);
    
}


///
/// #####################################################################
///                                 Draw
/// #####################################################################
/// 


///<summary>
/// Draw
///</summary>
public class DrawState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Draw");
        //Debug.Log("Weapon is drawing");
    }

    public void ExitState(Weapon weapon)
    {

    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Draw"))
        {
            weapon.SetState(new IdleState());
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

public class IdleState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        //Debug.Log("Weapon is Idle");
        weapon.PlayAnimation("Idle");
    }
    public void UpdateState(Weapon weapon)
    {

    }

    public void ExitState(Weapon weapon)
    {

    }

}


/// 
/// #####################################################################
///                                 Fire
/// #####################################################################
/// 
public class SingleFiringState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
            weapon.PlayAnimation("Fire");        
    }
    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Fire") || stateInfo.normalizedTime >= 0.9f )
        {
            
            weapon.SetState(new IdleState());
        }

    }

    public void ExitState(Weapon weapon)
    {
        //weapon.SetState(new DropState());
            
    }


}


/// <summary>
/// For AutoShot
/// Fire One, Two, Three
/// </summary>
public class FireOneState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimationAuto("Fire1");
    }
    public void UpdateState(Weapon weapon)
    {

        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    weapon.SetState(new FireTwoState<T>());
        //
        //}

        if (!stateInfo.IsName("Fire1") || stateInfo.normalizedTime >= 0.9f)
        {

            weapon.SetState(new IdleState());
        }

    }

    public void ExitState(Weapon weapon)
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
public class ReloadingState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {

        weapon.PlayAnimation("Reload");
    }

    public void ExitState(Weapon weapon)
    {
        
    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0); 
        if (!stateInfo.IsName("Reload") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new IdleState());
        }
    }
}


/// <summary>
/// Single Reload
/// StartReload -> Insert -> AfterReload
/// Fire state can be entered at any state
/// </summary>
public class SingleStartReloadState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {

        weapon.PlayAnimation("StartReload");
    }

    public void ExitState(Weapon weapon)
    {

    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("StartReload") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new IdleState());
        }
    }
}

public class SingleInsertReloadState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Insert");
    }

    public void ExitState(Weapon weapon)
    {
       
    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Insert") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new SingleAfterReloadState());
        }
    }
}

public class SingleAfterReloadState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        
    }

    public void ExitState(Weapon weapon)
    {
        
    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("AfterReload") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new IdleState());
        }
    }
}




/// <summary>
/// For Planting C4
/// </summary>
public class PlantingState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Planting");
    }

    public void ExitState(Weapon weapon)
    {

    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        
        //if (!stateInfo.IsName("Planting"))
        if(!stateInfo.IsName("Planting") || stateInfo.normalizedTime >= 1f)
        {
            weapon.SetState(new DropState());
        }
    }
}


/// <summary>
/// For Planting C4
/// </summary>
public class DropState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Drop");
    }

    public void ExitState(Weapon weapon)
    {

    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Drop") || stateInfo.normalizedTime >= 1f)
        {
            weapon.SetState(new IdleState());
        }
    }
}


public class KnifeStabHitState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Stab");
    }

    public void ExitState(Weapon weapon)
    {

    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Stab") || stateInfo.normalizedTime >= 1f)
        {
            weapon.SetState(new IdleState());
        }
    }
}

public class KnifeSlashState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Slash");
    }

    public void ExitState(Weapon weapon)
    {

    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.6f)
        {
            weapon.SetState(new IdleState());
        }
    }
}





public class KnifeStabMissState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("StabMiss");
    }

    public void ExitState(Weapon weapon)
    {

    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("StabMiss") || stateInfo.normalizedTime >= 1f)
        {
            weapon.SetState(new IdleState());
        }
    }
}
