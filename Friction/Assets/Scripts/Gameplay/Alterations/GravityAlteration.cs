using UnityEngine;

public class GravityAlteration : AlterationBase
{
    [Header("Forces")]
    public float outerForce = 18f;
    public float innerForce = 45f;
    public float coreForce = 70f;

    [Header("Zones")]
    public float innerRadiusRatio = 0.52f;
    public float coreRadiusRatio = 0.25f;

    [Header("Owner Control")]
    public float ownerForceMultiplier = 0.25f;

    [Header("Core Stabilization")]
    public float coreDamping = 14f;

    protected override void ApplyEffect(PlayerController player)
    {
        MovementSystem movement = player.GetComponent<MovementSystem>();
        if (movement == null) return;

        Vector3 toCenter = transform.position - player.transform.position;
        toCenter.y = 0f;

        float distance = toCenter.magnitude;
        if (distance < 0.01f) return;

        Vector3 direction = toCenter.normalized;

        float innerRadius = radius * innerRadiusRatio;
        float coreRadius = radius * coreRadiusRatio;

        float force = outerForce;

        if (distance <= innerRadius)
        {
            force = innerForce;
        }

        if (distance <= coreRadius)
        {
            force = coreForce;
            movement.DampenImpulse(coreDamping * Time.deltaTime);
        }

        if (player == owner)
        {
            force *= ownerForceMultiplier;
        }

        movement.AddExternalForce(direction * force * Time.deltaTime);
    }
}