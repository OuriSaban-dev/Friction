using System.Collections.Generic;
using UnityEngine;

public class PulseAlteration : AlterationBase
{
    [Header("Momentum Pulse")]
    public float baseForce = 6f;
    public float bonusForce = 7f;

    [Header("Owner Control")]
    public float ownerForceMultiplier = 0.35f;

    private HashSet<PlayerController> affected = new HashSet<PlayerController>();

    protected override void ApplyEffect(PlayerController player)
    {
        if (affected.Contains(player)) return;

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
        {
            if (owner != null)
            {
                direction = owner.transform.forward;
            }
            else
            {
                direction = Vector3.forward;
            }
        }

        direction.Normalize();

        float momentumRatio = GetOwnerMomentumRatio();

        float finalForce = baseForce + bonusForce * momentumRatio;

        if (player == owner)
        {
            finalForce *= ownerForceMultiplier;
        }

        MovementSystem targetMovement = player.GetComponent<MovementSystem>();

        if (targetMovement != null)
        {
            targetMovement.AddExternalForce(direction * finalForce);
        }

        PlayerImpactFeedback feedback = player.GetComponent<PlayerImpactFeedback>();
        if (feedback != null)
        {
            feedback.PlayImpact();
        }

        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(0.08f, 0.15f);
        }

        affected.Add(player);
    }

    private float GetOwnerMomentumRatio()
    {
        if (owner == null) return 0f;

        MovementSystem ownerMovement = owner.GetComponent<MovementSystem>();
        if (ownerMovement == null) return 0f;

        return Mathf.Clamp01(ownerMovement.CurrentSpeed / ownerMovement.MaxSpeed);
    }
}