using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public GameObject questOverlay;
    public Transform head;
    public TMP_Text logText;

    public void UpdateLog()
    {
        Debug.Log("UpdateLog called");
        logText.text = "";
        foreach (var item in ListTracker.instance.objectiveList)
        {
            string checkMark;
            if(item.scannedAmount != item.quantity)
            {
                checkMark = "<color=red>✗</color>";
            }
            else
            {
                checkMark = "<color=green>✓</color>";
            }
            logText.text += $"{checkMark} {item.itemName} ({item.scannedAmount}/{item.quantity})\n";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
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
        /*if (questOverlay.activeSelf)
        {
            // Offset position in camera (head) space
            Vector3 offset = head.right * 0.3f + head.up * 0.25f + head.forward * 0.5f;

            // Set position relative to head/camera
            questOverlay.transform.position = head.position + offset;

            // Make the overlay face the camera
            questOverlay.transform.rotation = Quaternion.LookRotation(questOverlay.transform.position - head.position);
        }*/
        if (questOverlay.activeSelf)
        {
            questOverlay.transform.position = head.position + head.forward * 0.5f;
            questOverlay.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            questOverlay.transform.forward *= -1;
        }
    }
}
