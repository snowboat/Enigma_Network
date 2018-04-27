using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class Wheels : MonoBehaviour {
    private PinnedTransformGesture gesture;
    private Transformer transformer;
    private AudioSource sound;
    public float minAngle = 40;
    public float maxAngle = 320;

    public float rotateAngle;

    private void OnEnable()
    {
        gesture = GetComponent<PinnedTransformGesture>();
        gesture.Transformed += transformedHandler;

        rotateAngle = transform.localEulerAngles.y;
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

        if (rotateAngle <= minAngle || rotateAngle >= maxAngle)
            this.GetComponent<Transformer>().enabled = false;
        
        if (rotateAngle <= minAngle && gesture.DeltaRotation < 0 && !this.GetComponent<Transformer>().enabled)
            this.GetComponent<Transformer>().enabled = true;
        if (rotateAngle >= minAngle && gesture.DeltaRotation > 0 && !this.GetComponent<Transformer>().enabled)
            this.GetComponent<Transformer>().enabled = true;

            rotateAngle = Mathf.Min(maxAngle, Mathf.Max(minAngle, rotateAngle));

        playWheelSound();
    }

    void playWheelSound()
    {
        if (!sound.isPlaying)
            sound.Play();
    }

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	}
}
