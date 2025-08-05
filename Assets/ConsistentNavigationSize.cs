using UnityEngine;

public class ConsistentNavigationSize : MonoBehaviour
{
    public Camera targetCamera;
    public float sizeOnScreen = 0.05f; // Adjust to your desired screen size
    public bool scaleByFOV = false;

    void LateUpdate()
    {
        if (targetCamera == null)
            return;

        float distance = Vector3.Distance(transform.position, targetCamera.transform.position);

        // Optional: adjust for FOV to keep visual size more consistent across different settings
        float scaleFactor = sizeOnScreen * distance;
        if (scaleByFOV)
            scaleFactor *= Mathf.Tan(targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        transform.localScale = Vector3.one * scaleFactor;
    }
}
