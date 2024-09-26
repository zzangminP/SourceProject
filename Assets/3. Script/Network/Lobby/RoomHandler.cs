using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : NetworkBehaviour
{
    public static RoomHandler instance;

    private void Awake()
    {
        instance = this;
    }


    [Command]
    public void CmdSendRoomInfoToServer(RoomInfo roomInfo)
    {
        CSNetworkManager.singleton.Addroom(roomInfo);
    }


}
