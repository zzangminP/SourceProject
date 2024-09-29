using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    void OnPlayerDie(PlayerControl player);
}

public interface IPlayer
{
    void ReceiveGameEvent(string message);
}
