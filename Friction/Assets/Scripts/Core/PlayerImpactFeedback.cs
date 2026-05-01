using System.Collections;
using UnityEngine;

public class PlayerImpactFeedback : MonoBehaviour
{
    public float squashDuration = 0.08f;
    public float squashAmount = 0.25f;

    private Vector3 originalScale;
    private Coroutine routine;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void PlayImpact()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }

        routine = StartCoroutine(ImpactRoutine());
    }

    private IEnumerator ImpactRoutine()
    {
        transform.localScale = new Vector3(
            originalScale.x + squashAmount,
            originalScale.y - squashAmount,
            originalScale.z + squashAmount
        );

        yield return new WaitForSeconds(squashDuration);

        transform.localScale = originalScale;
    }
}