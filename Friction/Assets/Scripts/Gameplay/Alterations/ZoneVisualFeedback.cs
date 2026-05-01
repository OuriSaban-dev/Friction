using UnityEngine;

public class ZoneVisualFeedback : MonoBehaviour
{
    public float startScaleMultiplier = 0.2f;
    public float popSpeed = 18f;
    public float pulseRotationSpeed = 180f;

    private Vector3 targetScale;

    private void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = targetScale * startScaleMultiplier;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            popSpeed * Time.deltaTime
        );

        transform.Rotate(Vector3.up, pulseRotationSpeed * Time.deltaTime);
    }
}