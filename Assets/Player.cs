using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            Debug.Log("Inventory Saved");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            inventory.Load();
            Debug.Log("Inventory Loaded");
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
