using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Unity.VisualScripting;
public class LobbyManager : MonoBehaviour
{

    public TMP_InputField inputField;

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



    private List<RoomInfo> rooms = new List<RoomInfo>();

    private void Start()
    {
        
        openRoomListButton.onClick.AddListener(RoomListUI);
        closeRoomListButton.onClick.AddListener(RoomListUI);

        openCreateRoomUIButton.onClick.AddListener(CreateRoomUI);
        closeCreateRoomUIButton.onClick.AddListener(CreateRoomUI);
        createRoomButton.onClick.AddListener(CreateRoom);
    }

    private void FindRoomList()
    {
        RoomListObject.SetActive(true);
        CSNetworkManager.singleton.networkAddress = "localhost";
        CSNetworkManager.singleton.StartClient();
        RefreshRoomList();

    }

    private void RefreshRoomList()
    {
        foreach (Transform i in showRoomList.transform)
        {
            Destroy(i.gameObject);
        }

        foreach(RoomInfo i in rooms)
        {
            GameObject room = Instantiate(roomListPrefab, showRoomList.transform);
        }
    }




    private void RoomListUI()
    {


        RoomListObject.SetActive(!RoomListObject.gameObject.activeSelf);

    }

    private void CreateRoomUI()
    {
        createRoomUIObject.SetActive(!createRoomUIObject.gameObject.activeSelf);

    }


    private void CreateRoom()
    {

        string roomName = inputField.text;


        CSNetworkManager.singleton.StartHost();

    }


}
