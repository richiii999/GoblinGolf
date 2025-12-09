using UnityEngine;
using System.Collections; // Make sure this namespace is included

public class movingBlockWithPause : MonoBehaviour
{
    // Adjust these in the Unity Inspector
    public float speed = 1f; // Speed of movement (adjusted for the new method)
    public float distance = 5.0f; // Total distance to travel
    public float waitTime = 5.0f; // Time to wait at each end

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        startPosition = transform.position;
        // Define the target end position along a specific axis (e.g., X-axis)
        endPosition = startPosition + Vector3.right * distance;

        // Start the movement loop immediately
        StartCoroutine(MoveRoutine());
    }

    // The Coroutine manages the movement sequence
    IEnumerator MoveRoutine()
    {
        while (true) // This creates an infinite loop that runs alongside your game
        {
            // --- Move from Start to End ---
            yield return StartCoroutine(MoveToPosition(startPosition, endPosition));
            
            // --- Wait at the End ---
            Debug.Log("Reached End, waiting 5 seconds...");
            yield return new WaitForSeconds(waitTime);

            // --- Move from End back to Start ---
            yield return StartCoroutine(MoveToPosition(endPosition, startPosition));
            
            // --- Wait at the Start ---
            Debug.Log("Reached Start, waiting 5 seconds...");
            yield return new WaitForSeconds(waitTime);
        }
    }

    // A helper coroutine to smoothly interpolate the position over time
    IEnumerator MoveToPosition(Vector3 fromPosition, Vector3 toPosition)
    {
        float t = 0f;
        // Calculate the duration based on speed and distance for consistent movement
        float duration = distance / speed; 

        while (t < duration)
        {
            // Increase 't' based on the time passed since the last frame
            t += Time.deltaTime;
            // Calculate the current progress (0 to 1)
            float normalizedT = t / duration; 
            
            
            // Interpolate position smoothly
            transform.position = Vector3.Lerp(fromPosition, toPosition, normalizedT);
            
            // Yield control back to Unity until the next frame is rendered
            yield return null; 
        }
        
        // Ensure the block snaps exactly to the final position when the loop finishes
        transform.position = toPosition;
    }
}