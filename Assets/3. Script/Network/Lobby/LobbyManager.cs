using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LobbyManager : MonoBehaviour
{

    public TMP_InputField inputField;
    public Button createRoomButton;
    public Button findRoomButton;
    public GameObject showRoomList;
    public GameObject roomListPrefab;


    private List<RoomInfo> rooms = new List<RoomInfo>();

    private void Start()
    {
        createRoomButton.onClick.AddListener(CreateRoom);
        findRoomButton.onClick.AddListener(FindRoomList);
    }

    private void FindRoomList()
    {
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


    private void CreateRoom()
    {

        string roomName = inputField.text;


        CSNetworkManager.singleton.StartHost();

    }


}
