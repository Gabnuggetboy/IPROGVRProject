using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 2;
    private bool isPaused;

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        togglePause();
    }
    public void restartPlaythrough()
    {
        Debug.Log("Restart");
    }
    public void quitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Start is called before the first frame update

    private void togglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenu.activeSelf)
        {
            //Nothing happens
        }
        else
        {
            if (showButton.action.WasPressedThisFrame())
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                togglePause();
            }
            if (pauseMenu.activeSelf)
            {
                pauseMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized;
                pauseMenu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
                pauseMenu.transform.forward *= -1;
            }
        }
       
    }
}
