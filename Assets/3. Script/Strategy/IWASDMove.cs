using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWASDMove 
{
    public void DefaultWASDMove(Weapon weapon);
}

public class WASDMove : IWASDMove
{

    public void DefaultWASDMove(Weapon weapon)
    {
        return;
    }
}
