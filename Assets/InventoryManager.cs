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


    // Update is called once per frame
    void Update()
    {

        inventory.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        inventory.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));

    }
}
