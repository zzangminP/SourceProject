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
            Debug.Log(hit.transform.name);


            //PlayerControl hitPlayer = hit.transform.GetComponent<PlayerControl>();
            Dummy hitPlayer = hit.transform.GetComponent<Dummy>();
            if (hitPlayer != null)
            {
                //hitPlayer.hp -= damage;
                hitPlayer.TakeDamage(damage);
                
                
                
                
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
