using UnityEngine;

public class MatchTelemetry : MonoBehaviour
{
    public static MatchTelemetry Instance;

    private float roundStartTime;

    private string lastAbilityUsed = "None";
    private string lastAbilityPlayer = "None";

    private int roundNumber = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        roundStartTime = Time.time;
        lastAbilityUsed = "None";
        lastAbilityPlayer = "None";
    }

    public void RecordAbility(string abilityName, PlayerController player)
    {
        lastAbilityUsed = abilityName;
        lastAbilityPlayer = player != null ? player.name : "Unknown";
    }

    public void EndRound(PlayerController loser, PlayerController winner)
    {
        float duration = Time.time - roundStartTime;

        Debug.Log(
            "[MATCH TELEMETRY]\n" +
            "Round: " + roundNumber + "\n" +
            "Duration: " + duration.ToString("F2") + "s\n" +
            "Winner: " + GetName(winner) + "\n" +
            "Loser: " + GetName(loser) + "\n" +
            "Last Ability: " + lastAbilityUsed + "\n" +
            "Last Ability Player: " + lastAbilityPlayer
        );

        roundNumber++;
    }

    private string GetName(PlayerController player)
    {
        return player != null ? player.name : "Unknown";
    }
}