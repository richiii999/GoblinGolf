using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour{
    public string nextHole = "MainMenu"; // What is the next hole? 
    public int waitTime = 5; // How long to wait when hole is complete?

    void Start(){
        if (SceneUtility.GetBuildIndexByScenePath(nextHole) == -1) {
            Debug.Log("Invalid nextHole, using 'MainMenu' instead");
            nextHole = "MainMenu";
        }

        
    }

    public void OnTriggerEnter(Collider other){ if (other.tag == "Golf Ball") StartCoroutine(HoleComplete()); }

    IEnumerator HoleComplete(){
        Debug.Log("Hole!");
        Renderer M = GetComponent<Renderer>();
        M.material.SetColor("_Color", Color.red); // BUG: not working
        

        // Display Strokes on screen
        // asdf
        
        // Wait for a few sec
        yield return new WaitForSeconds(waitTime);

        // Load next scene (or MainMenu if no next hole is set)
        SceneManager.LoadSceneAsync(nextHole);
    }
}
