using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadClick<T>
{
    void OnReloadClick(T weapon);
}


public class SingleReload<T> : IReloadClick<T> where T : Weapon<T>
{


    public void OnReloadClick(T w)
    {
        if(Input.GetKeyDown(KeyCode.R) &&
           w.currentAmmo < w.maxMag &&
           w.maxAmmo > 0 )
        {     
            w.SetState(new SingleStartReloadState<T>());
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
                    w.SetState(new SingleAfterReloadState<T>());

                }


                // Update ui
                w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";

            }

            
        }
    }

    IEnumerator ReloadShell_co(T w)
    {
        w.SetState(new SingleInsertReloadState<T>());
        yield return new WaitForSeconds(1f);

    }
}

public class MagReload<T> : IReloadClick<T> where T : Weapon<T>
{


    public void OnReloadClick(T w)
    {
        // MagazineReload

        if (Input.GetKeyDown(KeyCode.R) && 
            w.currentAmmo < w.maxMag &&
            w.maxAmmo > 0)
        {
            w.SetState(new ReloadingState<T>());


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

public class RClickNothing<T> : IReloadClick<T> where T : Weapon<T>
{


    public void OnReloadClick(T weapon)
    {
        return;
    }
}

