using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseAbility : MonoBehaviour
{
    private SphereCollider sc;
    private bool phaseOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sc = GetComponent<SphereCollider>();
        phaseOn = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        if (phaseOn)
        {
            if (other.gameObject.CompareTag("Phase Object"))
            {
                Physics.IgnoreCollision(sc, other, true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Phase Object"))
        {
            Physics.IgnoreCollision(sc, other, false);
            phaseOn = false;
        }
    }
}
