using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AWP : Weapon
{
    public AWP() {

        damage = 110;
        currentAmmo = 5;
        maxAmmo = 30;
        maxMag = 5;
        pellet = 1;
        range = 100f;
        cost = 4750;
        reward = 50;
        impactForce = 200f;
        fireRate = 10f;
        isAuto = false;

        type = Type.AWP;

        leftClick = new AWPLeft();
        rightClick = new ZoomIn();
        reloadClick = new MagReload();
        wasdMove = new WASDMove();


    }

    public GameObject scopeOverlay;
    public byte isScoped = 0;
    public PlayerControl player;


    private void Start()
    {
        Init();

    }

    private void Init()
    {

        // �θ� ������Ʈ�� �ڽ� �߿��� Weapon Camera�� ã��
        weaponHolder = transform.parent.parent; // v_awp�� �θ�� WeaponHolder
        weaponCamera = weaponHolder.parent.Find("Weapon Camera").gameObject;

        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
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
        scopeOverlay = player.scopeOverlay;
        Ammo_ui = GameObject.Find("Ammo").GetComponent<TMP_Text>();
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
