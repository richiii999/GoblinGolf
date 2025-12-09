using UnityEngine;
using UnityEngine.UI;

// Timer.cs, modified from https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
// Connects to a text UI component and displays the time in mm:ss since the scene started

public class Timer : MonoBehaviour{
    public Text timerText; // The text component to connect to

    void Update() { DisplayTime(); }

    void DisplayTime(){
        float timeToDisplay = Time.timeSinceLevelLoad;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (timerText) timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}