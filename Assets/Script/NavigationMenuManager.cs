using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class NavigationMenuManager : MonoBehaviour
{
    public GameObject helpMenu;
    public GameObject navigationMarker;
    public InputActionProperty helpButton;
    public Transform head;
    public float spawnDistance = 2f;

    private bool isHelpVisible = false;

    void Start()
    {
        helpMenu.SetActive(false);
        navigationMarker.SetActive(false);

        StartCoroutine(ShowHelpMenuAfterDelay(3f));
    }

    void Update()
    {
        if (helpButton.action.WasPressedThisFrame())
        {
            ToggleHelpMenu();
        }

        if (helpMenu.activeSelf)
        {
            // Position UI in front of player
            helpMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            helpMenu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            helpMenu.transform.forward *= -1;
        }
    }

    private void ToggleHelpMenu()
    {
        isHelpVisible = !isHelpVisible;
        helpMenu.SetActive(isHelpVisible);
    }

    private IEnumerator ShowHelpMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        helpMenu.SetActive(true);
        isHelpVisible = true;
    }

    public void OnHelpYes()
    {
        helpMenu.SetActive(false);
        navigationMarker.SetActive(true);
    }

    public void OnHelpNo()
    {
        helpMenu.SetActive(false);
        navigationMarker.SetActive(false);
    }
}
