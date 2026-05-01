using System.Collections.Generic;
using UnityEngine;

public class PhaseAlteration : AlterationBase
{
    private Vector3 targetPosition;
    private HashSet<PlayerController> usedBy = new HashSet<PlayerController>();

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    protected override void ApplyEffect(PlayerController player)
    {
        if (usedBy.Contains(player)) return;

        player.transform.position = targetPosition;

        MovementSystem movement = player.GetComponent<MovementSystem>();
        if (movement != null)
        {
            movement.ClearForces();
        }

        usedBy.Add(player);
    }
}