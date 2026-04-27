using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public float arenaRadius = 12f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        DrawCircle(Vector3.zero, arenaRadius);
    }

    private void DrawCircle(Vector3 center, float radius)
    {
        const int steps = 96;
        Vector3 previous = center + new Vector3(radius, 0f, 0f);

        for (int i = 1; i <= steps; i++)
        {
            float angle = i / (float)steps * Mathf.PI * 2f;
            Vector3 next = center + new Vector3(
                Mathf.Cos(angle) * radius,
                0f,
                Mathf.Sin(angle) * radius
            );

            Gizmos.DrawLine(previous, next);
            previous = next;
        }
    }
}