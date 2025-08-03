using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerScript : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("Open", true);
        StartCoroutine(InitialiseNextScene());
        
    }
    IEnumerator InitialiseNextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("PickGroceries");
        Debug.Log("Change to grocery picking stage");
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
