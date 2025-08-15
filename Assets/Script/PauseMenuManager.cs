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
    public AudioSource button;
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
    public float slideDuration = 1.0f;
    private bool questVisible = false;
    public Vector3 startOffset;
    public Vector3 endOffset;

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
        button.Play();
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
    IEnumerator OpenQuest(bool show)
    {
        float elapsed = 0f;
        startOffset = show ? new Vector3(0, -0.5f, 0.5f) : new Vector3(0, 0f, 0.5f); ;
        endOffset = show ? new Vector3(0, 0f, 0.5f) : new Vector3(0, -0.5f, 0.5f);
        yield return StartCoroutine(PrepositionQuest());
        questOverlay.SetActive(true);
        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / slideDuration);

            questOverlay.transform.position = head.position + head.forward * endOffset.z
                                            + head.up * Mathf.Lerp(startOffset.y, endOffset.y, t)
                                            + head.right * Mathf.Lerp(startOffset.x, endOffset.x, t);

            questOverlay.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            questOverlay.transform.forward *= -1;

            yield return null;
        }
        questButton.action.Enable();

        if (!show)
            questOverlay.SetActive(false);
    }
    IEnumerator PrepositionQuest()
    {
        questOverlay.transform.position = head.position + head.forward * startOffset.z
                                  + head.up * startOffset.y
                                  + head.right * startOffset.x;
        questOverlay.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        questOverlay.transform.forward *= -1;


        yield return null;
    }

    IEnumerator ReturnToStart()
    {
        button.Play();
        yield return StartCoroutine(initialiseFade());
        yield return StartCoroutine(Fade(1));
        SceneManager.LoadScene("Start");
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
        showButton.action.Enable();
        moveInput.action.Enable();
        questButton.action.Enable();
        //TimeTracking.tracker.isRunning = true;
        Debug.Log($"Objective count: {ListTracker.instance?.objectiveList?.Count}");
        if (SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
        {
            UpdateLog();
        }

    }
    private void Awake()
    {
        showButton.action.Disable();
        moveInput.action.Disable();
        questButton.action.Disable();
    }
    void Start()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        questOverlay.SetActive(false);
        fadeScreen.SetActive(true);
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
        {
            if (questButton.action.WasPressedThisFrame())
            {
                questButton.action.Disable();
                StartCoroutine(OpenQuest(!questVisible));
                questVisible = !questVisible;
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