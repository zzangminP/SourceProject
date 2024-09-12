using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerState
{
    S_Idle = 0,
    S_Walk,
    S_Run,
    S_Shoot,
    C_Idle,
    C_Move,
    C_Shoot
}

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private Animator player_movement_ani;

    private PlayerState state;


    [Header("Components")]
    [SerializeField] private Transform player_tr;
    [SerializeField] private Transform viewModel_tr;
    //[SerializeField] private Transform viewModelCam_tr;
    //
    //[SerializeField] private Transform viewModelTest_tr;

    [SerializeField] private Rigidbody player_rg;



    //[SerializeField] private MeshCollider hitBoxCollider;
    //[SerializeField] private Transform[] child;

    [Header("Basic Values")]
    [SerializeField] public int hp = 100;
    [SerializeField] public int armor = 100;


    [Header("Physics Values")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float JumpForce = 5f;


    [Header("Childern Components")]
    [SerializeField] private Rigidbody[] skeleton_rg;
    [SerializeField] private Collider[] skeleton_cl;


    [Header("Animator")]
    [SerializeField] private Animator v_model_ani;
    [SerializeField] private Animator w_model_ani;


    [Header("Weapon")]
    [SerializeField] private Weapon pri_weapon;
    [SerializeField] private Weapon sec_weapon;
    //[SerializeField] private Weapon weapon;
    [SerializeField] private Weapon[] gn;
    [SerializeField] private Weapon[] miscellaneous;
    [SerializeField] private Weapon current_weapon;
    [SerializeField] private Weapon privious_weapon = null;



    //private int holdingWeaponIndex = 0;
    /// <summary>
    /// 
    /// 1. [X                 ]
    /// 2. [X                 ]
    /// 3. [X                 ]
    /// 4. [X, X, X           ]
    /// 5. [X                 ]
    /// </summary>



    //private GameObject[,] weaponSlot = new GameObject[5,5];


    private bool isMove;
    private float xRotation = 0f;


    private void Start()
    {

        InitPlayer();
    }

    /// <summary>
    ///
    /// Set up Player
    /// 
    /// </summary>
    /// 


    private void InitPlayer()
    {
        player_movement_ani = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        player_rg = GetComponent<Rigidbody>();

        setRagdoll(true);   
        setCollider(true);
        transform.GetComponent<BoxCollider>().enabled = true;
        //hitBoxCollider.enabled = true;


        //v_model_ani = GameObject.Find("PlayerKeyOne").GetComponentInChildren<Animator>();
        WeaponSet();
        current_weapon = pri_weapon;

    }

    private void FixedUpdate()
    {
        //PlayerInput();


        HandleInput();
        //WeaponControl();
    }






    private void setRagdoll(bool state)
    {
        skeleton_rg = transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in skeleton_rg)
        {
            rb.isKinematic = state;
            //Debug.Log(rb.name);
        }
        transform.GetComponent<Rigidbody>().isKinematic = !state;
        //transform.GetComponent<Rigidbody>().isKinematic = !state;
    }
    private void setCollider(bool state)
    {
        skeleton_cl = transform.GetComponentsInChildren<Collider>();
        foreach (Collider cl in skeleton_cl)
        {
            cl.enabled = !state;
            //Debug.Log(cl.name);
        }
        
        //transform.GetComponent<Rigidbody>().isKinematic = !state;
    }



    private void HandleInput()
    {
        // WASD

        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        // Mouse

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        PlayerMovement(dirX, dirZ, mouseX, mouseY);







        // Jump


        if (Input.GetKey(KeyCode.Space))
        {
            PlayerJump();


        }


    }


    private void PlayerMovement(float dirX, float dirZ, float mouseX, float mouseY)
    {
        // WASD


        float _dirX = dirX;
        float _dirZ = dirZ;

        // mouse
        float _mouseX = mouseX * Time.deltaTime * mouseSensitivity;
        float _mouseY = mouseY * Time.deltaTime * mouseSensitivity;


        Vector3 direction = new Vector3(_dirX, 0, _dirZ);
        float gravity = -9.81f;


        isMove = false;


        // mouse rotation
        xRotation -= _mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        player_tr.Rotate(Vector3.up * _mouseX);
       
        //viewModelCam_tr.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //viewModelTest_tr.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //viewModel_tr.localRotation = Quaternion.Euler(, 0, 0);
        viewModel_tr.localRotation = Quaternion.Euler(xRotation, 0, 0);


        //viewModel_tr.rotation (viewModelCam_tr.position, xRotation);

        if (direction != Vector3.zero)
        {
            isMove = true;
            this.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            player_movement_ani.SetBool("Move", isMove);
            player_movement_ani.SetFloat("DirX", direction.x);
            player_movement_ani.SetFloat("DirZ", direction.z);
        }
        player_movement_ani.SetBool("Move", isMove);

    }

    private void PlayerJump()
    {
        player_rg.velocity = Vector3.zero;
        player_rg.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }


    private void WeaponSet()
    {
        pri_weapon = GameObject.Find("PlayerKeyOne").GetComponentInChildren<Weapon>();

        sec_weapon = GameObject.Find("PlayerKeyTwo").GetComponentInChildren<Weapon>();

    }

    private void TakeDamage(int amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        setRagdoll(false);
        setCollider(false);
    }




    protected void VModelPlayAnimation(string animationClip)
    {
        AnimatorStateInfo v_stateinfo = v_model_ani.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo w_stateinfo = w_model_ani.GetCurrentAnimatorStateInfo(0);
        if (!v_stateinfo.IsName(animationClip))//&& w_stateinfo.IsName(animationClip))
        {
            v_model_ani.CrossFadeInFixedTime(animationClip, 0f);
            //w_model_ani.CrossFadeInFixedTime(animationClip, 0f);
        }
    }

}
