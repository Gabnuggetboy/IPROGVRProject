using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;
using System.Collections.Generic;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public int X_START = -150; // Adjust based on your UI layout
    public int Y_START = 100;
    public int X_SPACE_BETWEEN_ITEM = 100;
    public int NUMBER_OF_COLUMN = 2; // 2x2 grid for 4 slots
    public int Y_SPACE_BETWEEN_ITEM = -100;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        // Ensure the Canvas has a Tracked Device Graphic Raycaster
        if (!GetComponent<TrackedDeviceGraphicRaycaster>())
        {
            gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();
        }

        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        // Clear existing displayed items
        foreach (var item in itemsDisplayed)
        {
            Destroy(item.Value);
        }
        itemsDisplayed.Clear();

        // Create display for all 4 slots
        for (int i = 0; i < 4; i++)
        {
            InventorySlot slot = i < inventory.Container.Count ? inventory.Container[i] : null;
            ItemObject item = slot?.item;
            int amount = slot?.amount ?? 0;

            // Use the item's prefab or empty prefab
            GameObject obj = Instantiate(item != null ? item.prefab : inventory.emptyItem.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            var textComponent = obj.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = amount > 0 ? amount.ToString("n0") : "";
            }
            itemsDisplayed.Add(slot ?? new InventorySlot(-1, inventory.emptyItem, 0), obj);
        }
    }

    public void UpdateDisplay()
    {
        // Update existing displayed items
        var displayedKeys = new List<InventorySlot>(itemsDisplayed.Keys);
        for (int i = 0; i < 4; i++)
        {
            InventorySlot slot = i < inventory.Container.Count ? inventory.Container[i] : null;
            ItemObject item = slot?.item;
            int amount = slot?.amount ?? 0;

            InventorySlot key = displayedKeys[i];
            GameObject obj = itemsDisplayed[key];

            // If the slot has changed, destroy and recreate the UI element
            if (key.item != item || key.amount != amount)
            {
                Destroy(obj);
                itemsDisplayed.Remove(key);

                obj = Instantiate(item != null ? item.prefab : inventory.emptyItem.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                var textComponent = obj.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = amount > 0 ? amount.ToString("n0") : "";
                }
                itemsDisplayed.Add(slot ?? new InventorySlot(-1, inventory.emptyItem, 0), obj);
            }
            else
            {
                // Update text if amount changed
                var textComponent = obj.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = amount > 0 ? amount.ToString("n0") : "";
                }
            }
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}