using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == other.gameObject.tag)
        {
            Debug.Log("Can put Object");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.ToString() == "Player")
        {
            //Nothing happens
        }
        else 
        {
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
