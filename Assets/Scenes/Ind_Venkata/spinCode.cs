using UnityEngine;

public class spinCode : MonoBehaviour
{
    // Adjust the speed and axis in the Inspector
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f);

    // Update is called once per frame
    void Update()
    {
        // Rotate the object every frame
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}