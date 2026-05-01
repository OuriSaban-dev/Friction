using System.Collections.Generic;
using UnityEngine;

public class AlterationSystem : MonoBehaviour
{
    [Header("Players")]
    public List<PlayerController> players = new List<PlayerController>();

    [Header("Prefabs")]
    public PulseAlteration pulsePrefab;
    public GravityAlteration gravityPrefab;
    public PhaseAlteration phasePrefab;

    [Header("Phase")]
    public float phaseDistance = 4.75f;

    private List<AlterationBase> activeAlterations = new List<AlterationBase>();

    public void SpawnPulse(Vector3 position, PlayerController owner)
    {
        position.y = 0.3f;

        PulseAlteration pulse = Instantiate(pulsePrefab, position, Quaternion.identity);
        pulse.Initialize(owner);

        activeAlterations.Add(pulse);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayPulse();
        }
    }

    public void SpawnGravity(Vector3 position, PlayerController owner)
    {
        position.y = 0.3f;

        GravityAlteration gravity = Instantiate(gravityPrefab, position, Quaternion.identity);
        gravity.Initialize(owner);

        activeAlterations.Add(gravity);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayGravity();
        }
    }

    public void SpawnPhase(Vector3 position, Vector3 direction, PlayerController owner)
    {
        position.y = 0.3f;

        Vector3 target = position + direction.normalized * phaseDistance;
        target.y = 0.3f;

        PhaseAlteration phase = Instantiate(phasePrefab, position, Quaternion.identity);
        phase.Initialize(owner);
        phase.SetTarget(target);

        activeAlterations.Add(phase);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayPhase();
        }
    }

    private void Update()
    {
        for (int i = activeAlterations.Count - 1; i >= 0; i--)
        {
            AlterationBase alteration = activeAlterations[i];

            alteration.Tick(players, Time.deltaTime);

            if (alteration.IsExpired)
            {
                activeAlterations.RemoveAt(i);
                Destroy(alteration.gameObject);
            }
        }
    }

    public void ClearAll()
    {
        foreach (AlterationBase alteration in activeAlterations)
        {
            if (alteration != null)
            {
                Destroy(alteration.gameObject);
            }
        }

        activeAlterations.Clear();
    }
}