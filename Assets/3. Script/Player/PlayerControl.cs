using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //private Animator player_ani;



    [Header("Components")]
    [SerializeField] private Transform player_tr;
    [SerializeField] private Transform viewModel_tr;
    [SerializeField] private Rigidbody player_rg;

    [SerializeField] private Rigidbody[] skeleton_rg;
    [SerializeField] private Collider[] skeleton_cl;
    [SerializeField] private MeshCollider hitBoxCollider;
    //[SerializeField] private Transform[] child;

    [Header("Physics Value")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float JumpForce = 5f;

    
    
    /// <summary>
    /// 
    /// 1. [X                 ]
    /// 2. [X                 ]
    /// 3. [X                 ]
    /// 4. [X, X, X           ]
    /// 5. [X                 ]
    /// </summary>



    private GameObject[,] weaponSlot = new GameObject[5,5];


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
        //player_ani = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        player_rg = GetComponent<Rigidbody>();

        setRagdoll(true);
        transform.GetComponent<Rigidbody>().isKinematic = false;
        setCollider(true);
        transform.GetComponent<BoxCollider>().enabled = true;
        hitBoxCollider.enabled = true;
    }

    private void Update()
    {
        PlayerInput();
    }






    private void setRagdoll(bool state)
    {
        skeleton_rg = transform.GetComponentsInChildren<Rigidbody>();
    
        foreach (Rigidbody rb in skeleton_rg)
        {
            rb.isKinematic = state;
            Debug.Log(rb.name);
        }
        //transform.GetComponent<Rigidbody>().isKinematic = !state;
    }
    private void setCollider(bool state)
    {
        skeleton_cl = transform.GetComponentsInChildren<Collider>();

        foreach (Collider cl in skeleton_cl)
        {
            cl.enabled = !state;
            Debug.Log(cl.name);
        }
        //transform.GetComponent<Rigidbody>().isKinematic = !state;
    }



    private void PlayerInput()
    {

        // WASD
        float _dirX = Input.GetAxis("Horizontal");
        float _dirZ = Input.GetAxis("Vertical");


        // mouse
        float _mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float _mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        Vector3 direction = new Vector3(_dirX, 0, _dirZ);

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


        }

        if (Input.GetKey(KeyCode.Space))
        {
            player_rg.velocity = Vector3.zero;
            player_rg.AddForce(Vector3.up * JumpForce);
        
        }

        //player_ani.SetBool("Move", isMove);
        //player_ani.SetFloat("DirX", direction.x);
        //player_ani.SetFloat("DirZ", direction.z);


    }
}
