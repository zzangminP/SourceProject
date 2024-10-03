using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{


    public GameObject joinRoomUIObject;
    public Button gameStartUIButton;
    public Button gameExitUIButton;





    private void Start()
    {


        gameStartUIButton.onClick.AddListener(GameStart);


    }

    void GameStart()
    {
        SceneManager.LoadScene("de_dust2");
    
    }
   

    void GameExit()
    {
        Application.Quit();
    }
    


    

}
