using UnityEngine;

public class GravityAlteration : AlterationBase
{
    public float force = 18f;

    protected override void ApplyEffect(PlayerController player)
    {
        Vector3 flatPlayerPosition = new Vector3(
            player.transform.position.x,
            0f,
            player.transform.position.z
        );

        Vector3 flatGravityPosition = new Vector3(
            transform.position.x,
            0f,
            transform.position.z
        );

        Vector3 direction = flatGravityPosition - flatPlayerPosition;

        if (direction.sqrMagnitude < 0.01f) return;

        direction.Normalize();

        MovementSystem movement = player.GetComponent<MovementSystem>();

        if (movement == null) return;

        movement.AddExternalForce(direction * force * Time.deltaTime);
    }
}