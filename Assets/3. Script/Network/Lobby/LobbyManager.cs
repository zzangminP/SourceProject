using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public struct EmptyMessage : NetworkMessage
{
    // EMPTY;
}

public class LobbyManager : MonoBehaviour
{


    [Header("Find Rooms")]    
    public GameObject RoomListObject;
    public GameObject showRoomList;
    public GameObject roomListPrefab;
    public Button openRoomListButton;
    public Button closeRoomListButton;

    [Header("Create Room")]
    public GameObject createRoomUIObject;
    public Button openCreateRoomUIButton;
    public Button closeCreateRoomUIButton;
    public Button createRoomButton;
    public TMP_InputField inputField;



    private List<RoomInfo> roomInfos = new List<RoomInfo>();

    private void Start()
    {
        
        openRoomListButton.onClick.AddListener(RoomListUI);
        closeRoomListButton.onClick.AddListener(RoomListUI);

        openCreateRoomUIButton.onClick.AddListener(CreateRoomUI);
        closeCreateRoomUIButton.onClick.AddListener(CreateRoomUI);
        createRoomButton.onClick.AddListener(CreateRoom);
    }









    private void RoomListUI()
    {


        RoomListObject.SetActive(!RoomListObject.gameObject.activeSelf);

        if (RoomListObject.activeSelf)
        {

            NetworkClient.Connect("localhost");

            StartCoroutine(RequestRoomListWhenConnected());
        }
        else
        {
            NetworkClient.Disconnect();
        }


    }


    IEnumerator RequestRoomListWhenConnected()
    {
        while (!NetworkClient.isConnected)
        {
            yield return null;
        }
        Debug.Log("Connected to Server, requsting room list");
        NetworkClient.Send(new EmptyMessage());
    }
    private void CreateRoomUI()
    {
        createRoomUIObject.SetActive(!createRoomUIObject.gameObject.activeSelf);

    }



    private void CreateRoom()
    {
        //RoomInfo roomInfo = new RoomInfo();
        //roomInfo.roomName = inputField.text;
        //roomInfo.iPAddress = "localhost"; 
        //roomInfo.port = 7777; 
        //roomInfo.maxPlayers = 10; 
        ////roomInfo.SendRoomInfoMessage(inputField.text, "localhost", 7777,10,1);
        RoomInfo newRoom = new RoomInfo
        {
            roomName = inputField.text,
            iPAddress = "localhost",
            port = 7777,
            maxPlayers = 10,
            currentPlayers = 1
        };



        //CSNetworkManager.singleton.Addroom(newRoom);
        CSNetworkManager.singleton.StartHost();
        //RoomHandler.instance.CmdSendRoomInfoToServer(roomInfo);
    }


    private void JoinRoom(RoomInfo room)
    {
        //Debug.Log($"Joining room: {room.roomName}");
        CSNetworkManager.singleton.networkAddress = room.iPAddress;
        CSNetworkManager.singleton.StartClient();

    }

}
