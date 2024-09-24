using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class C4 : Weapon
{
    public C4()
    {
        damage = 500;
        currentAmmo = 0;
        maxAmmo = 0;
        maxMag = 0;
        pellet = 0;
        range = 100f;
        cost = 0;
        reward = 0;
        impactForce = 5000f;
        fireRate = 0f;
        isAuto = false;

        type = Type.C4; 

        leftClick = new C4Left();
        rightClick = new RightClickNothing();
        reloadClick = new RClickNothing();
        wasdMove = new WASDMove();
        
    }

    // maybe change class.. 
    // PlayerControl -> Player
    public PlayerControl player;
        

    private void Start()
    {
        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        animator_w = transform.parent.GetComponentInParent<Animator>();
        //Ammo_ui = GameObject.Find("Ammo").GetComponent<TMP_Text>();
        player = GetComponentInParent<PlayerControl>();
        SetState(new DrawState());
        UIInit();
    }

    private void OnEnable()
    {
        UIInit();
    }

    private void UIInit()
    {

        // UI for c4?
        
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
