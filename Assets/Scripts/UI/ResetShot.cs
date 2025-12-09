using UnityEngine;

public class ResetShot : MonoBehaviour{

    public GameObject ball; // The golf ball "D20_Faces" object
    public DiceController diceController; // The ball's DiceController script
    
    void Start(){
        if (!ball){
            Debug.LogWarning("ball not set, attempting search");
            ball = GameObject.Find("D20_Faces"); 

            if (!ball) { Debug.LogError("ball not found!"); }
        }

        if (!diceController){
            Debug.LogWarning("diceController not set, attempting search");
            diceController = ball.GetComponent<DiceController>(); 

            if (!diceController) { Debug.LogError("diceController not found!"); }
        }
    }

    public void ResetBall() { 
        ball.transform.position = diceController.lastShotPos;
        ball.transform.rotation = diceController.lastShotRot;
    }
}
