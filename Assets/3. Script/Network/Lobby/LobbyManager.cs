using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using JetBrains.Annotations;

public class LobbyManager : MonoBehaviour
{

    [Header("Join Room")]
    public GameObject joinRoomUIObject;
    public Button openJoinRoomUIButton;
    public Button closeJoinRoomUIButton;
    public Button joinRoomButton;
    public TMP_InputField inputField;

    [Header("Create Rooms")]
    public Button createRoomButton;


    private void Start()
    {

        openJoinRoomUIButton.onClick.AddListener(JoinRoomUI);
        closeJoinRoomUIButton.onClick.AddListener(JoinRoomUI);
        joinRoomButton.onClick.AddListener(JoinRoom);
        createRoomButton.onClick.AddListener(CreateRoom);
        

    }



    private void JoinRoomUI()
    {


        joinRoomUIObject.SetActive(!joinRoomUIObject.gameObject.activeSelf);



    }

    private void CreateRoom()
    {

        //CSNetworkManager.singleton.StartHost();
        CSNetworkManager.singleton.StartHost();
    }


    private void JoinRoom()
    {
        //CSNetworkManager.singleton.networkAddress = inputField.text;
        //CSNetworkManager.singleton.StartClient();

        CSNetworkManager.singleton.networkAddress = inputField.text;
        CSNetworkManager.singleton.StartClient();

    }

}
