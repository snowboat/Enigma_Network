using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {
    public Text dayText;
    public Text monthText;
    public Text hourText;
    public Text minText;

    private int curSec, curMin, curHour, curDate;
    private string curMonth;

    private float startTime;
    private AudioSource clockAudio;

    // Use this for initialization
    void Start() {
        curSec = 0;
        curMin = int.Parse(minText.text);
        curHour = int.Parse(hourText.text);
        curDate = int.Parse(dayText.text);
        curMonth = monthText.text;

        SetTime(10, 59);

        clockAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - startTime > 1.0f)
        {
            curSec++;
            if (curSec >= 60)
            {
                curMin += (curSec / 60);
                curSec = curSec % 60;
                clockAudio.Play();
                if (curMin >= 60)
                {
                    curHour += (curMin / 60);
                    curMin = curMin % 60;
                    clockAudio.Play();
                }
            }
          //  Debug.Log(curSec);
            SetTime(curHour, curMin);
        }
    }

    public void SetTime(int hour, int min)
    {
        startTime = Time.time;
        curHour = hour;
        curMin = min;
        curSec = 0;

        if (hour < 10)
            hourText.text = "0" + curHour.ToString();
        else
            hourText.text = curHour.ToString();
        if (min < 10)
            minText.text = "0" + curMin.ToString();
        else
            minText.text = curMin.ToString();
    }

    public void SetMonth(string mon)
    {
        curMonth = mon;
        monthText.text = mon;
    }

    public void SetDate(int date)
    {
        curDate = date;
        if (date < 10)
            dayText.text = "0" + date.ToString();
        else
            dayText.text = date.ToString();
    }
}
