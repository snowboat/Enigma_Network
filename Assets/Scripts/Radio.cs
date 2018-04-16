using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    AudioSource radio;
    public AudioClip radio_static;
    public AudioClip radio01;
    public AudioClip radio02;
    public AudioClip radio03;

    private bool isRadioActived = true;

    public float channelPosMin, channelPosMax;
    private float angleDis, channelDis, minAngle, channelRatio, channelX;

    GameObject channelDisplay;
    GameObject wheel;
    GameObject button;
    float wheelRotation;
    float radioChannel;
    bool radioSwitched = false;
    bool isStaticRadio = true;
    Vector3 channelPos;

    // Use this for initialization
    void Start()
    {
        radio = GetComponent<AudioSource>();

        channelDisplay = GameObject.FindGameObjectWithTag("RadioChannel");
        channelPos = channelDisplay.transform.localPosition;

        wheel = GameObject.FindGameObjectWithTag("RadioWheel");
        angleDis = wheel.GetComponent<Wheels>().maxAngle - wheel.GetComponent<Wheels>().minAngle;
        minAngle = wheel.GetComponent<Wheels>().minAngle;
        channelDis = channelPosMax - channelPosMin;
      
       // Debug.Log(channelRatio);

        button = GameObject.FindGameObjectWithTag("RadioButton");
    }

    // Update is called once per frame
    void Update()
    {
        wheelRotation = wheel.GetComponent<Wheels>().rotateAngle;
        channelRatio = (wheelRotation - minAngle) / angleDis;

        channelX = channelPosMax - channelDis * channelRatio;
		channelDisplay.transform.localPosition = new Vector3(0.86463f, 0.01075f, channelX);

        if (channelRatio < 0.35 && channelRatio > 0.2)
        {
            if (!radioSwitched)
            {
                radio.clip = radio01;
                radioSwitched = true;
                isStaticRadio = false;
            }
        }
        else if (channelRatio < 0.6 && channelRatio > 0.45)
        {
            if (!radioSwitched)
            {
                radio.clip = radio02;
                radioSwitched = true;
                isStaticRadio = false;
            }
        }
        else if (channelRatio < 0.8 && channelRatio > 0.55)
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

    public void ActiveRadio()
    {
        isRadioActived = true;
    }

    public void DisableRadio()
    {
        isRadioActived = false;
    }
}
