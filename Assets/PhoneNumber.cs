using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class PhoneNumber : MonoBehaviour {
    private PressGesture gesture;
    private PhoneWheel phone;
    private GameObject phoneStop;

    public int thisNumber;

    private void Start()
    {
        phone = GameObject.FindGameObjectWithTag("PhoneWheel").GetComponent<PhoneWheel>();
        phoneStop = GameObject.FindGameObjectWithTag("PhoneStop");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PhoneStop" && phone.curNumberObject == this.gameObject)
        {
            phone.reachStop = true;
            Debug.Log(thisNumber);
        }
    }
}
