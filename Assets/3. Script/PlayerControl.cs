using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //private Animator player_ani;

    [SerializeField] private float moveSpeed;
    private bool isMove;


    private void Start()
    {
        //player_ani = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        float _dirX = Input.GetAxis("Horizontal");
        float _dirZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(_dirX, 0, _dirZ);

        isMove = false;


        if (direction != Vector3.zero)
        {
            isMove = true;
            this.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);


        }

        //player_ani.SetBool("Move", isMove);
        //player_ani.SetFloat("DirX", direction.x);
        //player_ani.SetFloat("DirZ", direction.z);


    }
}
