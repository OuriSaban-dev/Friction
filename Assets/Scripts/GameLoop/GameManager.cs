using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public BoundsSystem boundsSystem;
    public Transform player1;
    public Transform player2;

    [Header("Reset")]
    public float resetDelay = 0.8f;

    private Vector3 player1Start;
    private Vector3 player2Start;
    private bool resetting;

    public ScoreUI scoreUI;
    private int player1Score;
    private int player2Score;

    private void Start()
    {
        player1Start = player1.position;
        player2Start = player2.position;

        UpdateScoreUI();
    }

    private void Update()
    {
        if (resetting) return;

        if (player1 == null || player2 == null)
        {
            Debug.LogError("Player reference missing!");
            return;
        }

        if (boundsSystem.IsOutsideArena(player1.position))
        {
            player2Score++;
            UpdateScoreUI();
            StartCoroutine(ResetRound());
        }

        if (boundsSystem.IsOutsideArena(player2.position))
        {
            player1Score++;
            UpdateScoreUI();
            StartCoroutine(ResetRound());
        }
    }

    private IEnumerator ResetRound()
    {
        resetting = true;

        yield return new WaitForSeconds(resetDelay);

        player1.position = player1Start;
        player2.position = player2Start;

        resetting = false;
    }

    private void UpdateScoreUI()
    {
        if (scoreUI != null)
        {
            scoreUI.UpdateScore(player1Score, player2Score);
        }
    }
}