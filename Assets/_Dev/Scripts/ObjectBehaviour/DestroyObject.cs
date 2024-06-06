using UnityEngine;


namespace _Dev.Scripts.ObjectBehaviour
{
    public class DestroyObject : MonoBehaviour
    {
        private float destructionDuration = 3f;
        private float elapsedTime = 0f;
        private Renderer objectRenderer;
        private bool startDestroying = false;

        void Start()
        {
            objectRenderer = GetComponent<Renderer>();
        }

        void Update()
        {
            if (startDestroying)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= destructionDuration)
                {
                    Destroy(gameObject);
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Trigger"))
            {
                startDestroying = true;
            }
        }
    }
}