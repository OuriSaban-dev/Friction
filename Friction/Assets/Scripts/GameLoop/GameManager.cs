using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public BoundsSystem boundsSystem;
    public AlterationSystem alterationSystem;

    public Transform player1;
    public Transform player2;

    public ScoreUI scoreUI;

    [Header("Reset")]
    public float resetDelay = 0.5f;

    private Vector3 player1Start;
    private Vector3 player2Start;

    private int player1Score;
    private int player2Score;

    private bool resetting;

    private void Start()
    {
        player1Start = player1.position;
        player2Start = player2.position;

        UpdateScoreUI();

        if (MatchTelemetry.Instance != null)
        {
            MatchTelemetry.Instance.StartRound();
        }
    }

    private void Update()
    {
        if (resetting) return;

        if (boundsSystem.IsOutsideArena(player1.position))
        {
            HandlePlayerDeath(player1, player2);
            return;
        }

        if (boundsSystem.IsOutsideArena(player2.position))
        {
            HandlePlayerDeath(player2, player1);
            return;
        }
    }

    private void HandlePlayerDeath(Transform loserTransform, Transform winnerTransform)
    {
        PlayerController loser = loserTransform.GetComponent<PlayerController>();
        PlayerController winner = winnerTransform.GetComponent<PlayerController>();

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayDeath();
        }

        if (MatchTelemetry.Instance != null)
        {
            MatchTelemetry.Instance.EndRound(loser, winner);
        }

        if (winnerTransform == player1)
        {
            player1Score++;
        }
        else if (winnerTransform == player2)
        {
            player2Score++;
        }

        UpdateScoreUI();

        StartCoroutine(ResetRound());
    }

    private IEnumerator ResetRound()
    {
        resetting = true;

        yield return new WaitForSeconds(resetDelay);

        if (alterationSystem != null)
        {
            alterationSystem.ClearAll();
        }

        player1.position = player1Start;
        player2.position = player2Start;

        MovementSystem player1Movement = player1.GetComponent<MovementSystem>();
        MovementSystem player2Movement = player2.GetComponent<MovementSystem>();

        if (player1Movement != null)
        {
            player1Movement.ClearMovement();
        }

        if (player2Movement != null)
        {
            player2Movement.ClearMovement();
        }

        PlayerController player1Controller = player1.GetComponent<PlayerController>();
        PlayerController player2Controller = player2.GetComponent<PlayerController>();

        if (player1Controller != null)
        {
            player1Controller.ResetCooldowns();
        }

        if (player2Controller != null)
        {
            player2Controller.ResetCooldowns();
        }

        resetting = false;

        if (MatchTelemetry.Instance != null)
        {
            MatchTelemetry.Instance.StartRound();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreUI != null)
        {
            scoreUI.UpdateScore(player1Score, player2Score);
        }
    }
}