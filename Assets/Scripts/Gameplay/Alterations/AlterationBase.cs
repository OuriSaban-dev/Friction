using System.Collections.Generic;
using UnityEngine;

public abstract class AlterationBase : MonoBehaviour
{
    public float radius = 3f;
    public float duration = 0.5f;

    protected bool isInitialized;

    public bool IsExpired => duration <= 0f;

    public void Initialize()
    {
        isInitialized = true;

        // visuel simple
        transform.localScale = new Vector3(radius * 2f, 0.1f, radius * 2f);
    }

    public void Tick(List<PlayerController> players, float deltaTime)
    {
        duration -= deltaTime;

        foreach (var player in players)
        {
            if (player == null) continue;

            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= radius)
            {
                ApplyEffect(player);
            }
        }
    }

    protected abstract void ApplyEffect(PlayerController player);
}