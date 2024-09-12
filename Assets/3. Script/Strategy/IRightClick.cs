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

    public void OnRigtClick(Weapon weapon)
    {
        // ZoomIn Code here; 
    }
}



public class KnifeRight : IRightClick
{


    public void OnRigtClick(Weapon weapon)
    {
        //stab code here;
    }
}

