using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryProperties : MonoBehaviour
{
    public string itemId;
    public string itemName;
    public bool isScanned = false;
    public enum GroceryTag { Frozen, Refrigerated, Regular, Bulk }
    public GroceryTag groceryTag;

    [SerializeField] private Material outlineMaterial; // Material for outline effect
    private Material originalMaterial;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    public void SetOutline(Color color)
    {
        if (outlineMaterial != null)
        {
            objectRenderer.material = outlineMaterial;
            outlineMaterial.color = color;
        }
    }

    public void ResetOutline()
    {
        if (originalMaterial != null)
        {
            objectRenderer.material = originalMaterial;
        }
    }
}