using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour, IGameManager
{
    public static GameManager instance = null;


    #region Events

    public delegate void GameEvent();
    public static event GameEvent OnGameStart;
    public static event GameEvent OnRoundStart;
    public static event GameEvent OnRoundEnd;
    public static event GameEvent OnGameEnd;

    #endregion


    [SerializeField] private List<IPlayer> players = new List<IPlayer>();

    private void Start()
    {
        if (instance == null && SceneManager.GetActiveScene().name == "de_dust2")
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void OnPlayerDie(PlayerControl player)
    {
        Debug.Log($"GameManager: {player.gameObject.name}이(가) 죽었습니다.");

        // 다른 플레이어에게 알림을 보낼 수 있음
        foreach (var p in players)
        {
            p.ReceiveGameEvent($"{player.gameObject.name} died.");
        }
    }

    public void RegisterPlayer(IPlayer player)
    {
        players.Add(player);
    }

    public void UnregisterPlayer(IPlayer player) 
    { 
        players.Remove(player); 
    }
}
