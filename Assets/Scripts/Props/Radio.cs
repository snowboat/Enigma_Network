using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    AudioSource radio;
    // The ambient sound 
    public AudioClip radio_static;
    // Audio contains important informtaion
    public AudioClip real_radio;
    // Audios contain not important information
    public AudioClip fake_radio01;
    public AudioClip fake_radio02;
    public AudioClip fake_radio03;

    // Place to carry the current three sound clips that the radio is using
    public AudioClip radio01;
    public AudioClip radio02;
    public AudioClip radio03;

    // Max and min position of the redline
    public float channelPosMin, channelPosMax;
    private float angleDis, channelDis, minAngle, channelRatio, channelX;

    // Red line
    GameObject channelDisplay;
    // Radio's wheel
    GameObject wheel;
    // Radio's switch
    GameObject button;

    float wheelRotation;
    float radioChannel;
    bool radioSwitched = false;
    bool isStaticRadio = true;

    void Start()
    {
        radio = GetComponent<AudioSource>();

        channelDisplay = GameObject.FindGameObjectWithTag("RadioChannel");

        wheel = GameObject.FindGameObjectWithTag("RadioWheel");
        angleDis = wheel.GetComponent<Wheels>().maxAngle - wheel.GetComponent<Wheels>().minAngle;
        minAngle = wheel.GetComponent<Wheels>().minAngle;
        channelDis = channelPosMax - channelPosMin;

        // Initialize the red line's position
        channelDisplay.transform.localPosition = new Vector3(0.86463f, 0.01075f, channelPosMax);

        button = GameObject.FindGameObjectWithTag("RadioButton");

        radio01 = fake_radio01;
        radio02 = fake_radio02;
        radio03 = fake_radio03;
    }

    private void FixedUpdate()
    {
        wheelRotation = wheel.GetComponent<Wheels>().rotateAngle;

        // Use ratio to check whether the readio is at a target frequency
        channelRatio = (wheelRotation - minAngle) / angleDis;

        // Red line's position
        channelX = channelPosMax - channelDis * channelRatio;
        channelDisplay.transform.localPosition = new Vector3(0.86463f, 0.01075f, channelX);

        switchChannel();
    }
    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<tapableButton>().isPressed)
        {
            if (!radio.isPlaying)
                radio.Play();
        }
        else
        {
            radio.Stop();
        }
    }

    void switchChannel()
    {
        if (channelRatio < 0.25 && channelRatio > 0.1)
        {
            if (!radioSwitched)
            {
                radio.clip = radio01;
                radioSwitched = true;
                isStaticRadio = false;
            }
        }
        else if (channelRatio < 0.45 && channelRatio > 0.3)
        {
            if (!radioSwitched)
            {
                radio.clip = radio02;
                radioSwitched = true;
                isStaticRadio = false;
            }
        }
        else if (channelRatio < 0.8 && channelRatio > 0.65)
        {
            if (!radioSwitched)
            {
                radio.clip = radio03;
                radioSwitched = true;
                isStaticRadio = false;
            }
        }
        else
        {
            if (!isStaticRadio)
            {
                radio.clip = radio_static;
                isStaticRadio = true;
            }

            radioSwitched = false;
        }

        
    }

    // Turn radio on or off through the facilitator's App
    public void RemoteToggelRadio()
    {
        Debug.Log("receive access through outside");
        if (!radio.isPlaying)
        {
            button.GetComponent<tapableButton>().RemoteToggleButton();
            radio.Play();
        }
        else
        {
            button.GetComponent<tapableButton>().RemoteToggleButton();
            radio.Stop();
        }
    }

    // Set one of the frenquency carry the important radio message
    public void ActiveRadio(int id)
    {
        if (id == 1)
            radio01 = real_radio;
        else if (id == 2)
            radio02 = real_radio;
        else if (id == 3)
            radio03 = real_radio;

        switchChannel();
    }

    // Set all of the frequencies carry not important radio message
    public void DisableRadio()
    {
        radio01 = fake_radio01;
        radio02 = fake_radio02;
        radio03 = fake_radio03;

        switchChannel();
    }
}
