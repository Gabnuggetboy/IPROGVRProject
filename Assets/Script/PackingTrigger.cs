using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PackingTrigger : MonoBehaviour
{
    public AudioSource correctSelection;
    public AudioSource wrongSelection;
    public UnityEvent IncrementScore;
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == other.gameObject.tag)
        {
            Debug.Log("Can put Object");
            correctSelection.Play();
            IncrementScore.Invoke();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.ToString() == "Player")
        {
            //Nothing happens :)
        }
        else 
        {
            wrongSelection.Play();
            Debug.Log("Cannot put object");
        }
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
