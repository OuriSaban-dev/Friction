using UnityEngine;

public class BoundsSystem : MonoBehaviour
{
    public float arenaRadius = 12f;

    public bool IsOutsideArena(Vector3 position)
    {
        Vector2 flatPosition = new Vector2(position.x, position.z);
        return flatPosition.magnitude > arenaRadius;
    }
}