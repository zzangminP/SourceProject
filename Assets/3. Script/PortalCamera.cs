using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = transform.position - (player.transform.position - transform.position);

        camera.transform.LookAt(transform.position);
    }
}
