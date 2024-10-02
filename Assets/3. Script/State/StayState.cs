using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayState : BaseState
{
    
    private float losePlayerTimer;
    private float shotTimer;

    public override void Enter()
    {

    }

    public override void Execute()
    {
        if (dummy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            
            shotTimer += Time.deltaTime;
            dummy.transform.LookAt(dummy.Player.transform);

            if (shotTimer > dummy.fireRate)
            {
                Shoot();

            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new StayState());

            }
        }

    }

    public override void Exit()
    {

    }


    public void Shoot()
    {
        //Transform gunbarrel = dummy.gunBarrel;

        RaycastHit hit;
        if (Physics.Raycast(dummy.viewPoint.transform.position, dummy.viewPoint.transform.forward, out hit, (int)dummy.sightDistance))
        {
            //if(hit.transform.parent.TryGetComponent<Dummy>(out Dummy temp))
            //{
            //    return;
            //}

            PlayerControl hitPlayer = hit.transform.GetComponentInParent<PlayerControl>();
            int calcDamage = dummy.damage;

            if (hitPlayer != null)
            {
                float distance = Vector3.Distance(dummy.viewPoint.transform.position, hit.point);
                float distanceFactor = 1 - (distance / dummy.sightDistance);
                distanceFactor = Mathf.Clamp(distanceFactor, 0.2f, 1f);

                switch (hit.collider.gameObject.layer)
                {
                    case 9:
                        calcDamage = dummy.damage * 4;
                        break;
                    case 11:
                        calcDamage = (int)(dummy.damage * 1.25);
                        break;
                    case 12:
                        calcDamage = (int)(dummy.damage * 0.75);
                        break;
                    default:
                        calcDamage = dummy.damage;
                        break;

                }
                calcDamage = (int)(calcDamage * distanceFactor);
                hitPlayer.TakeDamage(calcDamage);

            }

        }

        shotTimer = 0;
    }

}
