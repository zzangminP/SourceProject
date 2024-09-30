using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<Transform> ctSpawnPoint = new List<Transform>();
    public List<Transform> tSpawnPoint = new List<Transform>();
    void Awake()
    {
        GameManager.instance.ctSpawnPoint = ctSpawnPoint;
        GameManager.instance.tSpawnPoint = tSpawnPoint;

    }
}
