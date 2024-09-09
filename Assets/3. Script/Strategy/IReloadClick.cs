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


    public void OnReloadClick(Weapon weapon)
    {
        // MagazineReload
    }
}

public class RClickNothing : IReloadClick
{


    public void OnReloadClick(Weapon weapon)
    {
        return;
    }
}

