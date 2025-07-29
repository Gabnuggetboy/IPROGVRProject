using UnityEngine;
using Unity.XR.CoreUtils;

public class MainManager : MonoBehaviour
{
    public GameObject mainMenu;
    public Transform head;
    public float spawnDistance = 1f;
    public GameObject spawnPoint;
    public XROrigin Player;
    public bool isMainMenuOn = true;
    public InventoryManager inventoryManager;

    void Start()
    {
        mainMenu.SetActive(isMainMenuOn);
    }

    public void teleportToStart()
    {
        Player.transform.position = spawnPoint.transform.position;
        mainMenu.SetActive(false);
        isMainMenuOn = false;
        // Enable the inventory
        if (inventoryManager != null)
        {
            inventoryManager.isInventoryEnabled = true;
            inventoryManager.inventory.SetActive(true);
        }
    }

    public void quitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void Update()
    {
        if (isMainMenuOn && mainMenu.activeSelf)
        {
            Vector3 forward = new Vector3(head.forward.x, 0, head.forward.z).normalized;
            mainMenu.transform.position = head.position + forward * spawnDistance;
            mainMenu.transform.LookAt(new Vector3(head.position.x, mainMenu.transform.position.y, head.position.z));
            mainMenu.transform.forward *= -1;
        }
    }
}