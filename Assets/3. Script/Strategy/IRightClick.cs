using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IRightClick
{
    public void OnRigtClick(Weapon weapon);
}


public class RightClickNothing : IRightClick
{


    public void OnRigtClick(Weapon w)
    {
        return;
    }

}

/// <summary>
/// Set Bust mode
/// </summary>
public class GlockRight : IRightClick
{
    public void OnRigtClick(Weapon w)
    {
        if(w.isBurst == false)
            w.isBurst = true;
        else
            w.isBurst = false;
        
    }
}

public class ZoomIn : IRightClick
{

    public float firstScopedFOV = 40f;
    public float secondScopedFOV = 10f;
    private float normalFOV = 60f;

    public void OnRigtClick(Weapon weapon)
    {
        AWP awp = weapon as AWP;

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (awp.isScoped < 2)
            {
                awp.StartCoroutine(OnScoped(awp));
            }
            else
            {
                OnUnscoped(awp);
            }

        }

    }

    IEnumerator OnScoped(AWP awp)
    {
        if(awp.isScoped == 0)
        {
            yield return new WaitForSeconds(.01f);
            awp.scopeOverlay.SetActive(true);
            awp.weaponCamera.SetActive(false);
            awp.fpsCam.fieldOfView = firstScopedFOV;
            awp.isScoped += 1;
        }
        else if(awp.isScoped == 1)
        {
            yield return new WaitForSeconds(.01f);
            awp.fpsCam.fieldOfView = secondScopedFOV;
            awp.isScoped += 1;
        }
        else
        {
            awp.isScoped = 2;
        }


    }
    void OnUnscoped(AWP awp)
    {
        awp.scopeOverlay.SetActive(false);
        awp.weaponCamera.SetActive(true);
        awp.fpsCam.fieldOfView = normalFOV;
        awp.isScoped = 0;

    }
}



public class KnifeRight : IRightClick
{


    public void OnRigtClick(Weapon weapon)
    {
        //stab code here;
    }
}

