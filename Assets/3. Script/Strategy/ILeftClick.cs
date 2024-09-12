using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeftClick
{
    public void OnLeftClick(Weapon weapon);
    //void Fire(Weapon weapon);
}

public class AK47Left : ILeftClick
{
    bool isFiring = false;

    public void OnLeftClick(Weapon w)
    {

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

        // 연사 모드로 발사
        while (isFiring && w.currentAmmo > 0)
        {
            OnAttack(w);

            // 발사 속도 조절: fireRate를 기준으로 대기
            yield return new WaitForSeconds(1f / w.fireRate);
        }

        isFiring = false; // 발사가 끝난 후 상태 초기화
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

            if (Physics.Raycast(w.fpsCam.transform.position, w.fpsCam.transform.forward, out hit, w.range))
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
    public void OnLeftClick(Weapon weapon)
    {
        
    }
}

public class PistolLeft : ILeftClick
{
    public void OnLeftClick(Weapon weapon)
    {

    }


}

public class KnifeLeft : ILeftClick
{
    public void OnLeftClick(Weapon weapon)
    {
        // Knife slash code here 
    }


}
