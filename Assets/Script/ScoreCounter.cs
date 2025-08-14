using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;
    public int score = 0;

    public void incrementScore()
    {
        score++;
    }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
