using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerScript : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    private void OnTriggerEnter(Collider other)
    {
        
        if(SceneManager.GetSceneByName("Start").name == "Start")
        {
            anim1.SetBool("Open", true);
            anim2.SetBool("Open", true);
            StartCoroutine(InitialisePickScene());
        }
        else if(SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
        {
            StartCoroutine(InitialisePackScene());
        }
    }
    IEnumerator InitialisePickScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("PickGroceries");
        Debug.Log("Change to grocery picking stage");
        yield return null;
    }
    IEnumerator InitialisePackScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Packing");
        Debug.Log("Change to packing stage");
        yield return null;
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
