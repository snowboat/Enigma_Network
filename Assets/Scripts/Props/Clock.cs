using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {
    // UI text objects for updating the clock's display
    public Text dayText;
    public Text monthText;
    public Text hourText;
    public Text minText;

    // Values that display on the clock
    private int curSec, curMin, curHour, curDate;
    private string curMonth;

    private float startTime;
    // Which animation to play
    private int triggerAnimation = -1;
    // Sound for clock flip
    private AudioSource clockAudio;
    private Animator clockAnim;

    void Start() {
        startTime = Time.time;
        curSec = 0;
        curMin = int.Parse(minText.text);
        curHour = int.Parse(hourText.text);
        curDate = int.Parse(dayText.text);
        curMonth = monthText.text;

        internalSetTime(10, 55);

        clockAudio = GetComponent<AudioSource>();
        clockAnim = GetComponent<Animator>();
        clockAnim.GetCurrentAnimatorStateInfo(0);
    }

    void Update() {
        // Update the clock's time
        if (Time.time - startTime > 1.0f)
        {
            curSec++;
            // Update Minute
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
                {
                    triggerAnimation = 1;
                }
            }
            internalSetTime(curHour, curMin);
        }

        // There is an animation need to display
        if (triggerAnimation > 0)
            playAnimation();
    }

    // Update the clock's display
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
        // No animation to play, return
        if (triggerAnimation <= 0)
            return;

        if (triggerAnimation == 1)
            clockAnim.SetTrigger("flipMin");
        else if (triggerAnimation == 2)
            clockAnim.SetTrigger("flipHr");
        else if (triggerAnimation == 3)
            clockAnim.SetTrigger("flipDay");
        else if (triggerAnimation == 4)
            clockAnim.SetTrigger("flipMon");

        // Play the flip sound
        clockAudio.Play();

        triggerAnimation = -1;
    }

    // For the local time update
    private void internalSetTime(int hour, int min)
    {
        startTime = Time.time;
        curHour = hour;
        curMin = min;

        // Call the updateText() after 3 seconds' wait
        // To align the text change with animation
        Invoke("updateText", 3);
    }

    // Functions for time change that requested by facilitator
    public void SetTime(int hour, int min)
    {
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
