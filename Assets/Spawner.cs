//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Spawner : MonoBehaviour
//{
//    public GameObject[] dummysPrefab = new GameObject[8];
//    public GameObject[] dummysInGame = new GameObject[8];
//    public Transform[] dummySpawnPoints = new Transform[8];
//
//    public Transform dummysParent;
//
//    private void Start()
//    {
//        SpawnDummy();
//
//    }
//
//    private void Update()
//    {
//        RespawnDummy();
//    }
//
//    void SpawnDummy()
//    {
//
//        for (int i = 0; i < dummysPrefab.Length; i++)
//        {
//            dummysInGame[i] = Instantiate(dummysPrefab[i], dummySpawnPoints[i].transform.position,
//                                          dummySpawnPoints[i].transform.rotation, dummysParent
//                                          );
//            
//        }
//
//
//    }
//    void RespawnDummy()
//    {
//        for (int i = 0; i < dummysPrefab.Length; i++)
//        {
//            if (dummysInGame[i] == null)
//            {
//                StartCoroutine(RespawnDummyCO(i));
//
//            }
//            else
//            {
//                StopCoroutine(RespawnDummyCO(i));
//            }
//
//        }
//
//
//    }
//
//    IEnumerator RespawnDummyCO(int i)
//    {
//        yield return new WaitForSeconds(15f);
//
//
//        dummysInGame[i] = Instantiate(dummysPrefab[i], dummySpawnPoints[i].transform.position,
//                                         dummySpawnPoints[i].transform.rotation, dummysParent
//                                         );
//    }
//
//}


using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] dummysPrefab = new GameObject[8];
    public GameObject[] dummysInGame = new GameObject[8];
    public Transform[] dummySpawnPoints = new Transform[8];

    public Transform dummysParent;

    private Coroutine[] respawnCoroutines = new Coroutine[9]; // �� ������ �ڷ�ƾ ���� ����

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

        // ���� �����
        dummysInGame[i] = Instantiate(dummysPrefab[i], dummySpawnPoints[i].position,
                                      dummySpawnPoints[i].rotation, dummysParent);
        

        // �ڷ�ƾ �Ϸ� �� ������ null�� ����
        respawnCoroutines[i] = null;
    }
}