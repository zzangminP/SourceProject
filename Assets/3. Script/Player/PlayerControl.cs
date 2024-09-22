using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] public GameObject fpsCam;



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
    [SerializeField] private List<GameObject> weaponHolder_List;
    [SerializeField] private List<GameObject> playerViewModels_List;
    //[SerializeField] private Weapon pri_weapon;
    //[SerializeField] private Weapon sec_weapon;
    [SerializeField] private Weapon[] playerWeapon_List;
    [SerializeField] private Weapon[] GE_weapon = null;
    [SerializeField] private Weapon C4_weapon;
    //[SerializeField] private Weapon weapon;
    //[SerializeField] private Weapon[] gn;
    //[SerializeField] private Weapon[] miscellaneous;
    [SerializeField] private GameObject current_weapon = null;
    [SerializeField] private GameObject privious_weapon = null;
    [SerializeField] public bool canC4Plant = false;

    [Header("UI")]
    [SerializeField] public GameObject scopeOverlay;

    public WeaponHolder weaponHolder;

    private int currentWeaponIndex = 0; // ���� ������ �ε���
    private int previousWeaponIndex = -1; // ���� ������ �ε���


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
        //scopeOverlay = GameObject.Find("Scope");
        //scopeOverlay.SetActive(false);


        setRagdoll(true);   
        setCollider(true);
        transform.GetComponent<BoxCollider>().enabled = true;
        //hitBoxCollider.enabled = true;
        playerWeapon_List = new Weapon[5];


        //v_model_ani = GameObject.Find("PlayerKeyOne").GetComponentInChildren<Animator>();
        WeaponSet();

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        //PlayerInput();


        HandleInput();
        //WeaponControl();
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.TryGetComponent<WeaponWorldDrop>(out WeaponWorldDrop tempGun))
        {
            Debug.Log(tempGun);
            ContactWeapon(tempGun);
        }

    }

    private void ContactWeapon(WeaponWorldDrop tempGun)
    {
        int tmpGunType = (int)tempGun.type;
        if ((tmpGunType >= 100 && tmpGunType <= 199))
        {
            //pri_weapon
            playerWeapon_List[0] = PickUpWeapon(tempGun);

        }
        else if ((tmpGunType >= 200 && tmpGunType <= 299))
        {
            //sec_weapon
            playerWeapon_List[1] = PickUpWeapon(tempGun);

        }
        else if (tmpGunType >= 400 && tmpGunType <= 499)
        {
            // GE
        }
        else if (tempGun.type == WeaponSetting.Type.C4)
        {
            // c4
            C4_weapon = PickUpWeapon(tempGun);
        }

        tempGun = null;

    }

    Weapon PickUpWeapon(WeaponWorldDrop tempGun)
    {

        for(int i = 0; i< playerViewModels_List.Count; i++)
        {
            playerViewModels_List[i].TryGetComponent<Weapon>(out Weapon playerGun);
            if (tempGun.type == playerGun.type)
            {
                playerGun.type = tempGun.type;
                playerGun.maxAmmo = tempGun.maxAmmo;
                playerGun.currentAmmo = tempGun.currentAmmo;

                Debug.Log(playerGun.maxAmmo);
                Debug.Log(playerGun.currentAmmo);
                return playerGun;           
            }
        }
        return null;
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BombSite"))
        {
            canC4Plant = true;
            Debug.Log("BombSite Entered");
        }



    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BombSite"))
        {
            canC4Plant = false;
            Debug.Log("BombSite Out");
        }

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


        // Drop Weapon
        if(Input.GetKey(KeyCode.G))
        {
            weaponHolder.WeaponDrop(fpsCam.transform.position + fpsCam.transform.forward, WeaponSetting.Type.AK47);
        }

        SwapWeapon();



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
        for (int i = 0 ; i < weaponHolder_List.Count; i++)
        {
            for(int j = 0; j < weaponHolder_List[i].transform.childCount; j++)
            {
                playerViewModels_List.Add(weaponHolder_List[i].transform.GetChild(j).gameObject);
            }
           
        }
        
        foreach(var i in playerViewModels_List)
        {
            if (i.GetComponent<Weapon>().type != WeaponSetting.Type.Knife)
            {
                i.SetActive(false);
            }
            else
            {
                playerWeapon_List[2] = i.GetComponent<Weapon>();
                current_weapon = i;
            }

        }


        //pri_weapon = transform.Find("PlayerKeyOne").GetComponentInChildren<Weapon>();
        //sec_weapon = transform.Find("PlayerKeyTwo").GetComponentInChildren<Weapon>();
        //weaponHolder = transform.Find("Main Camera/Weapon Camera/WeaponHolder").GetComponent<WeaponHolder>();
    }

    //private void SwitchingWeapon()
    //{
    //    // Q�� ���� ���� ����� ��ü
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        if (previousWeaponIndex != -1) // ���� ���Ⱑ ������ ��쿡��
    //        {
    //            SwapWeapon(previousWeaponIndex); // ���� ����� ��ü
    //        }
    //        else
    //        {
    //            return; // ���� ���Ⱑ ������ ����
    //        }
    //    }
    //
    //    // ���콺 ���� �Ʒ��� ��ũ��
    //    if (Input.mouseScrollDelta.y < 0)
    //    {
    //        previousWeaponIndex = currentWeaponIndex; // ���� ���� ����
    //        currentWeaponIndex = (currentWeaponIndex + 1) % 5; // ���� ����� ��ȯ (1~5�� ����)
    //        SwapWeapon(currentWeaponIndex); // ���� ��ü
    //    }
    //
    //    // ���콺 ���� ���� ��ũ��
    //    if (Input.mouseScrollDelta.y > 0)
    //    {
    //        previousWeaponIndex = currentWeaponIndex; // ���� ���� ����
    //        currentWeaponIndex = (currentWeaponIndex - 1 + 5) % 5; // ���� ����� ��ȯ (1~5�� ����)
    //        SwapWeapon(currentWeaponIndex); // ���� ��ü
    //    }
    //
    //    // ����Ű 1-5�� ������ �� �ش� ����� ��ü
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Alpha1 + i))
    //        {
    //            previousWeaponIndex = currentWeaponIndex; // ���� ���� ����
    //            currentWeaponIndex = i; // ������ ���� �ε����� ����
    //            SwapWeapon(currentWeaponIndex); // ���� ��ü
    //        }
    //    }
    //}



    //private void SwapWeapon(int weaponIndex)
    //{
    //    // ��� ���� �� �� ��Ȱ��ȭ (�ʿ�� ����)
    //    DeactivateAllWeapons();
    //
    //    // ���� �ε����� ���� ������ ���⸦ Ȱ��ȭ
    //    switch (weaponIndex)
    //    {
    //        case 0: // 1�� ����: pri_weapon
    //            if (pri_weapon != null)
    //            {
    //                ActivateWeaponModel(pri_weapon.type);
    //                Debug.Log($"Primary Weapon: {pri_weapon.type}");
    //            }
    //            break;
    //
    //        case 1: // 2�� ����: sec_weapon
    //            if (sec_weapon != null)
    //            {
    //                ActivateWeaponModel(sec_weapon.type);
    //                Debug.Log($"Secondary Weapon: {sec_weapon.type}");
    //            }
    //            break;
    //
    //        case 2: // 3�� ����: Knife
    //            ActivateWeaponModel(WeaponSetting.Type.Knife);
    //            Debug.Log("Knife activated");
    //            break;
    //
    //        case 3: // 4�� ����: GE (Grenades)
    //                // ���⿡ ����ź ���� ������ �߰��ϸ� �˴ϴ�.
    //            Debug.Log("Grenade slot activated");
    //            break;
    //
    //        case 4: // 5�� ����: C4
    //            if (C4_weapon != null)
    //            {
    //                ActivateWeaponModel(C4_weapon.type);
    //                Debug.Log("C4 activated");
    //            }
    //            break;
    //
    //        default:
    //            Debug.LogWarning("Invalid weapon slot");
    //            break;
    //    }
    //}



    private void DeactivateAllWeapons()
    {
        // ��� ���� ��� ��Ȱ��ȭ
        foreach (var weapon in playerViewModels_List)
        {
            weapon.SetActive(false);
        }
    }

    private void SwapWeapon()
    {

        previousWeaponIndex = currentWeaponIndex;


        if (Input.mouseScrollDelta.y > 0)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % playerViewModels_List.Count;

        }

        // ���콺 ���� �Ʒ��� ��ũ��
        if (Input.mouseScrollDelta.y < 0)
        {
            currentWeaponIndex = (currentWeaponIndex - 1 + playerViewModels_List.Count) % playerViewModels_List.Count;

        }

        // ����Ű (1 ~ playerViewModels_List.Count) �� ���� ��ü
        for (int i = 0; i < playerViewModels_List.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {

            }
        }

        // Q Ű�� ���� ����� ��ü
        if (Input.GetKeyDown(KeyCode.Q) && previousWeaponIndex != -1)
        {

        }

        // ������ ���� Ȱ��ȭ
        ActivateWeaponModel(currentWeaponIndex);
    }

    private void ActivateWeaponModel(int weaponIndex)
    {
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

}
