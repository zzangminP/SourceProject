using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Dummy : MonoBehaviour 
{




    [Header("Components")]
    [SerializeField] private Transform player_tr;
    [SerializeField] private Transform viewModel_tr;
    [SerializeField] private Rigidbody player_rg;



    //[SerializeField] private MeshCollider hitBoxCollider;
    //[SerializeField] private Transform[] child;

    [Header("Basic Values")]
    [SerializeField] public int hp = 100;
    [SerializeField] public int armor = 100;
    [SerializeField] private float deathTimer = 0;


    [Header("Childern Components")]
    [SerializeField] private Rigidbody[] skeleton_rg;
    [SerializeField] private Collider[] skeleton_cl;


    [Header("Animator")]
    [SerializeField] private Animator player_movement_ani;
    


    [Header("Weapon")]
    [SerializeField] private GameObject weapon;

    [Header("SFX")]
    [SerializeField] public AudioSource hit_audio;
    [SerializeField] public AudioSource death_audio;
    [SerializeField] public AudioSource attack_audio;
    [SerializeField] public AudioSource awp_audio;

    [Header("VFX")]
    public GameObject bloodImpact;

    //[SerializeField] private Weapon weapon;




    /// <summary>
    /// for AI
    /// </summary>
    [Header("AI")]
    
    [SerializeField]
    private string currentState;
    private StateMachine statemachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    public Path path;

    public bool isAWP = false;
    public GameObject player;
    public PlayerControl player_playerControl;
    
    private Vector3 lastKnowPos;
    public  Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }
    
    
    [Header("Sight Values")]
    public GameObject viewPoint;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    
    
    [Header("Weapon Values")]
    public Transform gunBarrel;
    

    [Range(0.1f, 10f)]
    public float fireRate;
    public int damage;



    private bool isMove;
    private bool isDead = false;


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

    private void OnEnable()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        statemachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        statemachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        player_playerControl = player.GetComponentInChildren<PlayerControl>();
        hit_audio = player_playerControl.hit_audio;
        death_audio = player_playerControl.death_audio;

        awp_audio = GameObject.Find("AWP Audio").GetComponent<AudioSource>();
        attack_audio = GameObject.Find("AK Audio").GetComponent<AudioSource>();

        weapon = GetComponentInChildren<WeaponWorldDrop>().gameObject;

        SetAnimator();
        player_rg = GetComponent<Rigidbody>();

        setRagdoll(true);
        setCollider(true);
        transform.GetComponent<BoxCollider>().enabled = true;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        SetWeapon(true);

        isDead = false;


    }
    private void SetAnimator()
    {
        player_movement_ani = GetComponent<Animator>();
        for (int i = 1; i < player_movement_ani.layerCount; i++)
        {
            player_movement_ani.SetLayerWeight(i, 0);
        }
        player_movement_ani.SetLayerWeight(player_movement_ani.GetLayerIndex("AK47"), 1);
        player_movement_ani.SetBool("Walk", true);

    }

    private void Update()
    {

        CanSeePlayer();
        currentState = statemachine.activeState.ToString();
        
        DummyMovement();


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
                        if (hitInfo.transform.gameObject.CompareTag("Smoke"))
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.gray);
                            return false;  
                        }




                        if (hitInfo.transform.gameObject == player)
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






    private void DummyMovement()
    {


        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);


        isMove = false;



        if (localVelocity.magnitude > 0.1f)
        {
            isMove = true;
            player_movement_ani.SetBool("Move", isMove);
            player_movement_ani.SetFloat("DirX", localVelocity.x);
            player_movement_ani.SetFloat("DirZ", localVelocity.z);
        }
        player_movement_ani.SetBool("Move", isMove);

    }


    public void TakeDamage(int amount)
    {
        hp -= amount;
        Instantiate(bloodImpact,transform.position + new Vector3(0,1f,0), Quaternion.identity);
        
        hit_audio.Play();
        
        if (hp <= 0)
        {

            Die();

        }

    }

    public void Die()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<Animator>().enabled = false;
        
        death_audio.Play();

        setRagdoll(false);
        //setCollider(false);
        SetWeapon(false);

        player_movement_ani = null;
        agent.enabled = false;
        statemachine.enabled = false;
        isDead = true;
        weapon.transform.SetParent(GameObject.Find("de_dust2").transform);

        player_playerControl.killCount += 1;
        StartCoroutine(AfterDie());

    }

    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(3);
        isDead = false;

        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        StopCoroutine(AfterDie());
    }
    void SetWeapon(bool state)
    {
        weapon.GetComponent<BoxCollider>().enabled = !state;
        weapon.GetComponent<Rigidbody>().isKinematic = state;

    }







}
