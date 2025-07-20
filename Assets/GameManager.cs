using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;
    public TMP_Text message;
    public Transform head;
    public float spawnDistance = 2; 


    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }

        if (menu.activeSelf)
        {
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            menu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            menu.transform.forward *= -1;

            message.text = "GameMenu 2";
        }
    }
}
