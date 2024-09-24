using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XM1014 : Weapon
{

    public XM1014() 
    {
        // Per pellet dmg
        damage      = 14;
        currentAmmo = 7;
        maxAmmo     = 32;
        maxMag      = 7;
        range       = 100f;
        cost        = 2000;
        reward      = 900;
        impactForce = 50f;
        fireRate    = 2f;
        isAuto      = false;
        pellet      = 6;

        leftClick   = new ShotgunLeft();
        rightClick  = new RightClickNothing();
        reloadClick = new SingleReload();
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
    //
    private void OnEnable()
    {
        UIInit();
        SetAnimator();
    }

    void SetAnimator()
    {
        for (int i = 1; i < animator_w.layerCount; i++)
        {
            animator_w.SetLayerWeight(i, 0);
        }
        animator_w.SetLayerWeight(animator_w.GetLayerIndex("XM1014"), 1);
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
        rightClick.OnRightClick(this);
        reloadClick.OnReloadClick(this);
        wasdMove.DefaultWASDMove(this);
    }

}
