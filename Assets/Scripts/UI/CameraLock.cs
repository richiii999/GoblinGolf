using Unity.Cinemachine;
using UnityEngine;

// CameraLock.cs, makes the camera move with the mouse, only when RMB is held

public class CameraLock : MonoBehaviour{
    public CinemachineInputAxisController RotationInputProvider;
   
    void Update(){ RotationInputProvider.enabled = Input.GetMouseButton(1); } // Right mouse to rotate
}
