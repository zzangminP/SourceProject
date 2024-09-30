using Mirror;
using TMPro;
using UnityEngine;

public class Knife : Weapon
{
    public Knife()
    {

        damage = 35;
        currentAmmo = 1;
        maxAmmo = 1;
        maxMag = 1;
        pellet = 1;
        range = 1f;
        cost = 0;
        reward = 1500;
        impactForce = 0f;
        fireRate = 0.5f;
        isAuto = true;

        type = Type.Knife;

        leftClick = new KnifeLeft();
        rightClick = new KnifeRight();
        reloadClick = new RClickNothing(); // Only return; -> Do Nothing
        wasdMove = new WASDMove();
    }


    private void Start()
    {
        //if (!player.isLocalPlayer)
        //{
        //    return;
        //}
        Init();

    }

    private void Init()
    {
        animator = GetComponent<Animator>();
        animator_w = transform.parent.GetComponentInParent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        Ammo_ui = GameObject.Find("Ammo").GetComponent<TextMeshProUGUI>();
        playerControl = GetComponentInParent<PlayerControl>();
        SetState(new DrawState());
        UIInit();
        SetAnimator();
    }

    private void OnEnable()
    {
        UIInit();
        SetAnimator();

    }


    private void UIInit()
    {
        Ammo_ui.text = $" ";
    }
    void SetAnimator()
    {
        for (int i = 1; i < animator_w.layerCount; i++)
        {
            animator_w.SetLayerWeight(i, 0);
        }
        animator_w.SetLayerWeight(animator_w.GetLayerIndex("Knife"), 1);
    }

    private void Update()
    {
        if(!playerControl.isLocalPlayer)
        {
            return;
        }
        StrategyControl();
        //Debug.Log(currentState);


    }


    void StrategyControl()
    {
        if (!playerControl.isLocalPlayer)
        {
            return;
        }
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
