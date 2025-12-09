using System.Collections;
using UnityEngine;

public class BlockSpinnerIntervals : MonoBehaviour
{
    // Adjust these public variables in the Unity Inspector
    public Vector3 rotationAmount = new Vector3(0, 50, 0); // Speed/Axis of rotation
    public float spinDuration = 3f; // How long it spins before stopping
    public float stopDuration = 2f; // How long it stops before spinning again

    void Start()
    {
        // Start the repeating routine when the game begins
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        while (true) // This makes the routine repeat indefinitely
        {
            // --- Spin Phase ---
            // The Update() method is no longer used for rotation. Instead, we use a loop here.
            float timer = 0;
            while (timer < spinDuration)
            {
                transform.Rotate(rotationAmount * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null; // Wait until the next frame
            }

            // --- Stop Phase ---
            // Pause the Coroutine for the specified stop duration
            yield return new WaitForSeconds(stopDuration);

            // The loop then continues back to the spin phase
        }
    }
}
