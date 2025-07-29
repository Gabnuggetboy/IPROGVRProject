using UnityEngine;
using UnityEngine.UI;

public class NavigationMarker : MonoBehaviour
{
    public Transform target;
    public Camera vrCamera;
    public float markerDistance = 2f;
    public RectTransform markerCanvas;
    public RectTransform arrow;
    public float maxAngle = 45f;

    void Update()
    {
        if (target == null) { Debug.LogError("Target not assigned!"); return; }

        Vector3 cameraPos = vrCamera.transform.position;
        Vector3 targetPos = target.position;
        Vector3 directionToTarget = (targetPos - cameraPos).normalized;

        Vector3 cameraForward = vrCamera.transform.forward;
        cameraForward.y = 0;
        directionToTarget.y = 0;
        cameraForward.Normalize();
        directionToTarget.Normalize();

        float angle = Vector3.Angle(cameraForward, directionToTarget);
        Debug.Log("Angle to Target: " + angle);

        if (angle < maxAngle)
        {
            Vector3 screenPoint = vrCamera.WorldToViewportPoint(targetPos);
            bool isOffScreen = screenPoint.z < 0 || screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
            Debug.Log("Is Off Screen: " + isOffScreen);

            if (!isOffScreen)
            {
                Vector2 canvasPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    markerCanvas,
                    vrCamera.WorldToScreenPoint(targetPos),
                    vrCamera,
                    out canvasPos
                );
                markerCanvas.anchoredPosition = canvasPos;
                arrow.gameObject.SetActive(false);
            }
            else
            {
                PlaceMarkerAtEdge(directionToTarget);
                arrow.gameObject.SetActive(true);
            }
        }
        else
        {
            PlaceMarkerAtEdge(directionToTarget);
            arrow.gameObject.SetActive(true);
        }

        if (arrow.gameObject.activeSelf)
        {
            Vector3 screenTarget = vrCamera.WorldToScreenPoint(targetPos);
            Vector3 screenMarker = vrCamera.WorldToScreenPoint(markerCanvas.position);
            Vector3 arrowDirection = (screenTarget - screenMarker).normalized;
            float arrowAngle = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.Euler(0, 0, arrowAngle);
        }
    }

    void PlaceMarkerAtEdge(Vector3 directionToTarget)
    {
        Vector3 markerPos = vrCamera.transform.position + vrCamera.transform.forward * markerDistance;
        Vector3 flatCameraForward = vrCamera.transform.forward;
        flatCameraForward.y = 0;
        flatCameraForward.Normalize();

        Vector3 cameraRight = vrCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        Vector3 cameraUp = Vector3.Cross(cameraRight, Vector3.up);

        float rightDot = Vector3.Dot(directionToTarget, cameraRight);
        float upDot = Vector3.Dot(directionToTarget, cameraUp);

        float edgeAngle = maxAngle * Mathf.Deg2Rad;
        float angleToTarget = Mathf.Atan2(rightDot, upDot);
        float clampedX = Mathf.Sin(angleToTarget) * markerDistance;
        float clampedY = Mathf.Cos(angleToTarget) * markerDistance;

        Vector3 worldOffset = cameraRight * clampedX + cameraUp * clampedY;
        markerCanvas.position = vrCamera.transform.position + vrCamera.transform.forward * markerDistance + worldOffset;

        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            markerCanvas,
            vrCamera.WorldToScreenPoint(markerCanvas.position),
            vrCamera,
            out canvasPos
        );
        markerCanvas.anchoredPosition = canvasPos;
    }
}