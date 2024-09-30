using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTeam : NetworkBehaviour
{

    public Button CTButton;
    public Button TButton;

    public Canvas UI;

    [SyncVar] public string teamSelection;

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



    [Command(requiresAuthority = false)]
    void SelectCT()
    {
        NetworkConnectionToClient sender = null;
        teamSelection = "CT";

        gameObject.SetActive(false);
        GameObject playerPrefab = Instantiate(CSNetworkManager.singleton.playerPrefab_CT);
        playerPrefab.GetComponent<PlayerControl>().team = teamSelection;

        NetworkServer.Spawn(playerPrefab, sender);

    }

    [Command(requiresAuthority = false)]
    void SelectT()
    {
        NetworkConnectionToClient sender = null;
        teamSelection = "T";
        
        gameObject.SetActive(false);
        GameObject playerPrefab = Instantiate(CSNetworkManager.singleton.playerPrefab_T);
        playerPrefab.GetComponent<PlayerControl>().team = teamSelection;

        NetworkServer.Spawn(playerPrefab, sender);

    }
}
