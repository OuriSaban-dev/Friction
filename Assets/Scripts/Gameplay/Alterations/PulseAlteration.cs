using System.Collections.Generic;
using UnityEngine;

public class PulseAlteration : AlterationBase
{
    public float force = 20f;

    private HashSet<PlayerController> affected = new HashSet<PlayerController>();

    protected override void ApplyEffect(PlayerController player)
    {
        if (affected.Contains(player)) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (direction.sqrMagnitude < 0.01f)
        {
            direction = Random.insideUnitSphere;
            direction.y = 0f;
            direction.Normalize();
        }

        MovementSystem movement = player.GetComponent<MovementSystem>();
        movement.AddExternalForce(direction * force);
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(0.08f, 0.15f);
        }
        PlayerImpactFeedback feedback = player.GetComponent<PlayerImpactFeedback>();
        if (feedback != null)
        {
            feedback.PlayImpact();
        }

        affected.Add(player);
    }
}