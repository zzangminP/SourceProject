using Mirror.Examples.AdditiveScenes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
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
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            dummy.transform.LookAt(dummy.player.transform);

            if (shotTimer > dummy.fireRate)
            {
                Shoot();
                
            }
            
            if (moveTimer > Random.Range(3, 7))
            {
                dummy.Agent.SetDestination(dummy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }

            dummy.LastKnowPos = dummy.player.transform.position;


        }
        else
        {

            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                if (stateMachine.isStay)
                {
                    stateMachine.ChangeState(new StayState());
                }
                else
                {
                    stateMachine.ChangeState(new SearchState());
                }
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
        if(Physics.Raycast(dummy.viewPoint.transform.position, dummy.viewPoint.transform.forward, out hit, (int)dummy.sightDistance ))
        {
            if(dummy.isAWP)
            {
                dummy.awp_audio.Play();
            }
            else
            {
                dummy.attack_audio.Play();
            }

            PlayerControl hitPlayer  = hit.transform.GetComponentInParent<PlayerControl>();
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
                if(hitPlayer.armor >= 0)
                {
                    calcDamage = (int)(calcDamage * distanceFactor * 0.5f);
                    hitPlayer.armor -= Random.Range(0, 4);
                }
                else
                {
                    calcDamage = (int)(calcDamage * distanceFactor);
                }

                hitPlayer.TakeDamage(calcDamage);

            }

        }
        
        shotTimer = 0;
    }


}
