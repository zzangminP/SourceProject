using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            (w.currentState is not ReloadingState))
        {
            w.currentAmmo -= 1;
            w.Ammo_ui.text = $"{w.currentAmmo} || {w.maxAmmo}";

            RaycastHit hit;
            int layerMask = -1 - (1 << LayerMask.NameToLayer("Player"));

            if (Physics.Raycast(w.fpsCam.transform.position, w.fpsCam.transform.forward, out hit, w.range, layerMask))
            {
                Debug.Log(hit.collider.gameObject.layer);

                PlayerControl hitPlayer = hit.transform.GetComponentInParent<PlayerControl>();
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

public class GELeft : ILeftClick
{
    public float throwForce = 10f;
    public GameObject grenadePrefab;
    public GameObject viewModel;





    public void OnLeftClick(Weapon w)
    {


        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade(w);

        }

    }
    public void ThrowGrenade(Weapon w)
    {

        GameObject grenade = Object.Instantiate(grenadePrefab, viewModel.transform.position + viewModel.transform.forward, viewModel.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(viewModel.transform.forward * throwForce, ForceMode.VelocityChange);


    }
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
                w.animator_w.SetTrigger("XMSetIdle");
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
                        PlayerControl hitPlayer = hit.transform.GetComponentInParent<PlayerControl>();
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
                    PlayerControl hitPlayer = hit.transform.GetComponentInParent<PlayerControl>();
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
    float plantingTime = 3.0f; 
    Coroutine plantingCoroutine;

    public void OnLeftClick(Weapon weapon)
    {
        C4 c4 = weapon as C4;

        if (c4.player.canC4Plant == false) // 플레이어가 C4를 설치할 수 있는지 확인
        {
            return;
        }

        // 마우스 왼쪽 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(0))
        {
            if (plantingCoroutine == null) // 이미 설치 중이 아닌 경우만 시작
            {
                c4.SetState(new PlantingState());
                plantingCoroutine = c4.StartCoroutine(PlantingC4(c4));
                
            }
        }

        // 마우스 왼쪽 버튼을 뗐을 때
        if (Input.GetMouseButtonUp(0))
        {
            if (plantingCoroutine != null) // 설치가 진행 중인 경우 취소
            {
                c4.StopCoroutine(plantingCoroutine);
                plantingCoroutine = null;
                isPlanting = false;
                Debug.Log("C4 설치 취소됨");
                c4.SetState(new IdleState());
            }
        }
    }

    IEnumerator PlantingC4(C4 c4)
    {
        isPlanting = true;
        Debug.Log("C4 설치 중...");

        // 3초 동안 기다림 (설치 시간)
        yield return new WaitForSeconds(plantingTime);

        if (isPlanting) // 여전히 설치 중이면 C4 설치 완료
        {
            PlaceC4(c4);
            plantingCoroutine = null;
            isPlanting = false;

            // 40초 후에 폭발
            c4.StartCoroutine(ExplodeC4(c4, 40.0f));
        }
    }

    void PlaceC4(C4 c4)
    {
        // C4 설치 애니메이션 재생


        // C4를 플레이어 위치에 설치
        Vector3 position = c4.player.transform.position + new Vector3(0, 0.1f, 0);
        GameObject plantedC4 = GameObject.Instantiate(c4.w_model, position, c4.w_model.transform.rotation);
        plantedC4.GetComponent<Rigidbody>().isKinematic = true;
        plantedC4.GetComponentInChildren<MeshCollider>().isTrigger = true;

        Debug.Log("C4가 설치되었습니다.");
    }

    IEnumerator ExplodeC4(C4 c4, float delay)
    {

        yield return new WaitForSeconds(delay);
        Debug.Log(delay);
        Debug.Log("boom");

    }
}


public class AWPLeft : ILeftClick
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
                    PlayerControl hitPlayer = hit.transform.GetComponentInParent<PlayerControl>();
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

    } // OnLeftClick
}

public class KnifeLeft : ILeftClick
{
    bool isFiring = false;
    public void OnLeftClick(Weapon w)
    {


        if (w.currentState is KnifeStabHitState ||
            w.currentState is KnifeStabMissState)
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
        w.SetState(new KnifeSlashState());
        w.Ammo_ui.text = $" ";

        RaycastHit hit;

        if (Physics.Raycast(w.fpsCam.transform.position, w.fpsCam.transform.forward, out hit, w.range))
        {
            //Debug.Log(hit.collider.gameObject.layer);
            Debug.DrawLine(w.fpsCam.transform.position, hit.point, Color.green, 5f);
            PlayerControl hitPlayer = hit.transform.GetComponentInParent<PlayerControl>();
            int calcDamage = w.damage;



            if (hitPlayer != null)
            {
                Vector3 rayDirection = (hit.point - w.transform.position).normalized;
                Vector3 targetForward = hitPlayer.transform.forward;

                // 내적 계산
                float dotProduct = Vector3.Dot(rayDirection, targetForward);
                if (dotProduct > 0)
                {
                    calcDamage = w.damage + 20;


                }


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
    }


}
