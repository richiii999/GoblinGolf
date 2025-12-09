using UnityEngine;
using UnityEngine.SceneManagement;

// ButtonHandler.cs, provides functions for the Main Menu buttons

public class ButtonHandler : MonoBehaviour{
    public string level = "MainMenu"; // Default to Main Menu scene
    public GameObject mainScreen;
    public GameObject controlScreen;

    public void PlayGame(){ SceneManager.LoadScene(level); } // Loads the specified level
    public void Controls(){ mainScreen.SetActive(false); controlScreen.SetActive(true); } //Move into controls
    public void Back(){ mainScreen.SetActive(true); controlScreen.SetActive(false); } //Move back to screen
    public void QuitGame(){ Application.Quit(); } // Quit
    public void DisplayMenu(){ SceneManager.LoadScene("MainMenu"); } // Takes you back to the main menu
}
