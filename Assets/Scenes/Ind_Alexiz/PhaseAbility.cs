using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseAbility : MonoBehaviour, IOpacity
{
    private Rigidbody rb;
    public float stopThreshold;
    private MeshCollider sc;
    private SphereCollider sphereCollider;
    private bool phaseOn;
    private string Tag;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LowerOpacityByTag("Phase Object");
        sc = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        phaseOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BallStopped());
    }
    void OnTriggerEnter(Collider other)
    {
        if (phaseOn)
        {
            if (other.gameObject.CompareTag("Phase Object"))
            {
                other.enabled = false;
                StartCoroutine(ColliderBackOn(other));
            }
        }
    }

    private IEnumerator ColliderBackOn(Collider other)
    {
        yield return new WaitForSeconds(5.0f);
        other.enabled = true;
    }

    private IEnumerator BallStopped()
    {
        if (phaseOn){
            yield return new WaitForSeconds(5.0f);
            if (rb.linearVelocity.magnitude < stopThreshold)
            {
                phaseOn = false;
                IncreaseOpacityByTag("Phase Object");
                sphereCollider.isTrigger = false;
                yield return new WaitForSeconds(2.0f);
                sphereCollider.isTrigger = true;
            }
        }
    }

    public void LowerOpacityByTag(string str)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(str);

        foreach(GameObject obj in taggedObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.material != null)
                {
                    Debug.Log("In Opacity");
                    Color currentColor = renderer.material.color;
                    renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
                }
        }
    }

    public void IncreaseOpacityByTag(string str)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(str);

        foreach(GameObject obj in taggedObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.material != null)
                {
                    Debug.Log("In Opacity");
                    Color currentColor = renderer.material.color;
                    renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
                }
        }
    }
}
