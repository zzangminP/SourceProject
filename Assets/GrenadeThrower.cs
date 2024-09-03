using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeThrower : MonoBehaviour
{

    public float throwForce = 10f;
    public GameObject grenadePrefab;
    public GameObject viewModel;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {        
            ThrowGrenade();

        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, viewModel.transform.position + viewModel.transform.forward, viewModel.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(viewModel.transform.forward * throwForce, ForceMode.VelocityChange);

    }
}
