using System.Collections.Generic;
using UnityEngine;

public class PulseAlteration : AlterationBase
{
    [Header("Momentum Pulse")]
    public float baseForce = 5f;
    public float bonusForce = 7f;

    [Header("Owner Control")]
    public float ownerForceMultiplier = 0.25f;

    private HashSet<PlayerController> affected = new HashSet<PlayerController>();

    protected override void ApplyEffect(PlayerController player)
    {
        if (affected.Contains(player)) return;

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
        {
            direction = owner != null ? owner.transform.forward : Vector3.forward;
        }

        direction.Normalize();

        float momentumRatio = GetOwnerMomentumRatio();
        float finalForce = baseForce + bonusForce * momentumRatio;

        if (player == owner)
        {
            finalForce *= ownerForceMultiplier;
        }

        MovementSystem movement = player.GetComponent<MovementSystem>();
        if (movement != null)
        {
            movement.AddExternalForce(direction * finalForce);
        }

        affected.Add(player);
    }

    private float GetOwnerMomentumRatio()
    {
        if (owner == null) return 0f;

        MovementSystem movement = owner.GetComponent<MovementSystem>();
        if (movement == null) return 0f;

        return Mathf.Clamp01(movement.CurrentSpeed / movement.MaxSpeed);
    }
}