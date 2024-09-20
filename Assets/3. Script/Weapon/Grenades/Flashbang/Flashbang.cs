using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Flashbang : Weapon
{
    public Flashbang() 
    {
        damage      = 0;
        currentAmmo = 1;
        maxAmmo     = 1;
        range       = 100f;
        cost        = 200;
        reward      = 300;
        impactForce = 0;
        fireRate    = 0;
        isAuto      = false;

        type = Type.FL;

        leftClick   = new AK47Left();
        rightClick  = new RightClickNothing();
        reloadClick = new MagReload();
        wasdMove    = new WASDMove();



    }

    public float delay = 3f;
    public GameObject explosionEffect;
    public float radius = 50f;
    private float countdown;
    private bool hasExploded = false;
    public Camera cam;


    void Start()
    {
        countdown = delay;
        //cam = Camera.main;
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

        Destroy(gameObject);
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        FlashPlayers();

        
        //FlashPlayers();

        
        Destroy(Instantiate(explosionEffect, transform.position, transform.rotation), 0.1f);
    }

    void FlashPlayers()
    {
        int layerMask = LayerMask.GetMask("Player");
        //Debug.Log("레이어 체크");
        //
        //
        Collider[] players = Physics.OverlapSphere(transform.position, radius, layerMask);
        //
        foreach (Collider player in players)
        {
        //
        //    Debug.Log(player);
        //    Vector3 directionToPlayer = player.transform.position - transform.position;
        //    float angle = Vector3.Angle(transform.forward, directionToPlayer);
        //
        //    //PlayerControl hitPlayer;
        //    player.TryGetComponent<PlayerControl>(out PlayerControl hitPlayer);
              cam = player.GetComponentInChildren<Camera>();
            //
            //    if (angle < 90f) 
            //    {
            //        Debug.Log(hitPlayer);
            //        
            //        hitPlayer.GetComponentInChildren<FlashEffect>().FlashScreen();
            //        Debug.Log(hitPlayer.GetComponentInChildren<FlashEffect>());
            //    }

            if (CheckVisibility() && cam != null)
            {
                player.GetComponentInChildren<FlashEffect>().FlashScreen();
            }
        }



        hasExploded = true;
    }

    bool CheckVisibility()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var point = transform.position;


        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) > 0)
            {
                Ray ray = new Ray(cam.transform.position, transform.position - cam.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    return hit.transform.gameObject == this.gameObject;
                }
                else
                    return false;
            }
            else
                return false;
        }
        return false;
    }

}

