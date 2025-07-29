using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayActivatorTag : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public float maxAngle = 15f;
    public float detectionRadius = 5f;
    public string equippableTag = "Equippable";

    private LineRenderer lineRenderer;

    void Start()
    {
        if (rayInteractor != null)
            lineRenderer = rayInteractor.GetComponent<LineRenderer>();
    }

    void Update()
    {
        bool isFacingAny = false;

        GameObject[] equippables = GameObject.FindGameObjectsWithTag(equippableTag);
        foreach (GameObject obj in equippables)
        {
            Vector3 toObject = obj.transform.position - transform.position;

            // Skip if too far
            if (toObject.magnitude > detectionRadius)
                continue;

            float angle = Vector3.Angle(transform.forward, toObject.normalized);

            if (angle < maxAngle)
            {
                isFacingAny = true;
                break;
            }
        }

        // Enable/disable ray interactor or just line visual
        if (rayInteractor != null)
            rayInteractor.enabled = isFacingAny;

        if (lineRenderer != null)
            lineRenderer.enabled = isFacingAny;
    }
}
