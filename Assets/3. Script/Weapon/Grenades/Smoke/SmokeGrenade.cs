using UnityEngine;

public class SmokeGrenade : MonoBehaviour
{
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
