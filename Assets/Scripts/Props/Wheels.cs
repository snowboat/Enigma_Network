using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class Wheels : MonoBehaviour {
    private PinnedTransformGesture gesture;
    private Transformer transformer;
    private AudioSource sound;

    // Max and min angle the radio's wheel can turn
    public float minAngle = 40;
    public float maxAngle = 320;
    // Wheel's current rotation
    public float rotateAngle;

    private void OnEnable()
    {
        gesture = GetComponent<PinnedTransformGesture>();
        gesture.Transformed += transformedHandler;

        rotateAngle = transform.localEulerAngles.y;
        // Set the wheel's start transformation within angle range
        if (rotateAngle < minAngle)
            transform.localEulerAngles = new Vector3(0, minAngle, 0);
        else if (rotateAngle > maxAngle)
            transform.localEulerAngles = new Vector3(0, maxAngle, 0);

        rotateAngle = transform.localEulerAngles.y;
    }

    private void OnDisable()
    {
        gesture.Transformed -= transformedHandler;
    }

    private void transformedHandler(object sender, System.EventArgs e)
    {
        rotateAngle = transform.localEulerAngles.y;

        // Force the wheel to stop at the min or max rotaion
        if (rotateAngle <= minAngle || rotateAngle >= maxAngle)
            this.GetComponent<Transformer>().enabled = false;
        
        if (rotateAngle <= minAngle && gesture.DeltaRotation < 0 && !this.GetComponent<Transformer>().enabled)
            this.GetComponent<Transformer>().enabled = true;
        if (rotateAngle >= minAngle && gesture.DeltaRotation > 0 && !this.GetComponent<Transformer>().enabled)
            this.GetComponent<Transformer>().enabled = true;

        // Update the rotateAngle to prevent it from exceeding the angle range
        rotateAngle = Mathf.Min(maxAngle, Mathf.Max(minAngle, rotateAngle));

        playWheelSound();
    }

    void playWheelSound()
    {
        if (!sound.isPlaying)
            sound.Play();
    }

    void Start () {
        sound = GetComponent<AudioSource>();
    }
	
	void Update () {
	}
}
