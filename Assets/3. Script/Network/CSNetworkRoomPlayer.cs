using Mirror;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class CSNetworkRoomPlayer : NetworkRoomPlayer
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.F1))
        {
            CmdChangeReadyState(!readyToBegin);
        }
    }



}
