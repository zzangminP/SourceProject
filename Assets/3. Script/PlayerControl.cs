using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //private Animator player_ani;



    [Header("Components")]
    [SerializeField] private Transform player_tr;
    [SerializeField] private Transform camera_tr;
    [SerializeField] private Rigidbody player_rg;


    [Header("Physics Value")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float JumpForce = 5f;


    private bool isMove;
    private float xRotation = 0f;


    private void Start()
    {
        //player_ani = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        player_rg = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerInput();
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
        camera_tr.localRotation = Quaternion.Euler(xRotation, 0, 0);


        if (direction != Vector3.zero)
        {
            isMove = true;
            this.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);


        }

        if(Input.GetButton())
        {
            player_rg.AddForce(Vector3.up * JumpForce);

        }

        //player_ani.SetBool("Move", isMove);
        //player_ani.SetFloat("DirX", direction.x);
        //player_ani.SetFloat("DirZ", direction.z);


    }
}
