using UnityEngine;
using System.Collections;
using System;

public class Float : MonoBehaviour
{
    [SerializeField] private Rigidbody component;
    [SerializeField] private bool abilityOn;
    [SerializeField] private bool inRoute = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Update()
    {
        if(abilityOn){
            if (component.linearVelocity.magnitude > 0.5f)
            {
                StartCoroutine(BallFloat());
            }
        }
    }
    private IEnumerator BallFloat()
    {
        if(!inRoute)
        {
            inRoute = true;
            yield return new WaitForSeconds(0.1f);
            component.useGravity = false;
            component.AddForce(Vector3.up * 1f, ForceMode.Force);

            StartCoroutine(BallNoFloat());
            inRoute = false;
        }
        
    }
    private IEnumerator BallNoFloat()
    {
        yield return new WaitForSeconds(2f);
        component.useGravity = true;
    }
    public void AbilityOn()
    {
        abilityOn = true;
    }

    public void AbilityOff()
    {
        abilityOn = false;
    }
}
