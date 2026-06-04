using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowerHE : Weapon
{



    public GrenadeThrowerHE()
    {
        type = WeaponSetting.Type.HE;
        //switch(selectedType)
        //{
        //    case GESeries.HE:
        //        type = WeaponSetting.Type.HE; break;
        //
        //    case GESeries.FL:
        //        type = WeaponSetting.Type.FL; break;
        //    case GESeries.SMOKE:
        //        type= WeaponSetting.Type.SMOKE; break;
        //    default:
        //        type = WeaponSetting.Type.HE;break;
        //}
    }

    public float throwForce = 10f;
    public GameObject grenadePrefab;
    public Animator viewModel_ani;
    public PlayerControl player;
    [SerializeField] private bool isHold = false;


    private void Start()
    {
        viewModel_ani = GetComponent<Animator>();    
    }

    private void OnEnable()
    {
        player = GetComponentInParent<PlayerControl>();
    }

    //private void OnEnable()
    //{
    //    SetAnimator();
    //}

    //void SetAnimator()
    //{
    //    {
    //        for (int i = 1; i < animator_w.layerCount; i++)
    //            animator_w.SetLayerWeight(i, 0);
    //    }
    //    animator_w.SetLayerWeight(animator_w.GetLayerIndex("Knife"), 1);
    //}






    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isHold = true;
            viewModel_ani.SetTrigger("GRENHold");

        }
            if (Input.GetKeyUp(KeyCode.Mouse0) && isHold == true)
            {
                ThrowGrenade();
                viewModel_ani.SetTrigger("GRENFire");
                player.playerWeapon_List[3] = null;
                gameObject.SetActive(false);
            }
        Debug.Log(type);
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position + transform.forward, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

    }
}
