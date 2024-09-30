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


    public List<PlayerControl> players = new List<PlayerControl>();
    public List<Transform> ctSpawnPoint = new List<Transform>();
    public List<Transform> tSpawnPoint = new List<Transform>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
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

    public void RegisterPlayer(PlayerControl player)
    {
        players.Add(player);
    }

    public void UnregisterPlayer(PlayerControl player) 
    { 
        players.Remove(player); 
    }
}
