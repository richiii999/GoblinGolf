using UnityEngine;

public class pathBlocking : MonoBehaviour
{
    // Adjust these in the Unity Inspector to change movement
    public float speed = 1.0f; // Speed of movement
    public float distance = 1.0f; // Total distance to travel from the start point

    private Vector3 startPosition;

    void Start()
    {
        // Save the initial position of the block when the game starts
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position using Mathf.PingPong
        // Time.time * speed creates a continuously increasing value
        // Mathf.PingPong keeps the value between 0 and the 'distance' value
        float pingPongValue = Mathf.PingPong(Time.time * speed, distance);

        // Calculate the new position along a specific axis (e.g., the X-axis)
        // You can change Vector3.right to Vector3.forward or Vector3.up to move along different axes
        Vector3 newPosition = startPosition + Vector3.up * pingPongValue;

        // Update the block's position
        transform.position = newPosition;
    }
}
