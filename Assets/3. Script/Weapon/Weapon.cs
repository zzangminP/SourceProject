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
    /// view model �ϰ� world model�ϰ� ��� �����Ҳ��� �����غ�����..
    /// 
    /// 
    /// </summary>
    
    public Animator animator;

    private IWeaponState currentState;
    //private EWeaponState eCurrentState;

    public int damage = 25;
    public float range = 100f;
    public float impactForce = 50f;
    public Camera fpsCam;

    void Start()
    {
        // �ʱ� ���¸� DrawState�� ����
        SetState(new DrawState());
    }

    void Update()
    {
        // ���� ������ UpdateState �޼��� ȣ��
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
        //Debug.Log(currentState);



        ///
        /// Actually fire guns
        ///

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }


        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);


    }

    void Shoot()
    {

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.collider.gameObject.layer);


            //PlayerControl hitPlayer = hit.transform.GetComponent<PlayerControl>();
            Dummy hitPlayer = hit.transform.GetComponentInParent<Dummy>();
            int calcDamage = 0;
            if (hitPlayer != null)
            {
                switch(hit.collider.gameObject.layer)
                {
                    case 9: calcDamage = damage * 4;
                        break;
                    //case 10:       ---> default
                    //    break;     ---> default
                    case 11: calcDamage = (int)(damage * 1.25);
                        break;
                    case 12: calcDamage = (int)(damage * 0.75);
                        break;
                    default: calcDamage = damage;
                        break;


                }


                //hitPlayer.hp -= damage;
                //hitPlayer.TakeDamage(damage);
                hitPlayer.TakeDamage(calcDamage);
                Debug.Log(calcDamage);

                if(hitPlayer.hp <= 0)
                {
                    
                    hitPlayer.GetComponent<Rigidbody>().AddForce(-hit.normal * impactForce, ForceMode.Impulse);
                    Debug.Log(-hit.normal);
                    Debug.Log("Added force");
                }
                
                
                
                
            }
            

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }




        }


        

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
