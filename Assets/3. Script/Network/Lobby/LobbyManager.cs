using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{

    //[Header("Join Room")]
    public GameObject joinRoomUIObject;
    public Button gameStartUIButton;
    public TMP_InputField inputField;

    //[Header("Create Rooms")]
    //public Button createRoomButton;


    private void Start()
    {

        //openJoinRoomUIButton.onClick.AddListener(JoinRoomUI);
        //closeJoinRoomUIButton.onClick.AddListener(JoinRoomUI);
        //joinRoomButton.onClick.AddListener(JoinRoom);
        //createRoomButton.onClick.AddListener(CreateRoom);
        gameStartUIButton.onClick.AddListener(SceneChange);


    }

    void SceneChange()
    {
        SceneManager.LoadScene("de_dust2");
    
    }
    


    

}
