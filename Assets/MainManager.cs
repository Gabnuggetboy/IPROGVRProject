using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.XR.CoreUtils;



public class MainManager : MonoBehaviour
{
    public GameObject mainMenu;
    public Transform head;
    public float spawnDistance = 2;
    public GameObject spawnPoint;
    public XROrigin Player;
    public bool isMainMenuOn = false;

    public void teleportToStart()
    {
        Player.transform.position = spawnPoint.transform.position;
        mainMenu.SetActive(false);
    }
    public void quitGame()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void start()
    {
    
    }
   
    // Update is called once per frame
    void Update()
    {
        if (mainMenu.activeSelf)
        {
            mainMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized;
            mainMenu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            mainMenu.transform.forward *= -1;
        }
    }
}
