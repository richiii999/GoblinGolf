using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
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
