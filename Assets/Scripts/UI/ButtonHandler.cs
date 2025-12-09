using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public string level = "MainMenu";
    public void PlayGame()
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Debug.Log("Quit was pressed");
        Application.Quit();
    }

    public void displayMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
