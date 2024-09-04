using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionEffect;
    public float radius = 50f;
    private float countdown;
    private bool hasExploded = false;


    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            
            Debug.Log("WTF is happening?");
        }
    }

    void Explode()
    {
        // 폭발 효과 생성
        Instantiate(explosionEffect, transform.position, transform.rotation);
        

        // 섬광탄 기능(플래시 효과 등) 실행
        FlashPlayers();

        // 섬광탄 오브젝트 파괴
        Destroy(gameObject);
    }

    void FlashPlayers()
    {
        int layerMask = LayerMask.GetMask("Player");
        Debug.Log("레이어 체크");


        Collider[] players = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider player in players)
        {

            Debug.Log(player);
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            //PlayerControl hitPlayer;
            player.TryGetComponent<PlayerControl>(out PlayerControl hitPlayer);
            

            if (angle < 90f) // 시야 각도 내에 있으면
            {
                Debug.Log(hitPlayer);
                // 시야 범위에 따라 강한 섬광 효과 적용
                hitPlayer.GetComponentInChildren<FlashEffect>().FlashScreen();
            }
        }
        hasExploded = true;
    }

}

