using UnityEngine;
using UnityEngine.InputSystem;

public class LineForce : MonoBehaviour
{
    [SerializeField] private float shotPower;
    [SerializeField] private float stopVelocity = .05f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PlayerInput playerInput;

    private InputAction aimAction, pointerAction;

    private bool isIdle;
    private bool isAiming;
    private Rigidbody rb;

    public ScoreManager ScoreHandler;
    public GameObject D20_Faces;
    private int currentNumber;
    private int stroke;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.enabled = false;
        isAiming = false;
        isIdle = true;
    }

    private void Start()
    {
        aimAction = playerInput.actions["Aim"];
        pointerAction = playerInput.actions["PointerPosition"];

        // When the Aim input is pressed call OnAimPressed
        // When canceled call the OnAimReleased method
        aimAction.performed += OnAimPressed;
        aimAction.canceled += OnAimReleased;
    }

    private void OnDestroy()
    {
        if (aimAction != null)
        {
            aimAction.performed -= OnAimPressed;
            aimAction.canceled -= OnAimReleased;
        }
    }

    private void Update()
    {
        // If the velocity is a small number stop the Die from moving
        if (rb.linearVelocity.magnitude < stopVelocity)
        {
            Stop();
        }
        else
        {
            isIdle = false;
        }

        ProcessAim();
    }

    private void OnAimPressed(InputAction.CallbackContext context)
    {
        // Left mouse button down
        if (isIdle && IsPointerOverBall())
        {
            // Enable the aiming state
            isAiming = true;
            // show the lineRenderer
            lineRenderer.enabled = true;
        }
    }

    private bool IsPointerOverBall()
    {
        if (pointerAction == null) return false;
        Vector2 pointerScreenPos = pointerAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(pointerScreenPos);

        RaycastHit hit;
        // Raycast using the pointer position
        if (Physics.Raycast(ray, out hit, 100f))
        {
            // Check if the ray hit this object's collider
            return hit.collider != null && hit.collider.gameObject == gameObject;
        }
        return false;
    }

    private void OnAimReleased(InputAction.CallbackContext context)
    {
        // Left mouse button up
        // Only call if we are currently aiming and the ball is idle
        if (isAiming && isIdle)
        {
            // Get the point in the world the player is currently aiming at
            Vector3? worldPoint = CastPointerRay();
            if (worldPoint.HasValue)
            {
                Shoot(worldPoint.Value);
            }
        }
    }

    private void ProcessAim()
    {
        // Only update line if aiming and idle
        if (!isAiming || !isIdle) return;

        // Get the world point under the cursor
        Vector3? worldPoint = CastPointerRay();
        if (worldPoint.HasValue)
        {
            DrawLine(worldPoint.Value);
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false; // Exit aiming state
        lineRenderer.enabled = false; // Hide the line
        stroke++; // Update the score/stroke

        // Makes shot horizontal by flattening the world point to amtch the ball's Y position
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        
        // Compute direction from ball to aim point
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        
        // Force scaled by distance
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        // apply the force to shoot the ball (negative direction makes it shoot opposite of line)
        rb.AddForce(shotPower * strength * -direction);

        // Ball is no longer idle
        isIdle = false;

        ScoreHandler.UpdateScore(stroke);
    }

    private void DrawLine(Vector3 worldPoint)
    {
        // Draw the line from ball to cursor target
        Vector3[] positions = { transform.position, worldPoint }; // Start the ball's position and the end is the aim point
        lineRenderer.SetPositions(positions); // Update LineRenderer positions
        lineRenderer.enabled = true; // Ensure line is visible
    }

    private void Stop()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isIdle = true;
        readDie();
    }
    public void readDie()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 2f))
        {
            string face = hit.collider.gameObject.name;

            if (face.StartsWith("Face"))
            {
                currentNumber = int.Parse(face.Substring(4));
                Debug.Log(currentNumber);
            }

        }
    }
    // Casts a ray from the camera through the pointer position (mouse cursor)
    // to find a point in the game world, typically on a collider
    // returns the hit point if successful, or null if nothing is hit
    private Vector3? CastPointerRay()
    {
        if (pointerAction == null) return null;

        // Read current cursor position oon the screen.
        Vector2 pointerScreenPos = pointerAction.ReadValue<Vector2>();

        // create a ray from the camera through the cursor's position
        Ray ray = Camera.main.ScreenPointToRay(pointerScreenPos);
        
        RaycastHit hit;
        // Perform a raycast in the direction of the ray
        if (Physics.Raycast(ray, out hit, 100f))
        {
            // If collider is hit return the point in world space where it hit
            return hit.point;
        }
        else
        {
            return null;
        }
    }
}