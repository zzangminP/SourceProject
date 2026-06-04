using UnityEngine;

    public class DestroyMe : MonoBehaviour
    {
        public float timeToDestroy;

        private void Start() => Invoke("DestroyMeObj", timeToDestroy);


        private void DestroyMeObj() => Destroy(this.gameObject);

    }
