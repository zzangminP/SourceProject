using Mirror;
using System.Collections.Generic;


[System.Serializable]
public struct RoomListMessage : NetworkMessage
{

    public List<RoomInfo> rooms;  
    
}
