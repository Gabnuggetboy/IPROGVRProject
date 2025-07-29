using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 2f;
    public bool isInventoryEnabled = false;
    public float verticalOffset = 0.5f; // Offset to position the inventory above the head

    void Awake()
    {
        var canvas = inventory.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = Camera.main;
            canvas.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f); // Adjusted for VR size
        }

        if (!inventory.GetComponent<TrackedDeviceGraphicRaycaster>())
        {
            inventory.AddComponent<TrackedDeviceGraphicRaycaster>();
        }
    }

    void Start()
    {
        inventory.SetActive(isInventoryEnabled);
    }

    void Update()
    {
        // Toggle inventory visibility with the Input Action
        if (showButton.action.WasPressedThisFrame())
        {
            isInventoryEnabled = !isInventoryEnabled;
            inventory.SetActive(isInventoryEnabled);
        }

        // Position the inventory like MainManager if active
        if (inventory.activeSelf)
        {
            inventory.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance + new Vector3(0, verticalOffset, 0);
            inventory.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            inventory.transform.forward *= -1;
        }
    }
}