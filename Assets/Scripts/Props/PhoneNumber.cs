using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class PhoneNumber : MonoBehaviour {
    private PressGesture gesture;
    private PhoneWheel phone;
       
    // The digit of this game object
    public int thisNumber;

    private void Start()
    {
        phone = GameObject.FindGameObjectWithTag("PhoneWheel").GetComponent<PhoneWheel>();
    }

    private void OnEnable()
    {
        gesture = GetComponent<PressGesture>();
        gesture.Pressed += pressedHandler;
    }

    private void OnDisable()
    {
        gesture.Pressed -= pressedHandler;
    }

    private void pressedHandler(object sender, System.EventArgs e)
    {
        phone.curNumber = thisNumber;
        phone.curNumberObject = this.gameObject;
    }

    // Reach the phone stop
    private void OnTriggerEnter(Collider other)
    {
        // Check whether the colliding object is the phonestop
        // and whether this game object is the one that player is pressing on
        if (other.tag == "PhoneStop" && phone.curNumberObject == this.gameObject)
        {
            phone.reachStop = true;
        }
    }
}
