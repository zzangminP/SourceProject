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



        leftClick = new KnifeLeft();
        rightClick = new KnifeRight();
        reloadClick = new RClickNothing(); // Only return; -> Do Nothing
        wasdMove = new WASDMove();
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
        rightClick.OnRightClick(this);
        reloadClick.OnReloadClick(this);
        wasdMove.DefaultWASDMove(this);
    }

}
