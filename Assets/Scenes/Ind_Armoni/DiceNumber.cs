using TMPro;
using UnityEngine;

public class DiceNumber : MonoBehaviour
{
    public TMP_Text Number;

    public void UpdateDice(int currentNumber)
    {
        Number.text = currentNumber.ToString();
    }
}
