using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTeam : MonoBehaviour
{

    public Button CTButton;
    public Button TButton;

    public GameObject UI;

    public int teamSelection;

    public List<Transform> ctSpawnPositions = new List<Transform>();
    public List<Transform> tSpawnPositions = new List<Transform>();

    public GameObject playerList;

    public GameObject playerPrefab_CT;
    public GameObject playerPrefab_T;

    private void Start()
    {
        //CTButton = GameObject.Find("CT").GetComponent<Button>();
        //TButton = GameObject.Find("T").GetComponent<Button>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CTButton.onClick.AddListener(SelectCT);
        TButton.onClick.AddListener(SelectT);
    }

    //public override void OnStartClient()
    //{
    //    UI.SetActive(true);
    //}

    public void SelectCT()
    {
        teamSelection = 0;
        CSNetworkManager.ReplaceCharacterMessage _replaceMessage = new CSNetworkManager.ReplaceCharacterMessage
        {
            team = teamSelection

        };

        CSNetworkManager.singleton.ReplaceCharacter(_replaceMessage);
        UI.SetActive(false);


    }
    public void SelectT()
    {
        teamSelection = 1;
        CSNetworkManager.ReplaceCharacterMessage _replaceMessage = new CSNetworkManager.ReplaceCharacterMessage
        {
            team = teamSelection

        };


        Debug.Log("_replaceMessage : "+ _replaceMessage);
        CSNetworkManager.singleton.ReplaceCharacter(_replaceMessage);
        UI.SetActive(false);

    }


    //[Command(requiresAuthority = false)]
    //void SelectCT()
    //{
    //    NetworkConnectionToClient sender = null;
    //    teamSelection = "CT";
    //
    //    gameObject.SetActive(false);
    //    GameObject playerPrefab = Instantiate(playerPrefab_CT, GameManager.instance.ctSpawnPoint[0].position, Quaternion.identity, playerList.transform);
    //    playerPrefab.GetComponent<PlayerControl>().team = teamSelection;
    //
    //    NetworkServer.Spawn(playerPrefab, sender);
    //    //NetworkServer.ReplacePlayerForConnection(sender, playerPrefab, true);
    //    UI.SetActive(false);
    //}

    //[Command(requiresAuthority = false)]
    //void SelectT()
    //{
    //    NetworkConnectionToClient sender = null;
    //    teamSelection = "T";
    //    
    //    gameObject.SetActive(false);
    //    GameObject playerPrefab = Instantiate(playerPrefab_T, GameManager.instance.tSpawnPoint[0].position, Quaternion.identity, playerList.transform);
    //    playerPrefab.GetComponent<PlayerControl>().team = teamSelection;
    //
    //    NetworkServer.Spawn(playerPrefab, sender);
    //    //NetworkServer.ReplacePlayerForConnection(sender, playerPrefab, true);
    //    UI.SetActive(false);
    //}
}
