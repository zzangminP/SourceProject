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
        // ���� ȿ�� ����
        Instantiate(explosionEffect, transform.position, transform.rotation);
        

        // ����ź ���(�÷��� ȿ�� ��) ����
        FlashPlayers();

        // ����ź ������Ʈ �ı�
        Destroy(gameObject);
    }

    void FlashPlayers()
    {
        int layerMask = LayerMask.GetMask("Player");
        Debug.Log("���̾� üũ");


        Collider[] players = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider player in players)
        {

            Debug.Log(player);
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            //PlayerControl hitPlayer;
            player.TryGetComponent<PlayerControl>(out PlayerControl hitPlayer);
            

            if (angle < 90f) // �þ� ���� ���� ������
            {
                Debug.Log(hitPlayer);
                // �þ� ������ ���� ���� ���� ȿ�� ����
                hitPlayer.GetComponentInChildren<FlashEffect>().FlashScreen();
            }
        }
        hasExploded = true;
    }

}

