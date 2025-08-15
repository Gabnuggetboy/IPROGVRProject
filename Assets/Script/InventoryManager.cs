using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 1.5f;
    public GameObject inventoryCanvas;
    public Vector3 canvasOffset = new Vector3(0, -0.5f, 0);

    private List<Image> inventorySlots = new List<Image>();
    private int maxSlots = 5;
    private int currentSlotIndex = 0;

    private StartingQuestMenuManager startingQuestManager;

    void Start()
    {
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
                slotImage.enabled = false;
            }
        }

        inventoryCanvas.SetActive(true);


        startingQuestManager = FindObjectOfType<StartingQuestMenuManager>(); // NEW
    }

    void Update()
    {
        if (inventoryCanvas.activeSelf)
        {
            Vector3 forwardDirection = new Vector3(head.forward.x, 0, head.forward.z).normalized;
            inventoryCanvas.transform.position = head.position + forwardDirection * spawnDistance + canvasOffset;
            inventoryCanvas.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            inventoryCanvas.transform.forward *= -1;
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


            if (startingQuestManager != null)
            {
                startingQuestManager.OnItemEquipped(item);
            }
        }
    }

    public void ToggleInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }
}
