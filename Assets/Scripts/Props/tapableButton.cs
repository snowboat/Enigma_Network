using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class tapableButton : MonoBehaviour {
    // Transforms for button's on/off status
    public Vector3 buttonTransform;

    private TapGesture gesture;
    Vector3 startPosition, endPosition;
    public bool isPressed;
    AudioSource sound;

    void Start() {
        // Transform for off status
		startPosition = this.transform.localEulerAngles;
        // For on status
        endPosition = new Vector3(startPosition.x + buttonTransform.x, startPosition.y + buttonTransform.y, startPosition.z + buttonTransform.z);
        isPressed = false;

        sound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        gesture = GetComponent<TapGesture>();
        gesture.Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        gesture.Tapped -= tappedHandler;
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        if (!isPressed)
        {
			this.transform.localEulerAngles = endPosition;
            isPressed = true;
        }
        else
        {
			this.transform.localEulerAngles = startPosition;
            isPressed = false;
        }

        Debug.Log("receive local tap button");
        playButtonSound();
    }

    // Turn on/off the radio through facilitator's App
    public void RemoteToggleButton()
    {
		if (!isPressed)
		{
			this.transform.localEulerAngles = endPosition;
			isPressed = true;
		}
		else
		{
			this.transform.localEulerAngles = startPosition;
			isPressed = false;
		}

        playButtonSound();
    }

    // Sound effect for tapping on buttion
    private void playButtonSound()
    {
        if (this.gameObject.tag == "RadioButton")
            sound.Play();
    }
}
