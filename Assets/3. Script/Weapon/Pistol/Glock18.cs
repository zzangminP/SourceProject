using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Glock18 : Weapon
{

     public Glock18() { 
        damage      = 25;
        currentAmmo = 20;
        maxAmmo     = 120;
        maxMag      = 20;
        pellet      = 1; 
        range       = 100f;
        cost        = 200;
        reward      = 300;      
        impactForce = 50f;
        fireRate    = 6.6f;
        isAuto      = true;
        isBurst     = false;

        type = Type.Glock18;
        
        leftClick   = new PistolLeft();
        rightClick  = new GlockRight();
        reloadClick = new MagReload();
        wasdMove    = new WASDMove();

     }




    private void Start()
    {
        Init();
    }

    private void Init()
    {
        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        animator_w = transform.parent.GetComponentInParent<Animator>();
        Ammo_ui = GameObject.Find("Ammo").GetComponent<TMP_Text>();

        UIInit();
        SetState(new DrawState());
        SetAnimator();
    }

    private void OnEnable()
    {
        UIInit();
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
        animator_w.SetLayerWeight(animator_w.GetLayerIndex("Glock18"), 1);
    }

    private void Update()
    {
        StrategyControl();
        //Debug.Log(currentState);
    
    
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
