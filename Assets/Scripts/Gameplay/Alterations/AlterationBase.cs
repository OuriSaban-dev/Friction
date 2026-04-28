using System.Collections.Generic;
using UnityEngine;

public abstract class AlterationBase : MonoBehaviour
{
    [Header("Base")]
    public float radius = 3f;
    public float duration = 1f;

    protected PlayerController owner;
    protected bool isInitialized;

    public bool IsExpired => duration <= 0f;

    public virtual void Initialize(PlayerController owner)
    {
        this.owner = owner;
        isInitialized = true;

        transform.localScale = new Vector3(radius * 2f, 0.4f, radius * 2f);
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
    }

    public void Tick(List<PlayerController> players, float deltaTime)
    {
        duration -= deltaTime;

        foreach (PlayerController player in players)
        {
            if (player == null) continue;

            float distance = Vector3.Distance(
                new Vector3(player.transform.position.x, 0f, player.transform.position.z),
                new Vector3(transform.position.x, 0f, transform.position.z)
            );

            if (distance <= radius)
            {
                ApplyEffect(player);
            }
        }
    }

    protected abstract void ApplyEffect(PlayerController player);
}