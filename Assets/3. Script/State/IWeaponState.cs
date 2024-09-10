using UnityEditor.IMGUI.Controls;
using UnityEngine;
public interface IWeaponState
{
    void EnterState(Weapon weapon);
    
    void UpdateState(Weapon weapon);

    void ExitState(Weapon weapon);
    
}

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

public class AutoFiringState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Fire1");
    }
    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Fire1") && Input.GetMouseButtonDown(0))
        {
            weapon.PlayAnimation("Fire2");
        }
        else if(stateInfo.IsName("Fire2") && Input.GetMouseButtonDown(0))
        {
            weapon.PlayAnimation("Fire3");
        }
        else if(stateInfo.IsName("Fire3") && Input.GetMouseButtonDown(0))
        {
            weapon.PlayAnimation("Fire1");
        }
        if (!stateInfo.IsName("Fire1") ||
            !stateInfo.IsName("Fire2") ||
            !stateInfo.IsName("Fire3") || 
            stateInfo.normalizedTime >= 0.9f)
        {

            weapon.SetState(new IdleState());
        }

    }

    public void ExitState(Weapon weapon)
    {
        //weapon.SetState(new DropState());

    }


}


public class FireOneState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimationAuto("Fire1");
    }
    public void UpdateState(Weapon weapon)
    {

        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetState(new FireTwoState());

        }

        if (!stateInfo.IsName("Fire1") || stateInfo.normalizedTime >= 0.9f)
        {

            weapon.SetState(new IdleState());
        }

    }

    public void ExitState(Weapon weapon)
    {

    }

}
public class FireTwoState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Fire2");
    }
    public void UpdateState(Weapon weapon)
    {

        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetState(new FireThreeState());

        }

        if (!stateInfo.IsName("Fire2") || stateInfo.normalizedTime >= 0.9f)
        {

            weapon.SetState(new IdleState());
        }

    }

    public void ExitState(Weapon weapon)
    {

    }

}

public class FireThreeState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        weapon.PlayAnimation("Fire3");
    }
    public void UpdateState(Weapon weapon)
    {

        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetState(new FireOneState());

        }

        if (!stateInfo.IsName("Fire3") || stateInfo.normalizedTime >= 0.9f)
        {

            weapon.SetState(new IdleState());
        }

    }

    public void ExitState(Weapon weapon)
    {

    }

}





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
        if (!stateInfo.IsName("Drop") || stateInfo.normalizedTime >= 0.1f)
        {
            weapon.SetState(new IdleState());
        }
    }
}