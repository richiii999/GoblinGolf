using UnityEngine;
using UnityEngine.InputSystem;

public class DiceController : MonoBehaviour
{
    [SerializeField] private float shotPower;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LayerMask diePlaneMask;
    private LayerMask selectionMask;
    [SerializeField] private new GameObject gameObject;
    [SerializeField] private bool stopped = false;
    [SerializeField] private bool abilityActivated = false;
    private float stopVelocity = 0.01f;
    

    private InputAction aimAction, pointerAction;
    public Vector3[] positions; // Positions is saved between shots (for use in PowerSlider)

    public Vector3 lastShotPos; // Save the last shot's position (to allow reset)
    public Vector3 currShotPos;
    public Quaternion lastShotRot;
    public Quaternion currShotRot;

    private bool isIdle;
    private bool isAiming;
    private Rigidbody rb;
    private bool stopHandled = false;

    public ScoreManager ScoreHandler;
    public DiceNumber DiceHandler;
    private int currentNumber;
    private int stroke;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        int diePlaneLayer = LayerMask.NameToLayer("DiePlane");
        diePlaneMask = 1 << diePlaneLayer;
        int everythingExceptDiePlane = ~(1 << diePlaneLayer);
        selectionMask = everythingExceptDiePlane;
        
        lineRenderer.enabled = false;
        isAiming = false;
        isIdle = true;
    }

    private void Start()
    {
        rb.sleepThreshold = 1f;
        aimAction = playerInput.actions["Aim"];
        pointerAction = playerInput.actions["PointerPosition"];

        // When the Aim input is pressed call OnAimPressed
        // When canceled call the OnAimReleased method
        aimAction.performed += OnAimPressed;
        aimAction.canceled += OnAimReleased;

        // Check if linked components are set or not
        if (!ScoreHandler){
            Debug.LogWarning("ScoreHandler not set, attempting search");
            ScoreHandler = GameObject.Find("UI/UI_Canvas/Score").GetComponent<ScoreManager>(); 

            if (!ScoreHandler) { Debug.LogError("ScoreHandler not found!"); }
        }

        if (!DiceHandler){
            Debug.LogWarning("DiceHandler not set, attempting search");
            DiceHandler = GameObject.Find("UI/UI_Canvas/D20/Number").GetComponent<DiceNumber>(); 

            if (!DiceHandler) { Debug.LogError("DiceHandler not found!"); }
        }

        Stop();
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
        ProcessAim();
    }

    private void FixedUpdate()
    {
        if (rb.IsSleeping() && !stopHandled)
        {
            stopHandled = true;
            Stop();
        }
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
        if (Physics.Raycast(ray, out hit, 100f, selectionMask))
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
        stopHandled = false;
        rb.WakeUp();

        isAiming = false; // Exit aiming state
        lineRenderer.enabled = false; // Hide the line
        stroke++; // Update the score/stroke

        // Makes shot horizontal by flattening the world point to match the ball's Y position
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        
        // Compute direction from ball to aim point
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        
        // Force scaled by distance
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        // apply the force to shoot the ball (negative direction makes it shoot opposite of line)
        rb.AddForce(shotPower * strength * -direction);
        stopped = true;
        abilityActivated = true;

        // Ball is no longer idle
        isIdle = false;

        ScoreHandler.UpdateScore(stroke);
    }

    private void DrawLine(Vector3 worldPoint)
    {
        // Set clamped point so the line doesn't hit the wall/floor/etc
        //Vector3 clampedPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        // Draw the line from ball to cursor target
        positions[0] = transform.position;
        positions[1] = worldPoint; // Start the ball's position and the end is the aim point

        lineRenderer.SetPositions(positions); // Update LineRenderer positions
        lineRenderer.enabled = true; // Ensure line is visible
    }

    public void Stop()
    {
        Debug.Log("Stopped");
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isIdle = true;
        if (stopped)
        {
            stopped = false;
            gameObject.GetComponent<Float>().AbilityOff();
            gameObject.GetComponent<PhaseAlt>().Phase(false);
            gameObject.GetComponent<ResizeBall>().returnToBase();
        }
        readDie();

        // Save the shot's position and rotation (to allow reset)
        lastShotPos = currShotPos;
        lastShotRot = currShotRot;
        currShotPos = transform.position;
        currShotRot = transform.rotation;
    }

    public void readDie()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 2f, selectionMask))
        {
            string face = hit.collider.gameObject.name;

            if (face.StartsWith("Face"))
            {
                currentNumber = int.Parse(face.Substring(4));
                
                //To determine Ability Use
                if(abilityActivated)
                {
                    if (currentNumber <= 4)
                    {
                        gameObject.GetComponent<PhaseAlt>().SetPhaseFlag("PhaseObjectBad");
                        gameObject.GetComponent<PhaseAlt>().Phase(true);
                    }
                    if (currentNumber >= 5 && currentNumber <= 8)
                    {
                        gameObject.GetComponent<ResizeBall>().Grow();
                    }
                    if (currentNumber >= 9 && currentNumber <= 12)
                    {
                        gameObject.GetComponent<Float>().AbilityOn();
                    }
                    if (currentNumber >= 13 && currentNumber <= 16)
                    {
                        gameObject.GetComponent<ResizeBall>().Shrink();
                    }
                    if (currentNumber >= 17 && currentNumber <= 20)
                    {
                        gameObject.GetComponent<PhaseAlt>().SetPhaseFlag("Phase Object");
                        gameObject.GetComponent<PhaseAlt>().Phase(true);
                    }
                    abilityActivated = false;
                }
                //Debug.Log(currentNumber);
                DiceHandler.UpdateDice(currentNumber);
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
        if (Physics.Raycast(ray, out hit, 100f, diePlaneMask))
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