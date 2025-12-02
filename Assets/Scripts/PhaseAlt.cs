using UnityEngine;

public class PhaseAlt : MonoBehaviour{
    public bool enableKeys = false; // Enable key controls for the ability (for testing)
    public string phaseTag = "Phase Object"; // The tag of objects to phase

    // NOTE: In order to work properly, objects with the phaseTag must have a collider with isTrigger, and a transparent material. 
    // 
    // By default, meshes dont work, you must set 'isConvex' -> True
    // By default, URP Materials dont work, you must change the material to something else, then set 'Surface Type' -> Transparent

    // BUG: unphasing when inside of something has wierd physics, need to bring over the fix from Alexis's script

    private GameObject[] phaseObjects;

    void Start(){
        phaseObjects = GameObject.FindGameObjectsWithTag(phaseTag); // Get all phase objects in the scene
        Phase(false); // Phase objects are solid to start
    }

    void Update(){
        if (enableKeys){
            if (Input.GetKeyDown("q")) Phase(true);
            if (Input.GetKeyDown("e")) Phase(false);
        }
    }

    public void Phase(bool onOrOff){ // Disables collision and makes objects transparent
        foreach(GameObject PO in phaseObjects){
            PO.GetComponent<Collider>().isTrigger = onOrOff;
            
            Material M = PO.GetComponent<Renderer>().material;
            M.color = new Color( M.color.r, M.color.g, M.color.b, onOrOff ? 0.5f : 1.0f ); // r,g,b,a
        }
    }
}
