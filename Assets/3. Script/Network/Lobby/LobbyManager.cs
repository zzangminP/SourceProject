using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Unity.VisualScripting;
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

    public void OnReceiveRoomList(RoomListMessage msg)
    {
        Debug.Log(msg);
        RefreshRoomList(msg.rooms);
    }


    private void RefreshRoomList(List<RoomInfo> rooms)
    {
        foreach (Transform i in showRoomList.transform)
        {
            Destroy(i.gameObject);
        }

        foreach(RoomInfo i in rooms)
        {
            GameObject room = Instantiate(roomListPrefab, showRoomList.transform);
            RoomItem item = room.GetComponent<RoomItem>();
            item.Setup(i, JoinRoom);
        }
    }




    private void RoomListUI()
    {


        RoomListObject.SetActive(!RoomListObject.gameObject.activeSelf);

        if (RoomListObject.activeSelf)
        {
            NetworkClient.RegisterHandler<RoomListMessage>(OnReceiveRoomList);
            NetworkClient.Connect("localhost");
            if(NetworkClient.isConnected)
            {
                Debug.Log("Conneted");
            }
        }
        else
        {
            NetworkClient.Disconnect();
        }


    }

    private void CreateRoomUI()
    {
        createRoomUIObject.SetActive(!createRoomUIObject.gameObject.activeSelf);

    }



    private void CreateRoom()
    {
        RoomInfo roomInfo = new RoomInfo();
        roomInfo.roomName = inputField.text;
        roomInfo.iPAddress = "localhost"; 
        roomInfo.port = 7777; 
        roomInfo.maxPlayers = 10; 
        //roomInfo.SendRoomInfoMessage(inputField.text, "localhost", 7777,10,1);





        CSNetworkManager.singleton.StartHost();
        RoomHandler.instance.CmdSendRoomInfoToServer(roomInfo);
    }


    private void JoinRoom(RoomInfo room)
    {
        Debug.Log($"Joining room: {room.roomName}");
        CSNetworkManager.singleton.networkAddress = room.iPAddress;
        CSNetworkManager.singleton.StartClient();

    }

}
