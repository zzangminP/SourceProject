using System.Collections;
using UnityEditor.ShaderGraph.Internal;
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
    /// 

    /// 0909 전략패턴 적용하기위해 Update문하고 Shoot 잠깐 주석처리함
    
    public Animator animator { get; set; }

    private IWeaponState currentState;

    //public int damage = 25;
    //public float range = 100f;
    //public float impactForce = 50f;
    public int damage { get; set; }
    public float range { get; set; }
    public float impactForce { get; set; }

    public int maxMag { get; set; }
    public int maxAmmo { get; set; }
    public int currentAmmo {  get; set; }

    public Camera fpsCam { get; set; }


    // strategy
    public ILeftClick leftClick;
    public IRightClick rightClick;
    public IReloadClick reloadClick;
    public IWASDMove wasdMove;


    void Start()
    {
        // 초기 상태를 DrawState로 설정     
        SetState(new DrawState());
        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
    }

    
    public void Left(Weapon weapon) { leftClick.OnLeftClick(weapon); }
    public void Right(Weapon weapon) { rightClick.OnRigtClick(weapon); }

    public void Reload(Weapon weapon) { reloadClick.OnReloadClick(weapon); }
    public void WASD(Weapon weapon) { wasdMove.DefaultWASDMove(weapon); }

    //void Shoot()
    //{
    //
    //    RaycastHit hit;
    //
    //    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
    //    {
    //        Debug.Log(hit.collider.gameObject.layer);
    //
    //
    //        //PlayerControl hitPlayer = hit.transform.GetComponent<PlayerControl>();
    //        Dummy hitPlayer = hit.transform.GetComponentInParent<Dummy>();
    //        int calcDamage = 0;
    //        if (hitPlayer != null)
    //        {
    //            switch(hit.collider.gameObject.layer)
    //            {
    //                case 9: calcDamage = damage * 4;
    //                    break;
    //                //case 10:       ---> default
    //                //    break;     ---> default
    //                case 11: calcDamage = (int)(damage * 1.25);
    //                    break;
    //                case 12: calcDamage = (int)(damage * 0.75);
    //                    break;
    //                default: calcDamage = damage;
    //                    break;
    //
    //
    //            }
    //
    //            hitPlayer.TakeDamage(calcDamage);
    //            Debug.Log(calcDamage);
    //
    //            if(hitPlayer.hp <= 0)
    //            {
    //                
    //                hitPlayer.GetComponent<Rigidbody>().AddForce(-hit.normal * impactForce, ForceMode.Impulse);
    //                Debug.Log(-hit.normal);
    //                Debug.Log("Added force");
    //            }         
    //            
    //            
    //        }
    //        
    //
    //        if (hit.rigidbody != null)
    //        {
    //            hit.rigidbody.AddForce(-hit.normal * impactForce);
    //
    //        }
    //
    //    }
    //
    //
    //    
    //
    //}


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
