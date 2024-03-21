using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    public float fadeDuration = 10f; // Duration of the fade effect in seconds
    private float elapsedTime = 0f; // Time elapsed since the fade started
    private Renderer renderer; // Reference to the Renderer component
    private Color originalColor; // Original color of the object
    private bool startFade = false; // Flag to start fading when collision occurs

    void Start()
    {
        // Get the Renderer component attached to the object
        renderer = GetComponent<Renderer>();

        // Store the original color of the object
        originalColor = renderer.material.color;
    }

    void Update()
    {
        if (startFade)
        {
            // Update the time elapsed
            elapsedTime += Time.deltaTime;

            // Calculate the current alpha value based on the elapsed time and fade duration
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Create a new color with the same RGB values as the original color, but with the calculated alpha
            Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // Apply the faded color to the object's material
            renderer.material.color = fadedColor;

            // If the elapsed time exceeds the fade duration, destroy the object
            if (elapsedTime >= fadeDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if collision occurs with the colliders of the object
        if (collision.gameObject.CompareTag("Trigger"))
        {
            startFade = true;
        }
    }
}




//using UnityEngine;

//public class DestroyObject : MonoBehaviour
//{
//    public float fadeDuration = 10f; // Duration of the fade effect in seconds
//    private float elapsedTime = 0f; // Time elapsed since the fade started
//    private Renderer renderer; // Reference to the Renderer component
//    private Color originalColor; // Original color of the object

//    void Start()
//    {
//        // Get the Renderer component attached to the object
//        renderer = GetComponent<Renderer>();

//        // Store the original color of the object
//        originalColor = renderer.material.color;
//    }

//    void Update()
//    {
//        // Update the time elapsed
//        elapsedTime += Time.deltaTime;

//        // Calculate the current alpha value based on the elapsed time and fade duration
//        float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

//        // Create a new color with the same RGB values as the original color, but with the calculated alpha
//        Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

//        // Apply the faded color to the object's material
//        renderer.material.color = fadedColor;

//        // If the elapsed time exceeds the fade duration, destroy the object
//        if (elapsedTime >= fadeDuration)
//        {
//            Destroy(gameObject);
//        }
//    }
//}