using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadClick
{
    void OnReloadClick(Weapon weapon);
}


public class SingleReload : IReloadClick
{


    public void OnReloadClick(Weapon weapon)
    {
        // SingleReload code here;
    }
}

public class MagReload : IReloadClick
{


    public void OnReloadClick(Weapon w)
    {
        // MagazineReload
        // MagazineReload
        if (Input.GetKeyDown(KeyCode.R) && 
            w.currentAmmo < w.maxMag &&
            w.maxAmmo > 0)
        {
            w.SetState(new ReloadingState());


            int neededAmmo = w.maxMag - w.currentAmmo;


            if (w.maxAmmo >= neededAmmo)
            {
                w.currentAmmo += neededAmmo;
                w.maxAmmo -= neededAmmo;
            }
            else
            {

                w.currentAmmo += w.maxAmmo;
                w.maxAmmo = 0;
            }

            w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";
        }

    }
}

public class RClickNothing : IReloadClick
{


    public void OnReloadClick(Weapon weapon)
    {
        return;
    }
}

