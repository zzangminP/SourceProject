using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomItem : MonoBehaviour
{

    public TextMeshProUGUI roomNameText;    
    public TextMeshProUGUI playerCountText;
    public Button joinButton;

    private RoomInfo roomInfo;

    public void Setup(RoomInfo room, System.Action<RoomInfo> onJoinButtonClicked)
    {
        roomInfo = room;
        roomNameText.text = room.roomName;
        playerCountText.text = $"{room.currentPlayers}/{room.maxPlayers}";
        joinButton.onClick.AddListener(() => onJoinButtonClicked(roomInfo));
    }

}
