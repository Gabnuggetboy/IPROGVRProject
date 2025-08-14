using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public Transform head; // Reference to the player's head (e.g., Main Camera or XR Rig head)
    public float spawnDistance = 1.5f; // Distance from head to spawn inventory UI
    public GameObject inventoryCanvas; // Reference to the inventory UI Canvas
    private List<Image> inventorySlots = new List<Image>(); // List of UI Image slots
    private int maxSlots = 5; // Maximum number of items in inventory
    private int currentSlotIndex = 0; // Tracks the next available slot

    void Start()
    {
        // Find the inventory UI Canvas
        if (inventoryCanvas == null)
        {
            inventoryCanvas = GameObject.FindWithTag("InventoryUI");
        }

        
        Transform panel = inventoryCanvas.transform.Find("Panel"); 
        foreach (Transform slot in panel)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null)
            {
                inventorySlots.Add(slotImage);
                slotImage.enabled = false; // Hide slots initially
            }
        }

       
        inventoryCanvas.SetActive(true); 
    }

    void Update()
    {
        
        if (inventoryCanvas.activeSelf)
        {
            inventoryCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            inventoryCanvas.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            inventoryCanvas.transform.forward *= -1; // Flip to face the player
        }
    }

    
    public void OnObjectGrabbed(SelectEnterEventArgs args)
    {
       
        EquippableItem item = args.interactableObject.transform.GetComponent<EquippableItem>();
        if (item != null && currentSlotIndex < maxSlots)
        {
            
            AddItemToInventory(item);
            Destroy(args.interactableObject.transform.gameObject);
        }
    }

    private void AddItemToInventory(EquippableItem item)
    {
        
        if (currentSlotIndex < inventorySlots.Count)
        {
            Image slot = inventorySlots[currentSlotIndex];
            slot.sprite = item.inventoryIcon;
            slot.enabled = true; 
            currentSlotIndex++; 
        }
    }

        public void ToggleInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }
}