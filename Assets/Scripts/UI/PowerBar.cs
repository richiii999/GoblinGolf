using UnityEngine;
using UnityEngine.UI;

// PowerBar.cs, connects to the PowerBar UI slider, and makes it slide with the value of lineAim

public class PowerBar : MonoBehaviour{
    public DiceController diceController; // DiceController component of the ball, required to read shot power value
    private Slider powerSlider; // Slider component (found automatically)

    void Start(){
        if (!diceController){
            Debug.LogWarning("diceController not set, attempting search");
            diceController = GameObject.Find("D20_Faces").GetComponent<DiceController>(); 

            if (!diceController) { Debug.LogError("diceController not found!"); }
        }

        powerSlider = GameObject.Find("PowerBar/Canvas/PowerSlider").GetComponent<Slider>();
    }

    void Update() { if (diceController) powerSlider.SetValueWithoutNotify( (diceController.positions[0] - diceController.positions[1]).magnitude / 7.0f); }
}
