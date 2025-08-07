using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrocerySetupHelper : MonoBehaviour
{
    [Header("Setup Settings")]
    public bool setupOnStart = true;

    void Start()
    {
        if (setupOnStart)
        {
            SetupAllGroceryItems();
        }
    }

    [ContextMenu("Setup All Grocery Items")]
    public void SetupAllGroceryItems()
    {
        GroceryProperties[] allGroceries = FindObjectsOfType<GroceryProperties>();

        foreach (GroceryProperties grocery in allGroceries)
        {
            SetupGroceryItem(grocery.gameObject);
        }

        Debug.Log($"Setup complete for {allGroceries.Length} grocery items");
    }

    private void SetupGroceryItem(GameObject groceryObj)
    {
        GroceryProperties grocery = groceryObj.GetComponent<GroceryProperties>();
        if (grocery == null) return;

        // Add OutlineHandler if missing
        if (groceryObj.GetComponent<OutlineHandler>() == null)
        {
            groceryObj.AddComponent<OutlineHandler>();
        }

        // Add Outline component if missing (required by Quick Outline)
        if (groceryObj.GetComponent<Outline>() == null)
        {
            groceryObj.AddComponent<Outline>();
        }

        // Setup XRGrabInteractable
        XRGrabInteractable grab = groceryObj.GetComponent<XRGrabInteractable>();
        if (grab == null)
        {
            grab = groceryObj.AddComponent<XRGrabInteractable>();
        }

        // Initially disable grabbing for all items with the specified tags
        if (IsScannableTag(grocery.groceryTag))
        {
            grab.enabled = false; // Start disabled
            grocery.isScanned = false; // Ensure not scanned initially
            // Keep interaction layer as "Scannable" - don't change it
        }

        // Ensure the object has a Rigidbody for XR interaction
        if (groceryObj.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = groceryObj.AddComponent<Rigidbody>();
            rb.isKinematic = false;
        }

        // Ensure the object has a Collider
        if (groceryObj.GetComponent<Collider>() == null)
        {
            Debug.LogWarning($"No collider found on {groceryObj.name}. Please add one manually.");
        }
    }

    private bool IsScannableTag(GroceryProperties.GroceryTag tag)
    {
        return tag == GroceryProperties.GroceryTag.Frozen ||
               tag == GroceryProperties.GroceryTag.Refrigerated ||
               tag == GroceryProperties.GroceryTag.Regular ||
               tag == GroceryProperties.GroceryTag.Bulk;
    }
}