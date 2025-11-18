using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
public class CameraLock : MonoBehaviour
{
    public CinemachineInputAxisController axisController;

    public string horizontal = "Mouse X";
    public string vertical = "Mouse Y";
    private void Start()
    {

    }
    void Update()
    {
        bool allowOrbit = Input.GetMouseButton(1); // Right mouse to rotate

        //if (allowOrbit)
        //{
        //}
        //else
        //{

        //}
    }
}
