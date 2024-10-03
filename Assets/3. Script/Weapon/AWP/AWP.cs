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
        reward = 150;
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

        // 부모 오브젝트의 자식 중에서 Weapon Camera를 찾음
        weaponHolder = transform.parent.parent; // v_awp의 부모는 WeaponHolder
        weaponCamera = weaponHolder.parent.Find("Weapon Camera").gameObject;

        animator = GetComponent<Animator>();
        animator_w = transform.parent.GetComponentInParent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        player = GetComponentInParent<PlayerControl>();
        SetState(new DrawState());
        UIInit();


    }

    private void OnEnable()
    {
        UIInit();
        SetAnimator();

    }

    private void UIInit()
    {
        scopeOverlay = player.scopeOverlay;
        Ammo_ui = GameObject.Find("Ammo").GetComponent<TMP_Text>();
        Ammo_ui.text = $"{currentAmmo} || {maxAmmo}";
        player.crosshairUI.SetActive(false);

    }
    private void OnDisable()
    {
        player.crosshairUI.SetActive(true);
    }

    void SetAnimator()
    {
        for (int i = 1; i < animator_w.layerCount; i++)
        {
            animator_w.SetLayerWeight(i, 0);
        }
        animator_w.SetLayerWeight(animator_w.GetLayerIndex("AWP"), 1);
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
