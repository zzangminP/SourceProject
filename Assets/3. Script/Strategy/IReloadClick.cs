using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadClick
{
    void OnReloadClick(Weapon weapon);
}


public class SingleReload : IReloadClick
{


    public void OnReloadClick(Weapon w)
    {
        if(Input.GetKeyDown(KeyCode.R) &&
           w.currentAmmo < w.maxMag &&
           w.maxAmmo > 0 )
        {     
            w.SetState(new SingleStartReloadState());
            if(Input.GetKeyDown(KeyCode.R))
            {
                while (w.currentAmmo < w.maxMag && 
                       w.maxAmmo > 0)
                {
                    w.StartCoroutine(ReloadShell_co(w));
                    //w.SetState(new SingleInsertReloadState());
                    w.currentAmmo += 1;
                    w.maxAmmo -= 1;

                }
                if (w.currentAmmo == w.maxMag || w.maxAmmo == 0)
                {
                    w.SetState(new SingleAfterReloadState());

                }


                // Update ui
                w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";

            }

            
        }
    }

    IEnumerator ReloadShell_co(Weapon w)
    {
        w.SetState(new SingleInsertReloadState());
        yield return new WaitForSeconds(1f);

    }
}

public class MagReload : IReloadClick
{


    public void OnReloadClick(Weapon w)
    {
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

