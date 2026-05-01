using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void UpdateScore(int player1Score, int player2Score)
    {
        scoreText.text = player1Score + " - " + player2Score;
    }
}