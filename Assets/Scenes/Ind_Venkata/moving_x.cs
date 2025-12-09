using UnityEngine;
using System.Collections; // Needed for IEnumerator and Coroutines

public class Moving_x : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 5f; // How far up from the starting point to move
    public float moveSpeed = 1.5f;  // Speed of movement
    public float waitTime = 5f;     // Time to wait at each end

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        // Store the initial global starting position
        startPosition = transform.position;

        // Calculate the target end position purely along the global Y-axis
        endPosition = new Vector3(startPosition.x + moveDistance, startPosition.y, startPosition.z);

        // Start the infinite movement loop sequence
        StartCoroutine(MoveLoop());
    }

    /// <summary>
    /// The main Coroutine that manages the sequence of movement and waiting.
    /// </summary>
    IEnumerator MoveLoop()
    {
        // This 'while (true)' loop runs forever once started
        while (true)
        {
            // 1. Move from start to end position
            yield return StartCoroutine(MoveToPosition(endPosition));

            // 2. Wait at the top for the specified time
            Debug.Log("Reached top. Waiting for " + waitTime + " seconds.");
            yield return new WaitForSeconds(waitTime);

            // 3. Move from end back to start position
            yield return StartCoroutine(MoveToPosition(startPosition));

            // 4. Wait at the bottom for the specified time
            Debug.Log("Reached bottom. Waiting for " + waitTime + " seconds.");
            yield return new WaitForSeconds(waitTime);
        }
    }

    /// <summary>
    /// A helper Coroutine to smoothly move the platform to a target Vector3.
    /// </summary>
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        // Continue moving as long as we are not at the exact target position
        while (transform.position != targetPosition)
        {
            // Use MoveTowards for consistent speed
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Yield null tells Unity to pause this Coroutine here and resume it next frame.
            yield return null;
        }
    }
}
