using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dummy : MonoBehaviour 
{

    [SerializeField] private Animator player_movement_ani;

    //private PlayerState state;


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
    //[SerializeField] private Animator v_model_ani;
    [SerializeField] private Animator w_model_ani;


    [Header("Weapon")]
    [SerializeField] private GameObject weapon;

    //[SerializeField] private Weapon weapon;




    /// <summary>
    /// for AI
    /// </summary>
    private StateMachine statemachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    [Header("AI")]
    [SerializeField]
    private string currentState;
    public Path path;
    [SerializeField]
    private GameObject player;
    public GameObject viewPoint;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;


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
        statemachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        statemachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

        weapon = GetComponentInChildren<WeaponWorldDrop>().gameObject;

        SetAnimator();
        //player_movement_ani = GetComponent<Animator>();
        //player_movement_ani.SetLayerWeight(player_movement_ani.GetLayerIndex("AK47"), 1);
        //Cursor.lockState = CursorLockMode.Locked;
        player_rg = GetComponent<Rigidbody>();

        setRagdoll(true);
        //transform.GetComponent<BoxCollider>().enabled = true;
        setCollider(true);
        transform.GetComponent<BoxCollider>().enabled = true;
        SetWeapon(true);
        //hitBoxCollider.enabled = true;


        //v_model_ani = GameObject.Find("PlayerKeyOne").GetComponentInChildren<Animator>();
        //WeaponSet();
        //current_weapon = pri_weapon;

    }
    private void SetAnimator()
    {
        player_movement_ani = GetComponent<Animator>();
        for (int i = 1; i < player_movement_ani.layerCount; i++)
        {
            player_movement_ani.SetLayerWeight(i, 0);
        }
        player_movement_ani.SetLayerWeight(player_movement_ani.GetLayerIndex("AK47"), 1);

    }

    private void Update()
    {
        //PlayerInput();


        //HandleInput();
        //WeaponControl();
        CanSeePlayer();
        currentState = statemachine.activeState.ToString();
    }


    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(viewPoint.transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - viewPoint.transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, viewPoint.transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(viewPoint.transform.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if(hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction  * sightDistance);
                            return true;
                        }

                    }

                }
            }
        }
        return false;

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

    }

    private void setCollider(bool state)
    {
        skeleton_cl = transform.GetComponentsInChildren<Collider>();
        //transform.GetComponent<BoxCollider>().enabled = state;
        foreach (Collider cl in skeleton_cl)
        {
            cl.enabled = state;
            //Debug.Log(cl.name);
        }
        transform.GetComponent<BoxCollider>().enabled = !state;
        //transform.GetComponent<Rigidbody>().isKinematic = !state;
    }



    //private void PlayerInput()
    //{
    //
    //    //PlayerMovement();
    //
    //    
    //
    //    WeaponControl();
    //    //player_ani.SetBool("Move", isMove);
    //    //player_ani.SetFloat("DirX", direction.x);
    //    //player_ani.SetFloat("DirZ", direction.z);
    //
    //
    //}

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


        //if (Input.GetKey(KeyCode.R))
        //{
        //    // input reload method here
        //    
        //
        //}


        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    pri_weapon.SetState(new FiringState());
        //}
        //
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    pri_weapon.SetState(new ReloadingState());
        //
        //}



    }


    private void PlayerMovement(float dirX, float dirZ, float mouseX, float mouseY)
    {
        // WASD
        //float _dirX = Input.GetAxis("Horizontal");
        //float _dirZ = Input.GetAxis("Vertical");

        float _dirX = dirX;
        float _dirZ = dirZ;

        // mouse
        //float _mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        //float _mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        float _mouseX = mouseX * Time.deltaTime * mouseSensitivity;
        float _mouseY = mouseY * Time.deltaTime * mouseSensitivity;


        Vector3 direction = new Vector3(_dirX, 0, _dirZ);
        float gravity = -9.81f;


        isMove = false;


        // mouse rotation
        xRotation -= _mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        player_tr.Rotate(Vector3.up * _mouseX);
        viewModel_tr.localRotation = Quaternion.Euler(xRotation, 0, 0);


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
        //if (Input.GetKey(KeyCode.R))
        //{   
        //    //if (!v_model_ani.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        //    //{ 
        //    //
        //    //}
        //    //v_model_ani.SetBool("Reload", false);
        //    //Debug.Log("Reloaded");
        //}

        //if(Input.GetKey(KeyCode.Mouse0))
        //{
        //    v_model_ani.SetBool("Fire", true);
        //}
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Debug.Log("before Die");
            Die();
            Debug.Log("after die");
        }

    }

    public void Die()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<Animator>().enabled = false;

        setRagdoll(false);
        //setCollider(false);
        SetWeapon(false);

        player_movement_ani = null;
    }
    void SetWeapon(bool state)
    {
        weapon.GetComponent<BoxCollider>().enabled = !state;
        weapon.GetComponent<Rigidbody>().isKinematic = state;

    }




    //protected void VModelPlayAnimation(string animationClip)
    //{
    //    AnimatorStateInfo v_stateinfo = v_model_ani.GetCurrentAnimatorStateInfo(0);
    //    //AnimatorStateInfo w_stateinfo = w_model_ani.GetCurrentAnimatorStateInfo(0);
    //    if (!v_stateinfo.IsName(animationClip))//&& w_stateinfo.IsName(animationClip))
    //    {
    //        v_model_ani.CrossFadeInFixedTime(animationClip, 0f);
    //        //w_model_ani.CrossFadeInFixedTime(animationClip, 0f);
    //    }
    //}


}
