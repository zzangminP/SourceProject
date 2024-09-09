using System.Collections;
using System.Collections.Generic;
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
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        SetState(new DrawState());
    }

    private void Update()
    {
        //Left(this);
        //Right(this);
        //Reload(this);
        //WASD(this);
        leftClick.OnLeftClick(this);
        rightClick.OnRigtClick(this);
        reloadClick.OnReloadClick(this);
        wasdMove.DefaultWASDMove(this);
        
        
    }


}
