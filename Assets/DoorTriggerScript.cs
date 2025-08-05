using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DoorTriggerScript : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public GameObject fadeScreen;
    public Image fadeImage;
    public float fadeDuration = 2.0f;
    public InputActionProperty moveInput;
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(InitialiseChangeScene());
    }
    IEnumerator InitialiseChangeScene()
    {
        if (SceneManager.GetSceneByName("Start").name == "Start")
        {
            anim1.SetBool("Open", true);
            anim2.SetBool("Open", true);
            moveInput.action.Disable();
            yield return new WaitForSeconds(0.05f);
            fadeScreen.SetActive(true);
            yield return StartCoroutine(Fade(1));
            SceneManager.LoadScene("PickGroceries");
            Debug.Log("Change to grocery picking stage");
            yield return null;
        }
        else if (SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
        {
            bool isObjectiveCompleted = true;

            foreach(var item in ListTracker.instance.objectiveList)
            {
                if(item.scannedAmount < item.quantity)
                {
                    isObjectiveCompleted = false;
                    Debug.Log("Quest not complete");
                    break;
                }
            }
            if (isObjectiveCompleted)
            {
                Debug.Log("Quest complete");
                moveInput.action.Disable();
                yield return new WaitForSeconds(0.05f);
                fadeScreen.SetActive(true);
                yield return StartCoroutine(Fade(1));
                SceneManager.LoadScene("Packing");
                Debug.Log("Change to packing stage");
                yield return null;
            }
        }

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
    private void OnTriggerExit(Collider other)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
