using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRightClick
{
    public void OnRightClick(Weapon weapon);
}


public class RightClickNothing : IRightClick
{


    public void OnRightClick(Weapon w)
    {
        return;
    }

}

/// <summary>
/// Set Bust mode
/// </summary>
public class GlockRight : IRightClick
{
    public void OnRightClick(Weapon w)
    {
        if(w.isBurst == false)
            w.isBurst = true;
        else
            w.isBurst = false;
        
    }
}

public class ZoomIn : IRightClick
{

    public float firstScopedFOV = 40f;
    public float secondScopedFOV = 10f;
    private float normalFOV = 60f;

    public void OnRightClick(Weapon weapon)
    {
        AWP awp = weapon as AWP;

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (awp.isScoped < 2)
            {
                awp.StartCoroutine(OnScoped(awp));
            }
            else
            {
                OnUnscoped(awp);
            }

        }

    }

    IEnumerator OnScoped(AWP awp)
    {
        if(awp.isScoped == 0)
        {
            yield return new WaitForSeconds(.01f);
            awp.scopeOverlay.SetActive(true);
            awp.weaponCamera.SetActive(false);
            awp.fpsCam.fieldOfView = firstScopedFOV;
            awp.isScoped += 1;
        }
        else if(awp.isScoped == 1)
        {
            yield return new WaitForSeconds(.01f);
            awp.fpsCam.fieldOfView = secondScopedFOV;
            awp.isScoped += 1;
        }
        else
        {
            awp.isScoped = 2;
        }


    }
    void OnUnscoped(AWP awp)
    {
        awp.scopeOverlay.SetActive(false);
        awp.weaponCamera.SetActive(true);
        awp.fpsCam.fieldOfView = normalFOV;
        awp.isScoped = 0;

    }
}



public class KnifeRight : IRightClick
{


    public void OnRightClick(Weapon w)
    {


        if (w.currentState is not IdleState)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {


            RaycastHit hit;

            if (Physics.Raycast(w.fpsCam.transform.position, w.fpsCam.transform.forward, out hit, w.range))
            {

                Dummy hitPlayer = hit.transform.GetComponentInParent<Dummy>();
                int calcDamage = w.damage;
                //Debug.DrawLine(w.fpsCam.transform.position, hit.point, Color.green, 5f);



                if (hitPlayer != null)
                {
                    w.SetState(new KnifeStabHitState());
                    Vector3 rayDirection = (hit.point - w.transform.position).normalized;
                    Vector3 targetForward = hitPlayer.transform.forward;


                    float dotProduct = Vector3.Dot(rayDirection, targetForward);

                    // if from behind
                    if (dotProduct > 0)
                    {
                        calcDamage = 9999;
                    }
                    else
                    {
                        calcDamage = w.damage + 20;
                    }



                    //hitPlayer.TakeDamage(calcDamage);
                    hitPlayer.TakeDamage(calcDamage);
                    if (hitPlayer.hp <= 0)
                    {
                        hitPlayer.player.gameObject.TryGetComponent<PlayerControl>(out PlayerControl _player);
                        _player.money += w.reward;
                    }

                    Debug.Log(calcDamage);

                } // if hitPlayer != null
                




            } // if Physics.Raycast
            else
            {
                Debug.Log("Stab Miss");
                w.SetState(new KnifeStabMissState());
            }



        } // if input.getkeydown mouseLeft 

    }

}

