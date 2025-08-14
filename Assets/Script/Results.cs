using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Results : MonoBehaviour
{
    public GameObject finalDisplay;
    public bool gameCompleted = false;
    public TMP_Text timing1;
    public TMP_Text timing2;
    public TMP_Text timing3;
    public TMP_Text totalTiming;
    public TMP_Text packingScore;

    public void showResults()
    {
        Debug.Log("Showing results");
        finalDisplay.SetActive(true);
        float t1 = TimeTracking.tracker.elapsedTimeEquipment;
        float t2 = TimeTracking.tracker.elapsedTimePicking;
        float t3 = TimeTracking.tracker.elapsedTimePacking;
        float total = t1 + t2 + t3;
        timing1.text = $"Stage 1 Timing: {FormatTime(t1)}";
        timing2.text = $"Stage 2 Timing: {FormatTime(t2)}";
        timing3.text = $"Stage 3 Timing: {FormatTime(t3)}";
        totalTiming.text = $"Total Time taken: {FormatTime(total)}";
        int totalItems = 0;
        foreach (var item in ListTracker.instance.objectiveList)
        {
            totalItems += item.quantity;
        }
        packingScore.text = $"Your packing score is: {ScoreCounter.instance.score}/{totalItems}";
    }
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return $"{minutes:D2}:{seconds:D2}";
    }
    // Start is called before the first frame update
    void Start()
    {
        finalDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameCompleted)
        {
            showResults();
        }
    }
}
