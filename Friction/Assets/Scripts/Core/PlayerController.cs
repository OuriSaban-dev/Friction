using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public MovementSystem movement;
    public AlterationSystem alterationSystem;

    [Header("Input Keys")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode pulseKey;
    public KeyCode gravityKey;
    public KeyCode phaseKey;

    [Header("Cooldowns")]
    public float pulseCooldown = 2f;
    public float gravityCooldown = 3f;
    public float phaseCooldown = 4f;

    public float pulseCd;
    public float gravityCd;
    public float phaseCd;

    public Vector3 CurrentInputDirection { get; private set; }

    private void Awake()
    {
        if (movement == null)
        {
            movement = GetComponent<MovementSystem>();
        }
    }

    private void Update()
    {
        ReadMovementInput();
        TickCooldowns();
        ReadAbilityInput();
    }

    private void ReadMovementInput()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKey(up)) input.z += 1f;
        if (Input.GetKey(down)) input.z -= 1f;
        if (Input.GetKey(left)) input.x -= 1f;
        if (Input.GetKey(right)) input.x += 1f;

        input = input.sqrMagnitude > 0.01f
            ? input.normalized
            : Vector3.zero;

        CurrentInputDirection = input;

        movement.SetInput(input);
    }

    private void TickCooldowns()
    {
        pulseCd = Mathf.Max(0f, pulseCd - Time.deltaTime);
        gravityCd = Mathf.Max(0f, gravityCd - Time.deltaTime);
        phaseCd = Mathf.Max(0f, phaseCd - Time.deltaTime);
    }

    private void ReadAbilityInput()
    {
        if (Input.GetKeyDown(pulseKey) && pulseCd <= 0f)
        {
            alterationSystem.SpawnPulse(transform.position, this);
            pulseCd = pulseCooldown;
        }

        if (Input.GetKeyDown(gravityKey) && gravityCd <= 0f)
        {
            alterationSystem.SpawnGravity(transform.position, this);
            gravityCd = gravityCooldown;
        }

        if (Input.GetKeyDown(phaseKey) && phaseCd <= 0f)
        {
            Vector3 phaseDirection = CurrentInputDirection.sqrMagnitude > 0.01f
                ? CurrentInputDirection
                : transform.forward;

            phaseDirection.y = 0f;

            if (phaseDirection.sqrMagnitude < 0.01f)
            {
                phaseDirection = Vector3.forward;
            }
            else
            {
                phaseDirection.Normalize();
            }

            alterationSystem.SpawnPhase(transform.position, phaseDirection, this);
            phaseCd = phaseCooldown;
        }
    }

    public void ResetCooldowns()
    {
        pulseCd = 0f;
        gravityCd = 0f;
        phaseCd = 0f;
    }
}