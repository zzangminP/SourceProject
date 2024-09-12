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
        
        leftClick   = new PistolLeft();
        rightClick  = new GlockRight();
        reloadClick = new MagReload();
        wasdMove    = new WASDMove();

     }




    private void Start()
    {
        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        Ammo_ui = GameObject.Find("Ammo").GetComponent<TMP_Text>();
        SetState(new DrawState());
        UIInit();
    }
    
    private void OnEnable()
    {
        UIInit();
    }

    private void UIInit()
    {
        Ammo_ui.text = $"{currentAmmo} || {maxAmmo}";
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
        rightClick.OnRigtClick(this);
        reloadClick.OnReloadClick(this);
        wasdMove.DefaultWASDMove(this);
    }    
    
    

    
    
    
    
    
}
