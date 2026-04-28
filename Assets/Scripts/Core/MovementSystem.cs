using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [Header("Movement Feel")]
    public float maxSpeed = 14f;
    public float startSpeed = 5.5f;
    public float acceleration = 16f;
    public float deceleration = 30f;
    public float turnAcceleration = 44f;

    [Header("External Forces")]
    public float impulseDecay = 7f;
    public float maxImpulseSpeed = 22f;
    public float counterInputMultiplier = 0.5f;

    [Header("Impact Control")]
    public float impactControlLockDuration = 0.20f;
    public float impactControlMultiplier = 0.25f;

    [Header("Impact Velocity Cut")]
    public float controlledVelocityCutOnImpact = 0.25f;

    private Vector3 inputDirection;
    private Vector3 controlledVelocity;
    private Vector3 impulseVelocity;
    private float impactLockTimer;

    public float CurrentSpeed => controlledVelocity.magnitude;
    public float MaxSpeed => maxSpeed;
    public Vector3 Velocity => controlledVelocity + impulseVelocity;

    public void SetInput(Vector3 direction)
    {
        inputDirection = direction;
    }

    public void AddExternalForce(Vector3 force)
    {
        controlledVelocity *= controlledVelocityCutOnImpact;

        impulseVelocity += force;
        impulseVelocity = Vector3.ClampMagnitude(impulseVelocity, maxImpulseSpeed);

        impactLockTimer = impactControlLockDuration;
    }

    public void ClearMovement()
    {
        inputDirection = Vector3.zero;
        controlledVelocity = Vector3.zero;
        impulseVelocity = Vector3.zero;
        impactLockTimer = 0f;
    }

    public void ClearForces()
    {
        impulseVelocity = Vector3.zero;
        impactLockTimer = 0f;
    }

    private void Update()
    {
        UpdateImpactLock();
        UpdateControlledMovement();
        UpdateImpulseMovement();

        Vector3 finalVelocity = controlledVelocity + impulseVelocity;

        transform.position += finalVelocity * Time.deltaTime;

        if (finalVelocity.sqrMagnitude > 0.01f)
        {
            transform.forward = finalVelocity.normalized;
        }
    }

    private void UpdateImpactLock()
    {
        if (impactLockTimer > 0f)
        {
            impactLockTimer -= Time.deltaTime;
        }
    }

    private void UpdateControlledMovement()
    {
        float controlMultiplier = impactLockTimer > 0f
            ? impactControlMultiplier
            : 1f;

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Vector3 targetVelocity = inputDirection * maxSpeed;

            if (controlledVelocity.magnitude < startSpeed)
            {
                controlledVelocity = inputDirection * startSpeed;
            }

            float dot = Vector3.Dot(
                controlledVelocity.normalized,
                inputDirection.normalized
            );

            float accelToUse = dot < 0.3f
                ? turnAcceleration
                : acceleration;

            accelToUse *= controlMultiplier;

            controlledVelocity = Vector3.MoveTowards(
                controlledVelocity,
                targetVelocity,
                accelToUse * Time.deltaTime
            );
        }
        else
        {
            controlledVelocity = Vector3.MoveTowards(
                controlledVelocity,
                Vector3.zero,
                deceleration * controlMultiplier * Time.deltaTime
            );
        }

        controlledVelocity = Vector3.ClampMagnitude(controlledVelocity, maxSpeed);
    }

    private void UpdateImpulseMovement()
    {
        float decay = impulseDecay;

        if (inputDirection.sqrMagnitude > 0.01f && impulseVelocity.sqrMagnitude > 0.01f)
        {
            Vector3 inputDir = inputDirection.normalized;
            Vector3 impulseDir = impulseVelocity.normalized;

            float opposition = Vector3.Dot(inputDir, -impulseDir);

            if (opposition > 0f)
            {
                decay *= 1f + opposition * counterInputMultiplier;
            }
        }

        impulseVelocity = Vector3.MoveTowards(
            impulseVelocity,
            Vector3.zero,
            decay * Time.deltaTime
        );
    }
}