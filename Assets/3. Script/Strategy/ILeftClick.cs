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

    public void OnLeftClick(Weapon w)
    {
        w.SetState(new FiringState());


            RaycastHit hit;

            if (Physics.Raycast(w.fpsCam.transform.position, w.fpsCam.transform.forward, out hit, w.range))
            {
                Debug.Log(hit.collider.gameObject.layer);


                //PlayerControl hitPlayer = hit.transform.GetComponent<PlayerControl>();
                Dummy hitPlayer = hit.transform.GetComponentInParent<Dummy>();
                int calcDamage = 0;
                if (hitPlayer != null)
                {
                    switch (hit.collider.gameObject.layer)
                    {
                        case 9:
                            calcDamage = w.damage * 4;
                            break;
                        //case 10:       ---> default
                        //    break;     ---> default
                        case 11:
                            calcDamage = (int)(w.damage * 1.25);
                            break;
                        case 12:
                            calcDamage = (int)(w.damage * 0.75);
                            break;
                        default:
                            calcDamage = w.damage;
                            break;


                    }

                    hitPlayer.TakeDamage(calcDamage);
                    Debug.Log(calcDamage);

                    if (hitPlayer.hp <= 0)
                    {

                        hitPlayer.GetComponent<Rigidbody>().AddForce(-hit.normal * w.impactForce, ForceMode.Impulse);
                        Debug.Log(-hit.normal);
                        Debug.Log("Added force");
                    }


                }


                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * w.impactForce);

                }

            }




        
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
