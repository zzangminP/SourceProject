using UnityEngine;

public class SmokeGEParticle : MonoBehaviour
{
    public float delay = 50f;
    private float countdown;
    // Start is called before the first frame update
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
