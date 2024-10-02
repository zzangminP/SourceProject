using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



//public enum PlayerState
//{
//    S_Idle = 0,
//    S_Walk,
//    S_Run,
//    S_Shoot,
//    C_Idle,
//    C_Move,
//    C_Shoot
//}

public class PlayerControl : MonoBehaviour
{


    //private PlayerState state;


    [Header("Components")]
    [SerializeField] private Transform player_tr;
    [SerializeField] private Transform viewModel_tr;
    [SerializeField] private Rigidbody player_rg;
    [SerializeField] public GameObject fpsCam;
    [SerializeField] public GameObject playerHead;
    [SerializeField] public AudioListener playerAudioListener;
    [SerializeField] private Animator player_movement_ani;


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
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isCrouch = false;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isWalking = false;


    [Header("Weapon")]
    [SerializeField] private List<GameObject> weaponHolder_List;
    [SerializeField] private List<GameObject> playerViewModels_List;

    [SerializeField] public Weapon[] playerWeapon_List;
    [SerializeField] public Weapon[] GE_weapon = null;
    [SerializeField] private Weapon C4_weapon;

    [SerializeField] private GameObject current_weapon = null;
    [SerializeField] private GameObject privious_weapon = null;
    [SerializeField] public bool canC4Plant = false;
    public WeaponHolder weaponHolder;
    
    /// <summary>
    /// In World, The Position that character holds Weapon
    /// </summary>
    [SerializeField] private GameObject placeWeaponBone;

    [Header("UI")]
    [SerializeField] public GameObject scopeOverlay;
    [SerializeField] private TextMeshProUGUI HP_TMP;
    [SerializeField] private TextMeshProUGUI armor_TMP;
    [SerializeField] private GameObject storeUI;
    [SerializeField] private TextMeshProUGUI money_TMP;
    [SerializeField] public int money = 0;
    [SerializeField] public int killCount = 0;
    private bool canIOpenStore = false;


    private int currentWeaponIndex = 0; // 현재 무기의 인덱스
    private int previousWeaponIndex = -1; // 이전 무기의 인덱스

    private int scrollValue = 0;

    [SerializeField,Header("Game Manager")]
    private IGameManager gameManager;
    public string team;



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

        //fpsCam.SetActive(false);
        //return;
 



        if (SceneManager.GetActiveScene().name != "Title")
        { 
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        player_rg = GetComponent<Rigidbody>();
        //scopeOverlay = GameObject.Find("Scope");
        //scopeOverlay.SetActive(false);

        player_movement_ani = GetComponent<Animator>();
        setRagdoll(true);   
        setCollider(true);
        transform.GetComponent<BoxCollider>().enabled = true;
        //hitBoxCollider.enabled = true;
        playerWeapon_List = new Weapon[5];
        fpsCam.SetActive(true);

        SetGameManager(gameManager);

        WeaponSet();

    }

    private void Update()
    {

        UI();
        
    }
    private void OnEnable()
    {
        fpsCam.SetActive(true);
    }

    private void FixedUpdate()
    {
        //PlayerInput();

        //if (!isLocalPlayer)
        //{
        //    return;
        //}
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

    public void SetGameManager(IGameManager manager)
    {
        this.gameManager = manager;
    }
    private void UI()
    {
        HP_TMP.text = ($"HP : {hp}");
        armor_TMP.text = ($"Armor : {armor}");
        money_TMP.text = ($"$ {money}");
        if (money >= 16000)
        {
            money = 16000;
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

                tempGun.gameObject.SetActive(false);
                return playerGun;           
            }
        }
        return null;
    }

    public Weapon BuyWeapon(Weapon tempGun)
    {

        for (int i = 0; i < playerViewModels_List.Count; i++)
        {
            playerViewModels_List[i].TryGetComponent<Weapon>(out Weapon playerGun);
            if (tempGun.type == playerGun.type)
            {
                //playerGun.type = tempGun;
                //playerGun.maxAmmo = tempGun.maxAmmo;
                //playerGun.currentAmmo = tempGun.currentAmmo;
                playerGun.type = tempGun.type;
                playerGun.maxAmmo = tempGun.maxAmmo;
                playerGun.currentAmmo = tempGun.currentAmmo;


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

        if(other.gameObject.CompareTag("Store"))
        {
            canIOpenStore = true;
       
        }



    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BombSite"))
        {
            canC4Plant = false;
            Debug.Log("BombSite Out");
        }

        if (other.gameObject.CompareTag("Store"))
        {
            canIOpenStore = false;

        }

    }




    private void setRagdoll(bool state)
    {
        skeleton_rg = transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in skeleton_rg)
        {
            rb.isKinematic = state;
        }
        transform.GetComponent<Rigidbody>().isKinematic = !state;
    }

    private void setCollider(bool state)
    {
        skeleton_cl = transform.GetComponentsInChildren<Collider>();
  
        foreach (Collider cl in skeleton_cl)
        {
            cl.isTrigger = state;
            if (hp > 0)
            {
                cl.enabled = state;


            }
            else
                cl.enabled = !state;

        }
        transform.GetComponent<BoxCollider>().enabled = !state;
        transform.GetComponent<BoxCollider>().isTrigger = !state;
  
    }


    private void HandleInput()
    {

        //if (!isLocalPlayer)
        //{
        //    return;
        //}
        // WASD

        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        // Walk - Key : left shift , Crouch - Key : left ctrl
        isWalking = Input.GetButton("Walk") ;
        isCrouch = Input.GetButton("Crouch") ? true : false;


        // Mouse

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        PlayerMovement(dirX, dirZ, mouseX, mouseY, isWalking, isCrouch);
        
        



        // Jump - Key : Space
        
        
        PlayerJump();
        


        // Drop Weapon - Key : G
        DropWeapon();


        // Pick up Weapon - Key : E
        PickUpWeaponByHand();
               

        
        
        // Swap Weapon - Key : Mouse Wheel, Q
        SwapWeapon();


        // OpenStoreUI - Key : B;
        OpenStoreUI();

    }

    private void OpenStoreUI()
    {
        if(canIOpenStore)
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                storeUI.SetActive(true);
            }

        }
    }



    private void PlayerMovement(float _dirX, float _dirZ, float mouseX, float mouseY, bool _isWalking ,bool _isCrouch)
    {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}
        // Movement


        //float _dirX = dirX;
        //float _dirZ = dirZ;
        //fpsCam.transform.position.y = playerHead.transform.position.y; 
        fpsCam.transform.position = new Vector3(
        fpsCam.transform.position.x,
        playerHead.transform.position.y,
        fpsCam.transform.position.z
        );

        // mouse
        float _mouseX = mouseX * Time.deltaTime * mouseSensitivity;
        float _mouseY = mouseY * Time.deltaTime * mouseSensitivity;


        Vector3 direction = new Vector3(_dirX, 0, _dirZ);
        float gravity = -9.81f;







        // mouse rotation
        xRotation -= _mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        player_tr.Rotate(Vector3.up * _mouseX);
        viewModel_tr.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //_isWalking = false;
        //_isCrouch = false;

        if (direction != Vector3.zero)
        {
            if (!_isWalking && !_isCrouch)
            {

                isRunning = true;
                this.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);


            }

            if (_isWalking)
            {

                isRunning = false;
                _isCrouch = false;
                this.transform.Translate(direction.normalized * moveSpeed * 0.5f * Time.deltaTime);

            }

            if (_isCrouch)
            {

                _isWalking = false;
                isRunning = false;
                PlayerCrouch(direction);

            }

        }

            player_movement_ani.SetBool("Run", isRunning);
            player_movement_ani.SetBool("Walk", _isWalking);
            player_movement_ani.SetBool("Crouch", _isCrouch);



            player_movement_ani.SetFloat("DirX", direction.x);
            player_movement_ani.SetFloat("DirZ", direction.z);


        


    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space))
        { 
            player_rg.velocity = Vector3.zero;
            player_rg.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void PlayerCrouch(Vector3 direction)
    {
        //Vector3 newPosition = new Vector3(
        //fpsCam.transform.position.x,
        //playerHead.transform.position.y,
        //fpsCam.transform.position.z
        //);           
        //fpsCam.transform.position = newPosition;


        this.transform.Translate(direction.normalized * moveSpeed * 0.3f * Time.deltaTime);
    
    }



    private void DropWeapon()
    {
        if (Input.GetKey(KeyCode.G))
        {
            weaponHolder.WeaponDrop(fpsCam.transform.position + fpsCam.transform.forward, playerWeapon_List[currentWeaponIndex]);
            if (playerWeapon_List[currentWeaponIndex].type != WeaponSetting.Type.Knife)
            {
                playerWeapon_List[currentWeaponIndex].gameObject.SetActive(false);
                playerWeapon_List[currentWeaponIndex] = null;
                currentWeaponIndex = 2; // knife index
                ActivateWeaponModel(currentWeaponIndex);
            }
        }

    }


    private void PickUpWeaponByHand()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 5f))
            {
                if (TryGetComponent<WeaponWorldDrop>(out WeaponWorldDrop pickupGun))
                {
                    PickUpWeapon(pickupGun);
                }
            }
        }
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

        for(int i = 0; i < playerWeapon_List.Length; i++)
        {
            if (playerWeapon_List[i] == null)
                continue;
            else if (playerWeapon_List[i].isActiveAndEnabled)
                currentWeaponIndex = i;

            Debug.Log(currentWeaponIndex);

        }
        
    }





    private void DeactivateAllWeapons()
    {
        // 모든 무기 뷰모델 비활성화
        foreach (var weapon in playerViewModels_List)
        {
            weapon.SetActive(false);
        }
    }

    private void SwapWeapon()
    {

        previousWeaponIndex = currentWeaponIndex;

        scrollValue = (int)Mathf.Clamp(Input.mouseScrollDelta.y, 0, 5f);


        if (scrollValue > 0)
        {
            currentWeaponIndex++;
            while(playerWeapon_List[currentWeaponIndex] == null)
            {
                currentWeaponIndex++;
                
                if (currentWeaponIndex > playerWeapon_List.Length - 1)
                {
                    currentWeaponIndex = 0;

                }
                

            }
            Debug.Log(currentWeaponIndex);
        }

        
        if (scrollValue < 0)
        {
            currentWeaponIndex--;
            while (playerWeapon_List[currentWeaponIndex] == null)
            {
                currentWeaponIndex--;
                if (currentWeaponIndex < 0)
                {
                    currentWeaponIndex = playerWeapon_List.Length - 1;

                }
                

            }
            Debug.Log(currentWeaponIndex);
        }

        
        for (int i = 0; i < playerWeapon_List.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentWeaponIndex = i;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Q) && previousWeaponIndex != -1)
        {
            int temp = previousWeaponIndex;
            previousWeaponIndex = currentWeaponIndex;
            currentWeaponIndex = temp;

        }

        if (previousWeaponIndex == currentWeaponIndex)
        {
            return;
        }

        ActivateWeaponModel(currentWeaponIndex);
    }

    private void ActivateWeaponModel(int weaponIndex)
    {

        //if (playerWeapon_List[weaponIndex] != null)
        //{
            DeactivateAllWeapons();
            playerWeapon_List[weaponIndex].gameObject.SetActive(true);
        //}
        //else
        //{
        //
        //}

    }





    public void TakeDamage(int amount)
    {
        Debug.Log($"Take Damage RPC : {amount}");
        hp -= amount;
        if(hp <= 0)
        {
            Die();
        }

    }


    //[ClientRpc]
    public void Die()
    {
        Debug.Log($"Die RPC");

        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<Animator>().enabled = false;
        player_movement_ani = null;
        setRagdoll(false);
        setCollider(false);

    }

    public void ReceiveGameEvent(string message)
    {
        Debug.Log($"플레이어가 게임 이벤트를 받음 : {message}");
    }
}
