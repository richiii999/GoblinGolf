using UnityEngine;
using System.Collections;

public class flipping_code : MonoBehaviour
{
    public float rotationSpeed = 90f; // Degrees per second
    public float rotationDuration = 1f; // Time to complete 90-degree rotation
    public float waitAfterRotation = 2f; // Time to wait after rotating
    public float waitBeforeRepeat = 2f; // Time to wait before repeating the cycle

    private Quaternion originalRotation;
    private bool isRotating = false;

    void Start()
    {
        originalRotation = transform.rotation;
        StartCoroutine(RotateCycle());
    }

    IEnumerator RotateCycle()
    {
        while (true) // Loop indefinitely
        {
            // Rotate 90 degrees
            yield return StartCoroutine(RotateToTarget(originalRotation * Quaternion.Euler(90, 0, 0)));

            // Wait after rotation
            yield return new WaitForSeconds(waitAfterRotation);

            // Return to original rotation
            yield return StartCoroutine(RotateToTarget(originalRotation));

            // Wait before repeating
            yield return new WaitForSeconds(waitBeforeRepeat);
        }
    }

    IEnumerator RotateToTarget(Quaternion targetRotation)
    {
        isRotating = true;
        float timeElapsed = 0f;
        Quaternion startRotation = transform.rotation;

        while (timeElapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / rotationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation; // Ensure exact target rotation
        isRotating = false;
    }
}
