using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IRightClick<T>
{
    public void OnRigtClick(T weapon);
}


public class RightClickNothing<T> : IRightClick<T> where T : Weapon<T>
{


    public void OnRigtClick(T w)
    {
        return;
    }

}

/// <summary>
/// Set Bust mode
/// </summary>
public class GlockRight : IRightClick<Glock18>
{
    public void OnRigtClick(Glock18 w)
    {
        if(w.isBust == false)
            w.isBust = true;
        else
            w.isBust = false;
        
    }
}

public class ZoomIn<T> : IRightClick<T> where T : Weapon<T>
{

    public void OnRigtClick(T weapon)
    {
        // ZoomIn Code here; 
    }
}



public class KnifeRight<T> : IRightClick<T> where T : Weapon<T>
{


    public void OnRigtClick(T weapon)
    {
        //stab code here;
    }
}

