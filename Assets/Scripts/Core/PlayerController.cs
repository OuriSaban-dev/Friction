using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public MovementSystem movement;

    [Header("Input Keys")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public AlterationSystem alterationSystem;
    public KeyCode pulseKey;
    public KeyCode gravityKey;
    public KeyCode phaseKey;

    public float pulseCd;
    public float gravityCd;
    public float phaseCd;

    public float pulseCooldown = 2f;
    public float gravityCooldown = 3f;
    public float phaseCooldown = 4f;

    private void Awake()
    {
        if (movement == null)
        {
            movement = GetComponent<MovementSystem>();
        }
    }

    private void Update()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKey(up)) input.z += 1f;
        if (Input.GetKey(down)) input.z -= 1f;
        if (Input.GetKey(left)) input.x -= 1f;
        if (Input.GetKey(right)) input.x += 1f;
        if (Input.GetKeyDown(pulseKey))
        {
            alterationSystem.SpawnPulse(transform.position);
        }
        if (Input.GetKeyDown(gravityKey))
        {
            alterationSystem.SpawnGravity(transform.position);
        }
        if (Input.GetKeyDown(phaseKey))
        {
            Vector3 dir = movement.transform.forward;
            alterationSystem.SpawnPhase(transform.position, dir);
        }

        input = input.sqrMagnitude > 0.01f ? input.normalized : Vector3.zero;

        movement.SetInput(input);
    }
}