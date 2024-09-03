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
        // ���� ȿ�� ����
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // �÷��̾�� Prop ���̾�� ������� ó���ϵ��� ���̾� ����ũ ����
        int layerMask = LayerMask.GetMask("ChestAndArms", "Prop");

        // �ݰ� ���� �浹ü�� ã��
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

        // ����ź ������Ʈ �ı�
        Destroy(gameObject);
    }
}
