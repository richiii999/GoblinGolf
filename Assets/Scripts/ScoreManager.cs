using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text ScoreText;

    public void UpdateScore(int score)
    {
        ScoreText.text = "Strokes: " + score;
    }
}
