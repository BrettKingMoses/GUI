using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerClockHardCode : MonoBehaviour
{
    public float timer;
    public string clockTime;
    public Text timeText;
    void Start()
    {
        Time.timeScale = 1;
    }

    void LateUpdate()
    {
        if (timer < 0)
        {
            timer = 0;
        }

    }
    void Update()
    {
        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer - mins * 60);

        clockTime = string.Format("{0:0}:{1:00}", mins, secs);
        timeText.text = "count: " + clockTime;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}