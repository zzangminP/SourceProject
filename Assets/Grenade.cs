using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 300f;
    public int damage = 500;

    public GameObject explosionEffect;
    private float countdown;
    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        // 폭발 효과 생성
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // 플레이어와 Prop 레이어만을 대상으로 처리하도록 레이어 마스크 설정
        int layerMask = LayerMask.GetMask("ChestAndArms", "Prop");

        // 반경 내의 충돌체를 찾음
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider nearbyObject in colliders)
        {
            Debug.Log(nearbyObject);
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Dummy hitPlayer = nearbyObject.GetComponentInParent<Dummy>();

            if (rb != null)
            {

                if (hitPlayer != null)
                {
                    hitPlayer.TakeDamage(damage);
                    Debug.Log("Hit player with damage: " + damage);
                }

                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Debug.Log("Boom!");

        // 수류탄 오브젝트 파괴
        Destroy(gameObject);
    }
}
