using System.Collections.Generic;
using UnityEngine;

public class AlterationSystem : MonoBehaviour
{
    public List<PlayerController> players = new List<PlayerController>();

    public PulseAlteration pulsePrefab;

    private List<AlterationBase> activeAlterations = new List<AlterationBase>();

    public GravityAlteration gravityPrefab;

    public PhaseAlteration phasePrefab;
    public float phaseDistance = 6f;

    public void SpawnPulse(Vector3 position)
    {
        PulseAlteration pulse = Instantiate(pulsePrefab, position, Quaternion.identity);
        pulse.Initialize();

        activeAlterations.Add(pulse);
    }

    private void Update()
    {
        for (int i = activeAlterations.Count - 1; i >= 0; i--)
        {
            var alt = activeAlterations[i];

            alt.Tick(players, Time.deltaTime);

            if (alt.IsExpired)
            {
                Destroy(alt.gameObject);
                activeAlterations.RemoveAt(i);
            }
        }
    }

    public void SpawnGravity(Vector3 position)
    {
        position.y = 0.3f;

        GravityAlteration gravity = Instantiate(gravityPrefab, position, Quaternion.identity);
        gravity.Initialize();

        activeAlterations.Add(gravity);
    }

    public void SpawnPhase(Vector3 position, Vector3 direction)
    {
        position.y = 0.3f;

        Vector3 target = position + direction.normalized * phaseDistance;
        target.y = 0.3f;

        PhaseAlteration phase = Instantiate(phasePrefab, position, Quaternion.identity);
        phase.Initialize();
        phase.SetTarget(target);

        activeAlterations.Add(phase);
    }
}