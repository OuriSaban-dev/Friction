using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPosition;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        Instance = this;
        originalPosition = transform.position;
    }

    public void Shake(float duration, float intensity)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, intensity));
    }

    private IEnumerator ShakeRoutine(float duration, float intensity)
    {
        float timer = 0f;

        while (timer < duration)
        {
            Vector3 offset = new Vector3(
                Random.Range(-intensity, intensity),
                Random.Range(-intensity, intensity),
                0f
            );

            transform.position = originalPosition + offset;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}