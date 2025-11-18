using UnityEngine;
using UnityEngine.InputSystem;

public class HitBall : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 forceDirection;
    public Vector3 force;
    public SphereCollider targetCollider;

    // This function will push the ball
    public void Push(InputAction.CallbackContext context)
    {
        // If the left mouse button is hit enter the conditional
        if (context.performed)
        {
            PerformRaycast();
        }
    }

    // This function performs a raycast to check whether or not the actual ball is being hit
    private void PerformRaycast()
    {
        // This variable reads the ray from where the mouse was clicked 
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // The conditional checks if the ray hit a collider and if it did we run the code
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Only adds force if the collider is the actual sphere collider
            if(hit.collider == targetCollider)
            {
                rb = GetComponent<Rigidbody>();
                
                forceDirection = Camera.main.transform.forward;
                forceDirection.y = 0;
                forceDirection.Normalize();

                Debug.Log("Hit: " + hit.collider.name);
                rb.AddForce(3.0f * forceDirection, ForceMode.VelocityChange);    
            }
        }
    }
}
