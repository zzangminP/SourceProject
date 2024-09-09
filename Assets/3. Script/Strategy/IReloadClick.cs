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
        if (Input.GetKeyDown(KeyCode.R) && w.currentAmmo < w.maxMag)
        {
            w.SetState(new ReloadingState());
            if (w.maxAmmo >= w.maxMag)
            {
                int tempAmmo = (w.maxMag - w.currentAmmo);
                w.maxAmmo -= tempAmmo;
                w.currentAmmo += tempAmmo;
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

