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
        if (!stateInfo.IsName("Draw") || stateInfo.normalizedTime >= 1.0f)
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
    }
    public void UpdateState(Weapon weapon)
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    weapon.SetState(new FiringState());
        //}
        //
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    weapon.SetState(new ReloadingState());
        //
        //}
    }

    public void ExitState(Weapon weapon)
    {
        //Debug.Log("Weapon Exitting");
    }

}

public class FiringState : IWeaponState
{
    public void EnterState(Weapon weapon)
    {
        //Debug.Log("Weapon Fired");
        weapon.PlayAnimation("Fire");
        
    }
    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Fire") || stateInfo.normalizedTime >= 1.0f)
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
        //Debug.Log("Weapon is Reloading");
        weapon.PlayAnimation("Reload");
    }

    public void ExitState(Weapon weapon)
    {
        
    }

    public void UpdateState(Weapon weapon)
    {
        AnimatorStateInfo stateInfo = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Fire") || stateInfo.normalizedTime >= 1.0f)
        {
            weapon.SetState(new IdleState());
        }
    }
}