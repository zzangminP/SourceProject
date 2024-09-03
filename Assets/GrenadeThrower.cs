using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeThrower : MonoBehaviour
{

    public float throwForce = 40f;
    public GameObject grenadePrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {        
            ThrowGrenade();

        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position + transform.forward, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

    }
}
