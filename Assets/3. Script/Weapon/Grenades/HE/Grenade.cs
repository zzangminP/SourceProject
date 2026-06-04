using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{




    public float delay = 3f;
    public float radius = 5f;
    public float force = 300f;
    public int damage = 98;
    private int calcDamage = 0;

    public GameObject explosionEffect;
    private float countdown;
    private bool hasExploded = false;
    public AudioSource explosionSound;

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

        Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionSound.Play();
        
        int layerMask = LayerMask.GetMask("Head", "Prop");

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider nearbyObject in colliders)
        {

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Dummy hitPlayer = nearbyObject.GetComponentInParent<Dummy>();

            if (rb != null)
            {

                if (hitPlayer != null)
                {
                    calcDamage = (int)(damage * Mathf.Pow((float)(1 - ((transform.position - hitPlayer.transform.position).magnitude) / radius), 2));
                    //hitPlayer.TakeDamage(calcDamage);
                    
                    hitPlayer.TakeDamage(calcDamage);
                    Debug.Log("Hit player with damage: " + calcDamage);
                }

                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Debug.Log("Boom!");

        // ¼ö·ùÅº ¿ÀºêÁ§Æ® ÆÄ±«
        Destroy(gameObject);


    }








}
