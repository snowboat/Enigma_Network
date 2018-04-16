using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class MovableObject : MonoBehaviour {
    private TransformGesture gesture;
    private Transformer transformer;
    private Rigidbody rb;

    private void OnEnable()
    {
        // The gesture
        gesture = GetComponent<TransformGesture>();
        // Transformer component actually MOVES the object
        transformer = GetComponent<Transformer>();
        rb = GetComponent<Rigidbody>();

        transformer.enabled = false;
        rb.isKinematic = false;
        
    }

    private void OnDisable()
    {
    }

    private void transformStartedHandler(object sender, Event e)
    {
        // When movement starts we need to tell physics that now WE are moving this object manually
        rb.isKinematic = true;
        transformer.enabled = true;
    }

    private void transformCompletedHandler(object sender, Event e)
    {
        transformer.enabled = false;
        rb.isKinematic = false;
        rb.WakeUp();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
