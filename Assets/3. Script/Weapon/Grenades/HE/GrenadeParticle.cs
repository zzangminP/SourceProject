using UnityEngine;

public class GrenadeParticle : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 3f;
    private float countdown;

    void Start()
    {
        countdown = delay;
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
            Destroy(gameObject);
 
    }
}
