using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public InputActionProperty showButton;
    private bool isPaused;
    public Transform head;
    public float spawnDistance = 2;
    public GameObject spawnPoint;
    public XROrigin Player;
    public GameObject fadeScreen;
    public Image fadeImage;
    public float fadeDuration = 4.0f;

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        togglePause();
    }

    public void restartPlaythrough()
    {
        Debug.Log("Restart");
    }

    public void teleportToStart()
    {
        mainMenu.SetActive(false);
        StartCoroutine(FadeTeleport(Player.transform, spawnPoint.transform.position));
    }
    public void quitGame()
    {
        if(SceneManager.GetSceneByName("Start").name == "Start")
        {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            SceneManager.LoadScene("Start");
        }
    }

    private void togglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }


    IEnumerator FadeTeleport(Transform player, Vector3 spawnPoint)
    {
        yield return StartCoroutine(initialiseFade());

        yield return StartCoroutine(Fade(1));

       //player.position = spawnPoint;
        yield return StartCoroutine(teleportPlayer(player, spawnPoint));

        yield return StartCoroutine(Fade(0));

        yield return StartCoroutine(closeFade());
    }
    IEnumerator initialiseFade()
    {
        fadeScreen.SetActive(true);
        yield return null;
    }

    IEnumerator teleportPlayer(Transform player, Vector3 spawnPoint)
    {
        player.position = spawnPoint;
        yield return null;
    }

    IEnumerator closeFade()
    {
        fadeScreen.SetActive(false);
        yield return null;
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
    void Start()
    {
        if(SceneManager.GetSceneByName("Start").name != "Start")
        {
            mainMenu.SetActive(false);
        }
        fadeScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetSceneByName("Start").name == "Start")
        {
            if (fadeScreen.activeSelf)
            {
                fadeScreen.transform.position = head.position + head.forward * 0.5f;
                fadeScreen.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
                fadeScreen.transform.forward *= -1;
            }
            if (mainMenu.activeSelf)
            {
                mainMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized;
                mainMenu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
                mainMenu.transform.forward *= -1;
            }

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
            if (fadeScreen.activeSelf)
            {
                fadeScreen.transform.position = head.position + head.forward * 0.5f;
                fadeScreen.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
                fadeScreen.transform.forward *= -1;
            }
        }
        
    }
}
