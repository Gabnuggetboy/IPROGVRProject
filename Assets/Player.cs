using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public XRBaseController rightController;
    private XRBaseInteractor rightInteractor;

    private void Awake()
    {
        rightInteractor = rightController.GetComponent<XRBaseInteractor>();
        if (rightInteractor != null)
        {
            rightInteractor.selectEntered.AddListener(OnSelectEnter);
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        var item = args.interactableObject.transform.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(item.gameObject);
        }
    }

    private void Update()
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

    private void OnDestroy()
    {
        if (rightInteractor != null)
        {
            rightInteractor.selectEntered.RemoveListener(OnSelectEnter);
        }
    }
}