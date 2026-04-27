using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    [Header("External Forces")]
    public float externalForceDecay = 10f;
    public float maxExternalForce = 25f;

    private Vector3 inputDirection;
    private Vector3 externalForce;

    public void SetInput(Vector3 direction)
    {
        inputDirection = direction;
    }

    public void AddExternalForce(Vector3 force)
    {
        externalForce += force;
        externalForce = Vector3.ClampMagnitude(externalForce, maxExternalForce);
    }

    public void ClearForces()
    {
        externalForce = Vector3.zero;
    }

    private void Update()
    {
        Vector3 velocity = inputDirection * moveSpeed + externalForce;

        transform.position += velocity * Time.deltaTime;

        externalForce = Vector3.Lerp(
            externalForce,
            Vector3.zero,
            externalForceDecay * Time.deltaTime
        );

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            transform.forward = inputDirection;
        }
    }
}