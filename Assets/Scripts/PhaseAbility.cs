using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseAbility : MonoBehaviour
{
    private Rigidbody rb;
    public float stopThreshold;
    private SphereCollider sphereCollider;
    private bool phaseOn, routOn;
    private GameObject[] taggedObjects;
    private List<Material> oldMaterials;
    private Material newMaterial;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldMaterials = new List<Material>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        newMaterial = GameObject.FindGameObjectWithTag("Wall").GetComponent<Renderer>().material;
        phaseOn = true;
        routOn = true;
        PhaseOnByTag("Phase Object");
    }

    // Update is called once per frame to detect if ball has stopped
    void Update()
    {
        if (routOn)
        {
            StartCoroutine(BallStopped());
        }
    }


    // Checks if ball has stopped to reenable collision and turn off phase
    private IEnumerator BallStopped()
    {
        routOn = false;
        if (phaseOn){
            yield return new WaitForSeconds(5.0f);
            if (rb.linearVelocity.magnitude < stopThreshold)
            {
                phaseOn = false;
                PhaseOffByTag("Phase Object");
                sphereCollider.isTrigger = false;
                yield return new WaitForSeconds(2.0f);
                sphereCollider.isTrigger = true;
            }
        }
        routOn = true;
    }

    //Handles that phase on aspect of the ability
    public void PhaseOnByTag(string str)
    {
        taggedObjects = GameObject.FindGameObjectsWithTag(str);
        
        foreach(GameObject obj in taggedObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            oldMaterials.Add(renderer.material);
            renderer.material = newMaterial;
            Collider collid = obj.GetComponent<Collider>();
            collid.enabled = false;      
        }
    }

    //Handles that phase off aspect of the ability
    public void PhaseOffByTag(string str)
    {
        int i = 0;
        foreach(GameObject obj in taggedObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = oldMaterials[i];
            Collider collid = obj.GetComponent<Collider>();
            collid.enabled = true;
            i++;
        }
    }
}
