using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("PhaseAbilityTest");
    }

    public void QuitGame()
    {
        Debug.Log("Quit was pressed");
        Application.Quit();
    }
}
