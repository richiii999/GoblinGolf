using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public GameObject D20_Faces;
    private int currentNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
