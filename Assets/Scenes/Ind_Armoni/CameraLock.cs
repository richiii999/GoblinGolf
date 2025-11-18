using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class CameraLock : MonoBehaviour
{
    public CinemachineInputAxisController RotationInputProvider;
   
    
    private void Start()
    {
        
    }
    void Update()
    {
        bool allowOrbit = Input.GetMouseButton(1); // Right mouse to rotate

        if (allowOrbit)
        {
           RotationInputProvider.enabled = true;
        }
        else
        {
           RotationInputProvider.enabled= false;
        }
    }
}
