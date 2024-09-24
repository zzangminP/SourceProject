using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AK47 : Weapon
{

    public AK47()
    {
        damage      = 27;
        currentAmmo = 30;
        maxAmmo     = 90;
        maxMag      = 30;
        pellet      = 1; 
        range       = 100f;
        cost        = 2700;
        reward      = 300;      
        impactForce = 50f;
        fireRate    = 10f;
        isAuto      = true;

        type = Type.AK47;
       
        leftClick   = new AK47Left();
        rightClick  = new RightClickNothing();
        reloadClick = new MagReload();
        wasdMove    = new WASDMove();

    }
    //public Animator animator_w;



    private void Start()
    {
        Init();
    }


    private void Init()
    {

        animator = GetComponent<Animator>();
        animator_w = transform.parent.GetComponentInParent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        Ammo_ui = GameObject.Find("Ammo").GetComponent<TMP_Text>();

        UIInit();
        SetAnimator();
        SetState(new DrawState());

    }

    private void OnEnable()
    {
        UIInit();
        SetAnimator();
    }

    private void UIInit()
    {
        Ammo_ui.text = $"{currentAmmo} || {maxAmmo}";
    }

    void SetAnimator()
    {
        for (int i = 1; i < animator_w.layerCount; i++)
        {
            animator_w.SetLayerWeight(i, 0);
        }
        animator_w.SetLayerWeight(animator_w.GetLayerIndex("AK47"), 1);
    }

    private void Update()
    {
        StrategyControl();   
    }

    void StrategyControl()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }

        leftClick.OnLeftClick(this);
        rightClick.OnRightClick(this);
        reloadClick.OnReloadClick(this);
        wasdMove.DefaultWASDMove(this);
    }


}
