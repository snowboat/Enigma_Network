using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class tapableButton : MonoBehaviour {
    public Vector3 buttonTransform;

    private TapGesture gesture;
    Vector3 startPosition, endPosition;
    public bool isPressed;
    AudioSource sound;

    // Use this for initialization
    void Start() {
		startPosition = this.transform.localEulerAngles;
        endPosition = new Vector3(startPosition.x + buttonTransform.x, startPosition.y + buttonTransform.y, startPosition.z + buttonTransform.z);
        isPressed = false;

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

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
            //this.transform.position = startPosition;
			this.transform.localEulerAngles = startPosition;
            isPressed = false;
        }

        Debug.Log("receive local tap button");
        playButtonSound();
    }

    public void RemoteToggleButton()
    {
		if (!isPressed)
		{
			this.transform.localEulerAngles = endPosition;
			isPressed = true;
		}
		else
		{
			//this.transform.position = startPosition;
			this.transform.localEulerAngles = startPosition;
			isPressed = false;
		}

        playButtonSound();
    }

    private void playButtonSound()
    {
        // Check tag in case of having multiple types of buttons
        if (this.gameObject.tag == "RadioButton")
            sound.Play();
    }
}
