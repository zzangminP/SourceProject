using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AK47 : Weapon
{

    public AK47()
    {
        leftClick = new AK47Left();
        rightClick = new RightClickNothing();
        reloadClick = new MagReload();
        wasdMove = new WASDMove();
        damage = 27;
        currentAmmo = 30;
        maxAmmo = 90;
        maxMag = 30;
        range = 100f;
        cost = 2700;
        impactForce = 50f;

    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        Ammo_ui = GameObject.Find("Ammo").GetComponent<TMP_Text>();
        SetState(new DrawState());
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
        
    }

    void StrategyControl()
    {
        leftClick.OnLeftClick(this);
        rightClick.OnRigtClick(this);
        reloadClick.OnReloadClick(this);
        wasdMove.DefaultWASDMove(this);
    }


}
