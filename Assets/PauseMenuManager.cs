using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject questOverlay;
    public TMP_Text logText;
    public InputActionProperty showButton;
    public InputActionProperty questButton;
    public InputActionProperty moveInput;
    public Transform head;
    public float spawnDistance = 2;
    private bool isPaused;
    public GameObject fadeScreen;
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    public void UpdateLog()
    {
        Debug.Log("UpdateLog called");
        logText.text = "";
        foreach (var item in ListTracker.instance.objectiveList)
        {
            string checkMark;
            if (item.scannedAmount != item.quantity)
            {
                checkMark = "<color=red>☐</color>";
            }
            else
            {
                checkMark = "<color=green>☑</color>";
            }
            logText.text += $"{checkMark} {item.itemName} ({item.scannedAmount}/{item.quantity})\n";
        }
    }

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
        togglePause();
        StartCoroutine(ReturnToStart());
    }
    // Start is called before the first frame update

    private void togglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    IEnumerator ReturnToStart()
    {
        yield return StartCoroutine(initialiseFade());
        yield return StartCoroutine(Fade(1));
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
        yield return StartCoroutine(Fade(0));
        yield return StartCoroutine(closeFade());
    }

    IEnumerator initialiseFade()
    {
        fadeScreen.SetActive(true);
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
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(Fade(0));
        yield return StartCoroutine(closeFade());

    }
    private void Awake()
    {
        showButton.action.Disable();
        moveInput.action.Disable();
    }
    void Start()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        //questOverlay.SetActive(false);
        fadeScreen.SetActive(true);
        StartCoroutine(FadeOut());
        showButton.action.Enable();
        moveInput.action.Enable();
        Debug.Log($"Objective count: {ListTracker.instance?.objectiveList?.Count}");
        if (SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
        {
            questOverlay.SetActive(true);
            UpdateLog();
        }
    }

    // Update is called once per frame
    void Update()
    {
    if (SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
    {
        if (questButton.action.WasPressedThisFrame())
            {
                questOverlay.SetActive(!questOverlay.activeSelf);
            }
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
    if (fadeScreen.activeSelf)
    {
        fadeScreen.transform.position = head.position + head.forward * 0.5f;
        fadeScreen.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        fadeScreen.transform.forward *= -1;
    }
    if (questOverlay.activeSelf)
      {
            questOverlay.transform.position = head.position + head.forward * 0.5f;
            questOverlay.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            questOverlay.transform.forward *= -1;
        }
    }
}

