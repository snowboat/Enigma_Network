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
    private int triggerAnimation = -1;
    private AudioSource clockAudio;
    private Animator clockAnim;

    // Use this for initialization
    void Start() {
        startTime = Time.time;
        curSec = 58;
        curMin = int.Parse(minText.text);
        curHour = int.Parse(hourText.text);
        curDate = int.Parse(dayText.text);
        curMonth = monthText.text;

        internalSetTime(10, 55);

        clockAudio = GetComponent<AudioSource>();
        clockAnim = GetComponent<Animator>();
        clockAnim.GetCurrentAnimatorStateInfo(0);
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

                if (curMin >= 60)
                {
                    curHour += (curMin / 60);
                    curMin = curMin % 60;

                    triggerAnimation = 2;
                }
                else
                    triggerAnimation = 1;
            }
            internalSetTime(curHour, curMin);
        }

        if (triggerAnimation > 0)
            playAnimation();
    }

    private void updateText()
    {
        if (curHour < 10)
            hourText.text = "0" + curHour.ToString();
        else
            hourText.text = curHour.ToString();
        if (curMin < 10)
            minText.text = "0" + curMin.ToString();
        else
            minText.text = curMin.ToString();
        if (curDate < 10)
            dayText.text = "0" + curDate.ToString();
        else
            dayText.text = curDate.ToString();

        monthText.text = curMonth;
    }

    private void playAnimation()
    {
        if (triggerAnimation <= 0)
            return;

        Debug.Log(triggerAnimation);
        if (triggerAnimation == 1)
            clockAnim.SetTrigger("flipMin");
        else if (triggerAnimation == 2)
            clockAnim.SetTrigger("flipHr");
        else if (triggerAnimation == 3)
            clockAnim.SetTrigger("flipDay");
        else if (triggerAnimation == 4)
            clockAnim.SetTrigger("flipMon");

        clockAudio.Play();

        triggerAnimation = -1;
    }

    private void internalSetTime(int hour, int min)
    {
        startTime = Time.time;
        curHour = hour;
        curMin = min;

        Invoke("updateText", 3);
    }

    public void SetTime(int hour, int min)
    {
       // clockAnim.SetTrigger("flipHr");
        startTime = Time.time;
        curHour = hour;
        curMin = min;
        curSec = 0;

        if (triggerAnimation <= 0)
            triggerAnimation = 2;


        Invoke("updateText", 3);
    }

    public void SetMonth(string mon)
    {
        if (triggerAnimation <= 0)
            triggerAnimation = 4;

        curMonth = mon;
        Invoke("updateText", 3);
    }

    public void SetDate(int date)
    {
        if (triggerAnimation <= 0)
            triggerAnimation = 3;

        curDate = date;
        Invoke("updateText", 3);
    }
}
