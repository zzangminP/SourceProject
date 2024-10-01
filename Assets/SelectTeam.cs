using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTeam : NetworkBehaviour
{

    public Button CTButton;
    public Button TButton;

    public GameObject UI;
    public GameObject playerList;

    public string teamSelection;

    public List<Transform> ctSpawnPositions = new List<Transform>();
    public List<Transform> tSpawnPositions = new List<Transform>();
    private void Start()
    {
        //CTButton = GameObject.Find("CT").GetComponent<Button>();
        //TButton = GameObject.Find("T").GetComponent<Button>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CTButton.onClick.AddListener(SelectCT);
        TButton.onClick.AddListener(SelectT);
    }
    public override void OnStartClient()
    {
        UI.gameObject.SetActive(true);

    }



    [Command(requiresAuthority = false)]
    void SelectCT()
    {
        NetworkConnectionToClient sender = null;
        teamSelection = "CT";

        gameObject.SetActive(false);
        GameObject playerPrefab = Instantiate(CSNetworkManager.singleton.playerPrefab_CT, GameManager.instance.ctSpawnPoint[0].transform.position, Quaternion.identity, playerList.transform);
        playerPrefab.GetComponent<PlayerControl>().team = teamSelection;

        NetworkServer.ReplacePlayerForConnection(sender, playerPrefab, true);
        UI.gameObject.SetActive(false);

    }

    [Command(requiresAuthority = false)]
    void SelectT()
    {
        NetworkConnectionToClient sender = null;
        teamSelection = "T";
        
        gameObject.SetActive(false);
        GameObject playerPrefab = Instantiate(CSNetworkManager.singleton.playerPrefab_T, GameManager.instance.tSpawnPoint[0].transform.position, Quaternion.identity, playerList.transform);
        playerPrefab.GetComponent<PlayerControl>().team = teamSelection;

        NetworkServer.ReplacePlayerForConnection(sender, playerPrefab, true); 
        UI.gameObject.SetActive(false);
    }
}
