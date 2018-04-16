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
    public float rotateSpeed = 100.0f;

    public float rotateAngle;

    private void OnEnable()
    {
        gesture = GetComponent<PinnedTransformGesture>();
        gesture.Transformed += transformedHandler;

        rotateAngle = transform.localEulerAngles.y;
    }

    private void OnDisable()
    {
        gesture.Transformed -= transformedHandler;
    }

    private void transformedHandler(object sender, System.EventArgs e)
    {
       // Debug.Log(gesture.DeltaRotation);
        if (gesture.DeltaRotation < 0)
            rotateAngle += Time.deltaTime * rotateSpeed;
        else
            rotateAngle -= Time.deltaTime * rotateAngle;

        rotateAngle = Mathf.Min(maxAngle, Mathf.Max(minAngle, rotateAngle));
		transform.localEulerAngles = new Vector3(0, rotateAngle, 0);


        playWheelSound();
    }

    void playWheelSound()
    {
        if (this.tag == "ObjectWheel" && !sound.isPlaying)
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
