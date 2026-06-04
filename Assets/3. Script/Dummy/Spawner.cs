using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] dummysPrefab = new GameObject[8];
    public GameObject[] dummysInGame = new GameObject[8];
    public Transform[] dummySpawnPoints = new Transform[8];

    public Transform dummysParent;

    private Coroutine[] respawnCoroutines = new Coroutine[9]; // 각 더미의 코루틴 참조 변수

    private void Start()
    {
        SpawnDummy();
    }

    private void Update()
    {
        RespawnDummy();
    }

    void SpawnDummy()
    {
        for (int i = 0; i < dummysPrefab.Length; i++)
        {
            dummysInGame[i] = Instantiate(dummysPrefab[i], dummySpawnPoints[i].position,
                                          dummySpawnPoints[i].rotation, dummysParent);
            dummysInGame[i].SetActive(true);
        }
    }

    void RespawnDummy()
    {
        for (int i = 0; i < dummysPrefab.Length; i++)
        {
            if (dummysInGame[i] == null && respawnCoroutines[i] == null) 
            {

                respawnCoroutines[i] = StartCoroutine(RespawnDummyCO(i));
            }
        }
    }

    IEnumerator RespawnDummyCO(int i)
    {
        yield return new WaitForSeconds(15f);


        dummysInGame[i] = Instantiate(dummysPrefab[i], dummySpawnPoints[i].position,
                                      dummySpawnPoints[i].rotation, dummysParent);

        dummysInGame[i].SetActive(true);
        


        respawnCoroutines[i] = null;
    }
}