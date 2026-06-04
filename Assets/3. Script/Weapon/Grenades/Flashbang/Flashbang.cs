using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionEffect;
    public float radius = 50f;
    private float countdown;
    private bool hasExploded = false;
    public Camera cam;
    public AudioSource explosionSound;

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
        FlashPlayers();
        FlashDummys();
        explosionSound.Play();




        Destroy(Instantiate(explosionEffect, transform.position, transform.rotation), 0.1f);
    }




    void FlashPlayers()
    {
        int layerMask = LayerMask.GetMask("Player");
        Collider[] players = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider player in players)
        {

            cam = player.GetComponentInChildren<Camera>(true);

            Camera[] cameras = player.GetComponentsInChildren<Camera>();
            foreach (Camera c in cameras)
            {
                if (c.CompareTag("MainCamera")) 
                {
                    cam = c;
                    break;
                }
            }

            // 카메라가 없으면 continue로 넘어감
            if (cam == null)
            {
                Debug.LogWarning("플레이어에 카메라가 없습니다.");
                continue;
            }

            if (CheckVisibility())
            {
                FlashEffect flashEffect = player.GetComponentInChildren<FlashEffect>();
                if (flashEffect != null)
                {
                    flashEffect.FlashScreen();
                }
                else
                {
                    Debug.LogWarning("플래시 효과가 있는 스크립트를 찾을 수 없습니다.");
                }
            }
        }

        hasExploded = true;
    }

    void FlashDummys()
    {
        int layerMask = LayerMask.GetMask("Dummy");
        Collider[] dummies = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider dummyCollider in dummies)
        {
            Dummy dummy = dummyCollider.GetComponent<Dummy>();

            if (dummy != null && IsInDummySight(dummy))
            {

                dummy.GetComponent<StateMachine>().ChangeState(new StunState());
            }
        }

        hasExploded = true;
    }


    bool IsInDummySight(Dummy dummy)
    {
        Vector3 directionToFlashbang = transform.position - dummy.viewPoint.transform.position;
        float distanceToFlashbang = directionToFlashbang.magnitude;


        if (distanceToFlashbang <= dummy.sightDistance)
        {

            float angleToFlashbang = Vector3.Angle(dummy.viewPoint.transform.forward, directionToFlashbang);

            if (angleToFlashbang <= dummy.fieldOfView / 2)
            {

                Ray ray = new Ray(dummy.viewPoint.transform.position, directionToFlashbang.normalized);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, dummy.sightDistance))
                {
                    if (hitInfo.transform.gameObject == this.gameObject)
                    {
                        return true; 
                    }
                }
            }
        }

        return false; 
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

