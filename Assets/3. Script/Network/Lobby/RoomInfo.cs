using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoomInfo
{

    //public struct RoomInfoMessage : NetworkMessage
    //{

        public string roomName;
        public string iPAddress;
        public int port;
        public int maxPlayers;
        public int currentPlayers;

    //}
}
