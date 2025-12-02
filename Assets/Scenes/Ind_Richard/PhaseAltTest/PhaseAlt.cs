using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseAlt : MonoBehaviour{
    public bool enableKeys = false; // Enable key controls for the ability
    public string phaseTag = "Phase Object"; // Put the tag to phase here (ex. "Phase Object" tag)

    private GameObject[] phaseObjects;

    void Start(){
        phaseObjects = GameObject.FindGameObjectsWithTag(phaseTag); // Get all phase objects in the scene
        PhaseOff(); // Phase objects are solid to start
    }

    void Update(){
        if (enableKeys){
            if (Input.GetKeyDown("e")) PhaseOn();
            if (Input.GetKeyDown("q")) PhaseOff();
        }
    }

    public void PhaseOn(){ // Handles that phase on aspect of the ability
        foreach(GameObject PO in phaseObjects){
            PO.GetComponent<Collider>().isTrigger = true; // Disable collision 
            
            Material M = PO.GetComponent<Renderer>().material; // Make transparent
            M.color = new Color( M.color.r, M.color.g, M.color.b, 0.5f ); // r,g,b,a
        }
    }

    public void PhaseOff(){ // Handles that phase off aspect of the ability
        foreach(GameObject PO in phaseObjects){
            PO.GetComponent<Collider>().isTrigger = false; // Enable collision 
            
            Material M = PO.GetComponent<Renderer>().material; // Make Opaque
            M.color = new Color( M.color.r, M.color.g, M.color.b, 1.0f ); // r,g,b,a
        }
    }
}
