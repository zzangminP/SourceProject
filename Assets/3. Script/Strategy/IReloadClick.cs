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

        if ((w.currentState is FireOneState ||
             w.currentState is SingleFiringState))
             return;

        if(Input.GetKeyDown(KeyCode.R) &&
           w.currentAmmo < w.maxMag &&
           w.maxAmmo > 0 )
        {     
            w.SetState(new SingleStartReloadState());
            w.animator_w.SetTrigger("SingleReload");            
            if (Input.GetKeyDown(KeyCode.R))
            {
                while (w.currentAmmo < w.maxMag && 
                       w.maxAmmo > 0)
                {
                    w.StartCoroutine(ReloadShell_co(w));
                    //w.SetState(new SingleInsertReloadState());


                }
                if (w.currentAmmo == w.maxMag || w.maxAmmo == 0)
                {
                    w.SetState(new SingleAfterReloadState());
                    w.animator_w.SetTrigger("XMReloadEnd");

                }


                // Update ui
                w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";

            }

            
        }
    }

    IEnumerator ReloadShell_co(Weapon w)
    {
        XM1014 _w = w as XM1014;
        _w.SetState(new SingleInsertReloadState());
        yield return new WaitForSeconds(_w.reloadTime); 
        _w.reload_audio.Play();
        _w.currentAmmo += 1;
        _w.maxAmmo -= 1;

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
            w.animator_w.SetTrigger("MagReload");
            w.StartCoroutine(MagReloadCo(w));            
        }
     

    }

    IEnumerator MagReloadCo(Weapon w)
    {

        yield return new WaitForSeconds(w.reloadTime);

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

public class RClickNothing : IReloadClick
{


    public void OnReloadClick(Weapon weapon)
    {
        return;
    }
}

