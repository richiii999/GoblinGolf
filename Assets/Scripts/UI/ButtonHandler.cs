using UnityEngine;
using UnityEngine.SceneManagement;

// ButtonHandler.cs, provides functions for the Main Menu buttons

public class ButtonHandler : MonoBehaviour{
    public string level = "MainMenu"; // Default to Main Menu scene

    public void PlayGame(){ SceneManager.LoadScene(level); } // Loads the specified level
    public void QuitGame(){ Application.Quit(); } // Quit
    public void DisplayMenu(){ SceneManager.LoadScene("MainMenu"); } // Takes you back to the main menu
}
