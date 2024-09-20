using UnityEngine;

public class SmokeGrenade : Weapon
{

    public SmokeGrenade()
    {
        damage      = 1;
        currentAmmo = 1;
        maxAmmo     = 1;
        range       = 100f;
        cost        = 300;
        reward      = 300;
        impactForce = 0;
        fireRate    = 0;
        isAuto      = false;

        type = Type.SMOKE;

        leftClick = new GELeft();
        rightClick = new RightClickNothing();
        reloadClick = new RClickNothing();
        wasdMove = new WASDMove();
    }


    public float delay = 3f;


    public GameObject smokeEffect;
    private float countdown;
    private bool hasExploded = false;
    private bool isGround = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGround = true;
        
    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded && isGround)
        {
            Explode();
            hasExploded = true;
        }
        Debug.Log("SmokeGE is on the ground? : " + isGround);
    }

    void Explode()
    {
       
        Instantiate(smokeEffect, transform.position, transform.rotation);

        // ¼ö·ùÅº ¿ÀºêÁ§Æ® ÆÄ±«
        isGround = false;
        Destroy(gameObject);
    }

}
