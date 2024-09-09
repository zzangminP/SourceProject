using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRightClick
{
    public void OnRigtClick(Weapon weapon);
}


public class RightClickNothing : IRightClick
{


    public void OnRigtClick(Weapon weapon)
    {
        return;
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

