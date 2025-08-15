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

    [Header("Audio Settings")]
    public AudioClip scanBeepSound;
    private AudioSource audioSource;

    private GameObject currentTarget;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, scanLayer))
        {
            GameObject hitObj = hit.collider.gameObject;
            GroceryProperties grocery = hitObj.GetComponent<GroceryProperties>();


            if (grocery != null && IsValidTag(grocery.groceryTag))
            {
                if (hitObj != currentTarget)
                {
                    ClearPreviousOutline();
                    currentTarget = hitObj;
                }

                OutlineHandler outlineHandler = hitObj.GetComponent<OutlineHandler>();
                XRGrabInteractable grab = hitObj.GetComponent<XRGrabInteractable>();

                if (grab != null)
                {
                    grab.enabled = true;
                }

                if (outlineHandler != null)
                {
                    if (!grocery.isScanned)
                    {
                        outlineHandler.ShowOutline(Color.red);

                        if (triggerAction.action.WasPressedThisFrame())
                        {
                            ScanItem(grocery, hitObj, outlineHandler);
                        }
                    }
                    else
                    {
                        outlineHandler.ShowOutline(Color.green);
                    }
                }
            }
            else
            {
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

    private void ScanItem(GroceryProperties grocery, GameObject hitObj, OutlineHandler outlineHandler)
    {
        grocery.isScanned = true;
        ListTracker.instance.MarkScanned(hitObj);

        outlineHandler.ShowOutline(Color.green);

        if (scanBeepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scanBeepSound);
        }

        Debug.Log($"Scanned: {grocery.itemName} ({grocery.groceryTag})");
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