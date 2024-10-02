using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Weapon
{

    public Grenade()
    {
        damage      = 1;
        currentAmmo = 1;
        maxAmmo     = 1;
        range       = 100f;
        cost        = 300;
        reward      = 300;
        impactForce = 0;
        fireRate    = 0;
        isAuto      = false;

        type        = Type.HE;

        leftClick   = new GELeft();
        rightClick  = new RightClickNothing();
        reloadClick = new RClickNothing();
        wasdMove    = new WASDMove();



    }


    public float delay = 3f;
    public float radius = 5f;
    public float force = 300f;
    public int damage = 98;
    private int calcDamage = 0;

    public GameObject explosionEffect;
    private float countdown;
    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        SetAnimator();
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

    private void OnEnable()
    {
        SetAnimator();
    }

    void SetAnimator()
    {
        for (int i = 1; i < animator_w.layerCount; i++)
        {
            animator_w.SetLayerWeight(i, 0);
        }
        animator_w.SetLayerWeight(animator_w.GetLayerIndex("Knife"), 1);
    }

    void Explode()
    {

        Instantiate(explosionEffect, transform.position, transform.rotation);

        
        int layerMask = LayerMask.GetMask("Head", "Prop");

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider nearbyObject in colliders)
        {

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            PlayerControl hitPlayer = nearbyObject.GetComponentInParent<PlayerControl>();

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

        // ����ź ������Ʈ �ı�
        Destroy(gameObject);


    }








}
