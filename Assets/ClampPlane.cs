using UnityEngine;

public class ClampPlane : MonoBehaviour
{
    public Transform dieTransform;
    private Vector3 diePlaneClampedRotation = Vector3.zero;    
    void Start()
    {
        if (dieTransform == null && transform.parent != null)
        {
            dieTransform = transform.parent;
        }
    }

    void Update()
    {
        if (dieTransform != null)
        {
            Vector3 targetPos = dieTransform.position;
            // Keep original Y
            transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);

            transform.rotation = Quaternion.Euler(diePlaneClampedRotation);
        }
    }
}
