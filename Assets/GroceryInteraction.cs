using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GroceryInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float raycastDistance = 5f;
    public LayerMask scanLayer;

    [Header("Input Settings")]
    public InputActionProperty triggerAction;

    [Header("Target Tags")]
    public GroceryProperties.GroceryTag[] scanableTags = {
        GroceryProperties.GroceryTag.Frozen,
        GroceryProperties.GroceryTag.Refrigerated,
        GroceryProperties.GroceryTag.Regular,
        GroceryProperties.GroceryTag.Bulk
    };

    private GameObject currentTarget;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, scanLayer))
        {
            GameObject hitObj = hit.collider.gameObject;
            GroceryProperties grocery = hitObj.GetComponent<GroceryProperties>();

            // Only interact with objects that have the specified tags
            if (grocery != null && IsValidTag(grocery.groceryTag))
            {
                if (hitObj != currentTarget)
                {
                    ClearPreviousOutline();
                    currentTarget = hitObj;
                }

                OutlineHandler outlineHandler = hitObj.GetComponent<OutlineHandler>();
                XRGrabInteractable grab = hitObj.GetComponent<XRGrabInteractable>();

                if (outlineHandler != null)
                {
                    if (!grocery.isScanned)
                    {
                        // Show red outline and disable grabbing
                        outlineHandler.ShowOutline(Color.red);
                        if (grab != null)
                        {
                            grab.enabled = false; // Completely disable grabbing
                        }

                        // Handle scanning
                        if (triggerAction.action.WasPressedThisFrame())
                        {
                            ScanItem(grocery, hitObj, outlineHandler, grab);
                        }
                    }
                    else
                    {
                        // Show green outline and enable grabbing
                        outlineHandler.ShowOutline(Color.green);
                        if (grab != null)
                        {
                            grab.enabled = true; // Enable grabbing for scanned items
                        }
                    }
                }
            }
            else
            {
                // If we're looking at something that's not a valid grocery item, clear outline
                ClearPreviousOutline();
            }
        }
        else
        {
            ClearPreviousOutline();
        }
    }

    private bool IsValidTag(GroceryProperties.GroceryTag tag)
    {
        foreach (var validTag in scanableTags)
        {
            if (tag == validTag)
                return true;
        }
        return false;
    }

    private void ScanItem(GroceryProperties grocery, GameObject hitObj, OutlineHandler outlineHandler, XRGrabInteractable grab)
    {
        grocery.isScanned = true;
        ListTracker.instance.MarkScanned(hitObj);

        // Change to green outline
        outlineHandler.ShowOutline(Color.green);

        // Enable grabbing
        if (grab != null)
        {
            grab.enabled = true;
        }

        Debug.Log($"Scanned: {grocery.itemName} ({grocery.tag})");
    }

    private void ClearPreviousOutline()
    {
        if (currentTarget != null)
        {
            OutlineHandler outlineHandler = currentTarget.GetComponent<OutlineHandler>();
            if (outlineHandler != null)
                outlineHandler.HideOutline();
            currentTarget = null;
        }
    }
}