using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTracking : MonoBehaviour
{
    public static TimeTracking tracker;
    public float savedTimeEquipment = 0f;
    public float savedTimePicking = 0f;
    public float savedTimePacking = 0f;
    public float elapsedTimeEquipment = 0f;
    public float elapsedTimePicking = 0f;
    public float elapsedTimePacking = 0f;
    public bool isRunning = false;

    private void Awake()
    {
        if (tracker != null && tracker != this)
        {
            Destroy(this);
        }
        else
        {
            tracker = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("PickGroceries").name == "PickGroceries")
        {
            if (isRunning)
            {
                elapsedTimePicking += Time.deltaTime;
                //Debug.Log("Elapsed: " + elapsedTime.ToString("F2"));
            }
        }
        else if (SceneManager.GetSceneByName("Start").name == "Start")
        {
            if (isRunning)
            {
                elapsedTimeEquipment += Time.deltaTime;
                //Debug.Log("Elapsed: " + elapsedTime.ToString("F2"));
            }
        }
        else if (SceneManager.GetSceneByName("Picking").name == "Picking")
        {
            if (isRunning)
            {
                elapsedTimePacking += Time.deltaTime;
                //Debug.Log("Elapsed: " + elapsedTime.ToString("F2"));
            }
        }
        else
        {
            isRunning = false;
            Debug.Log("Error with time tracking");
        }

    }
}
