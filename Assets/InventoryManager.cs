using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public InputActionProperty showButton;
    public TMP_Text message;
    public Transform head;
    public float spawnDistance = 2;

    public bool isInventoryEnabled = false; 


    // Update is called once per frame
    void Update()
    {

        if (!isInventoryEnabled)
        {
            inventory.SetActive(false); // Hide until enabled
            return;
        }

        inventory.SetActive(true);

        Vector3 forward = new Vector3(head.forward.x, 0, head.forward.z).normalized;
        Vector3 position = head.position + forward * spawnDistance;
        position.y -= 0.3f; 

        inventory.transform.position = position;
        inventory.transform.LookAt(new Vector3(head.position.x, inventory.transform.position.y, head.position.z));
        inventory.transform.forward *= -1;

    }
}
