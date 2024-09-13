using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ILeftClick
{
    public void OnLeftClick(Weapon weapon);
   
}

public class AK47Left : ILeftClick
{
    bool isFiring = false;

    public void OnLeftClick(Weapon w)
    {
        if (w.currentState is DrawState ||
            w.currentState is ReloadingState)
            return;

        if (Input.GetMouseButtonDown(0))
        {

            w.StartCoroutine(StartAutoFire_Co(w));
        }
        else if (Input.GetMouseButtonUp(0))
        {

            isFiring = false;
            Debug.Log("isFiring? : " + isFiring);
            w.StopCoroutine(StartAutoFire_Co(w));
        }

    }

    IEnumerator StartAutoFire_Co(Weapon w)
    {
        isFiring = true;


        while (isFiring && w.currentAmmo > 0)
        {
            OnAttack(w);

            
            yield return new WaitForSeconds(1f / w.fireRate);
        }

        isFiring = false;
    }

    void OnAttack(Weapon w)
    {
        w.SetState(new FireOneState());
        if (w.currentAmmo > 0 &&
            !(w.currentState is ReloadingState))
        {
            w.currentAmmo -= 1;
            w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";

            RaycastHit hit;
            int layerMask = -1 - (1 << LayerMask.NameToLayer("Player"));

            if (Physics.Raycast(w.fpsCam.transform.position, w.fpsCam.transform.forward, out hit, w.range, layerMask))
            {
                Debug.Log(hit.collider.gameObject.layer);

                Dummy hitPlayer = hit.transform.GetComponentInParent<Dummy>();
                int calcDamage = 0;

                if (hitPlayer != null)
                {
                    switch (hit.collider.gameObject.layer)
                    {
                        case 9:
                            calcDamage = w.damage * 4;
                            break;
                        case 11:
                            calcDamage = (int)(w.damage * 1.25);
                            break;
                        case 12:
                            calcDamage = (int)(w.damage * 0.75);
                            break;
                        default:
                            calcDamage = w.damage;
                            break;
                    } // switch - case

                    hitPlayer.TakeDamage(calcDamage);
                    Debug.Log(calcDamage);

                    if (hitPlayer.hp <= 0)
                    {
                        hitPlayer.GetComponent<Rigidbody>().AddForce(-hit.normal * w.impactForce, ForceMode.Impulse);
                        Debug.Log("Added force to dead player.");
                    }
                } // if hitPlayer != null

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * w.impactForce);
                }
            } // if Physics.Raycast
        } // if w.currentAmmo blablabla....
    } //OnAttack()
}


public class ShotgunLeft : ILeftClick
{
    public void OnLeftClick(Weapon w)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (w.currentState is SingleFiringState)
                return;


            if (w.currentAmmo > 0)
            {
                w.SetState(new SingleFiringState());
                w.currentAmmo -= 1;
                w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";

                RaycastHit hit;

                for (int i = 0; i < w.pellet; i++)
                {


                    Vector3 shotgunRay = ShotgunRay(w);

                    if (Physics.Raycast(w.fpsCam.transform.position, shotgunRay, out hit, w.range))
                    {
                        //Debug.Log(hit.collider.gameObject.layer);
                        //Debug.DrawLine(w.fpsCam.transform.position, hit.point, Color.green, 5f);
                        Dummy hitPlayer = hit.transform.GetComponentInParent<Dummy>();
                        int calcDamage = w.damage;

                        if (hitPlayer != null)
                        {

                            float distance = Vector3.Distance(w.fpsCam.transform.position, hit.point);


                            float distanceFactor = 1 - (distance / w.range);
                            distanceFactor = Mathf.Clamp(distanceFactor, 0.2f, 1f);


                            switch (hit.collider.gameObject.layer)
                            {
                                case 9:
                                    calcDamage = w.damage * 4;
                                    break;
                                case 11:
                                    calcDamage = (int)(w.damage * 1.25);
                                    break;
                                case 12:
                                    calcDamage = (int)(w.damage * 0.75);
                                    break;
                                default:
                                    calcDamage = w.damage;
                                    break;
                            } // switch - case

                            calcDamage = (int)(calcDamage * distanceFactor);

                            hitPlayer.TakeDamage(calcDamage);
                            Debug.Log(calcDamage);

                            if (hitPlayer.hp <= 0)
                            {
                                hitPlayer.GetComponent<Rigidbody>().AddForce(-hit.normal * w.impactForce, ForceMode.Impulse);
                                //Debug.Log("Added force to dead player.");
                            }
                        } // if hitPlayer != null

                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * w.impactForce);
                        }

                    } // if Physics.Raycast
                } // for i < pellet

            } // if w.currentAmmo blablabla....

        } // if input.getkeydown mouseLeft 
    } // OnLeftAttack

    Vector3 ShotgunRay(Weapon w)
    {
        Vector3 direction = w.fpsCam.transform.forward; // your initial aim.
        Vector3 spread = Vector3.zero;
        spread += w.fpsCam.transform.up * Random.Range(-1f, 1f); // add random up or down (because random can get negative too)
        spread += w.fpsCam.transform.right * Random.Range(-1f, 1f); // add random left or right

        direction += spread.normalized * Random.Range(0f, 0.1f);
        Debug.Log(direction);

        return direction;
    }
}

public class PistolLeft : ILeftClick
{
    public void OnLeftClick(Weapon w)
    {


        if (w.currentState is ReloadingState || 
            w.currentState is SingleFiringState)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {



            if (w.currentAmmo > 0)
            {
                w.SetState(new SingleFiringState());
                w.currentAmmo -= 1;
                w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";

                RaycastHit hit;

                if (Physics.Raycast(w.fpsCam.transform.position, w.fpsCam.transform.forward, out hit, w.range))
                {
                    //Debug.Log(hit.collider.gameObject.layer);
                    //Debug.DrawLine(w.fpsCam.transform.position, hit.point, Color.green, 5f);
                    Dummy hitPlayer = hit.transform.GetComponentInParent<Dummy>();
                    int calcDamage = w.damage;

                    if (hitPlayer != null)
                    {

                        float distance = Vector3.Distance(w.fpsCam.transform.position, hit.point);


                        float distanceFactor = 1 - (distance / w.range);
                        distanceFactor = Mathf.Clamp(distanceFactor, 0.2f, 1f);


                        switch (hit.collider.gameObject.layer)
                        {
                            case 9:
                                calcDamage = w.damage * 4;
                                break;
                            case 11:
                                calcDamage = (int)(w.damage * 1.25);
                                break;
                            case 12:
                                calcDamage = (int)(w.damage * 0.75);
                                break;
                            default:
                                calcDamage = w.damage;
                                break;
                        } // switch - case

                        calcDamage = (int)(calcDamage * distanceFactor);

                        hitPlayer.TakeDamage(calcDamage);
                        Debug.Log(calcDamage);

                        if (hitPlayer.hp <= 0)
                        {
                            hitPlayer.GetComponent<Rigidbody>().AddForce(-hit.normal * w.impactForce, ForceMode.Impulse);
                            //Debug.Log("Added force to dead player.");
                        }
                    } // if hitPlayer != null

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * w.impactForce);
                    }

                } // if Physics.Raycast

            } // if w.currentAmmo blablabla....

        } // if input.getkeydown mouseLeft 

    }


}

public class C4Left : ILeftClick
{
    bool isPlanting = false;
    float plantingAmount = 1000f;
    float current = 0f;
    public void OnLeftClick(Weapon weapon)
    {
        C4 c4 = weapon as C4;
        bool isPressing = false;
        float pressTime = 0f;

        if (c4.player.canC4Plant == false)
        {
            return;
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {

                c4.StartCoroutine(PlantingC4_co(c4));
                

            }
            else if(Input.GetMouseButtonUp(0))
            {
                isPressing = false;
                c4.StopCoroutine(PlantingC4_co(c4));

            }
            

        }  // if (canC4Plant = false) -- else
        
    } // OnLeftClick

    IEnumerator PlantingC4_co(C4 c4)
    {
        isPlanting = true;

        while (isPlanting)
        {

            PlatingC4(c4);
            yield return new WaitForSeconds(3.5f);
        }

        isPlanting = false;   

        // Planting Amount need to connect to slide ui
        // platingAmount = ;;;; ;; ;; ; ; ; ;

    }

    void PlatingC4(C4 c4)
    {

        c4.SetState(new PlantingState());
        //c4.StartCoroutine(Wait_co());
        Vector3 position = c4.GetComponentInParent<PlayerControl>().transform.position;
        position.y += 0.1f;
        GameObject newC4 = GameObject.Instantiate(c4.w_model, position ,c4.w_model.transform.rotation);
        newC4.GetComponent<Rigidbody>().isKinematic = true;
        newC4.GetComponentInChildren<MeshCollider>().isTrigger = true;
    }                                                       

    IEnumerator Wait_co()
    {
        yield return new WaitForSeconds(3.5f);
    }



}

public class KnifeLeft : ILeftClick
{
    public void OnLeftClick(Weapon weapon)
    {
        // Knife slash code here 
    }


}
