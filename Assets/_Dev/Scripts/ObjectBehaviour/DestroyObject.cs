using UnityEngine;


namespace _Dev.Scripts.ObjectBehaviour
{
    public class DestroyObject : MonoBehaviour
    {
        private float fadeDuration = 10f;
        private float elapsedTime = 0f;
        private Renderer objectRenderer;
        private Color originalColor;
        private bool startFade = false;

        void Start()
        {
            objectRenderer = GetComponent<Renderer>();

            originalColor = objectRenderer.material.color;
        }

        void Update()
        {
            if (startFade)
            {
                elapsedTime += Time.deltaTime;

                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

                Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                objectRenderer.material.color = fadedColor;

                if (elapsedTime >= fadeDuration)
                {
                    Destroy(gameObject);
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Trigger"))
            {
                startFade = true;
            }
        }
    }
}