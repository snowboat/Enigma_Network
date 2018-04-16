using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;

public class PhoneWheel : MonoBehaviour {
    private PinnedTransformGesture gesture;
    private Transformer transformer;

    private bool gestureChanged = false;
    private bool resetWheel = false;

    private bool isPhoneActived = true;

    private float wheelAngleY, rotateAngle;
    bool addedNumber = false;
    private string number = "";

    private Vector2 curDir, pos, wheelCenter, dialDir, startDir;
    private float dotProduct;

    private float stopTime = -1;

    public string targetNumber = "000000";
    public float waitCallTime = 5;
    public float rotateSpeed = 100.0f;
    public float resetSpeed = 30.0f;
    public Vector2 presetDialDir;

    AudioSource phone;
    public AudioClip dialing;
    public AudioClip wrongNumber;
    public AudioClip phone1;
    public AudioClip phoneRing;

    // Use this for initialization
    void Start () {
        phone = GetComponent<AudioSource>();

        wheelCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        dialDir = presetDialDir - wheelCenter;
        dialDir.Normalize();
      //  dialDir.y *= -1;
	}

    private void FixedUpdate()
    {
        pos = gesture.ScreenPosition;
        curDir = pos - wheelCenter;
        curDir.Normalize();
        dotProduct = Vector2.Dot(curDir, dialDir);

        //Debug.Log(dotProduct);

        addDigit();
    }

    // Update is called once per frame
    void Update() {
      //  Debug.Log(gesture.ScreenPosition);

        if (gesture.State == TouchScript.Gestures.Gesture.GestureState.Changed && !gestureChanged)
        {
            gestureChanged = true;

            startDir = gesture.ScreenPosition - wheelCenter;
            startDir.Normalize();

            string output = startDir.x.ToString("0.000") + ", " + startDir.y.ToString("0.000");
            Debug.Log(output);
            //Debug.Log(startDir);
        }

        if (gestureChanged && gesture.State == TouchScript.Gestures.Gesture.GestureState.Idle)
        {
            resetWheel = true;
            gestureChanged = false;

            resetSpeed = Mathf.Max(100.0f, rotateAngle * 2f);
            wheelAngleY = rotateAngle;
            StartCoroutine(ResetWheel());
            resetWheel = false;
            addedNumber = false;
            rotateAngle = 0.0f;

            stopTime = Time.realtimeSinceStartup;
            startDir = new Vector2(-5, -5);
        }


        if (number.Length > 0 && stopTime > 0 && Time.realtimeSinceStartup - stopTime > waitCallTime && gesture.State == TouchScript.Gestures.Gesture.GestureState.Idle)
        {
            Debug.Log("Times up. Check number");
            if (number == targetNumber && isPhoneActived)
            {
                Debug.Log("Got the correct number");
                CallNumber(number);
            }
            else
            {
                Debug.Log("Number is wrong");
                CallNumber("xxx");
            }

            stopTime = -1;
            number = "";
        }
	}

    private void DialPhone()
    {
        phone.clip = dialing;
        if (!phone.isPlaying)
            phone.Play();
    }

    public void CallNumber(string callNumber)
    {
        if (callNumber == "xxx")
            phone.clip = wrongNumber;
        else if (callNumber == targetNumber)
            phone.clip = phone1;

        phone.Play();
    }

    private bool addDigit()
    {
        if (!gestureChanged)
            return false;

        if (pos.x <= 0 || pos.y <= 0 || pos.x < Screen.width / 2 || pos.y > Screen.height / 2)
            return false;

        if (dotProduct > 0.988 && dotProduct < 0.998)
        {
           // Debug.Log(dotProduct);
            if (!addedNumber)
            {
                addedNumber = true;
                if (startDir.x <= 1.1f && startDir.x >= 0.78f && startDir.y >= 0.38f && startDir.y <= 0.61f)
                    number += "1";
                else if (startDir.x <= 0.61f && startDir.x >= 0.38f && startDir.y >= 0.78f && startDir.y <= 0.92f)
                    number += "2";
                else if (startDir.x <= 0.3f && startDir.x >= -0.21f && startDir.y >= 0.9f && startDir.y <= 1.1f)
                    number += "3";
                else if (startDir.x <= -0.39f && startDir.x >= -0.61f && startDir.y >= 0.79f && startDir.y <= 1.1f)
                    number += "4";
                else if (startDir.x <= -0.75f && startDir.x >= -1.0f && startDir.y >= 0.35f && startDir.y <= 0.65f)
                    number += "5";
                else if (startDir.x <= -0.9f && startDir.x >= -1.001f && startDir.y >= -0.21f && startDir.y <= 0.2f)
                    number += "6";
                else if (startDir.x <= -0.77f && startDir.x >= -0.92f && startDir.y >= -0.63f && startDir.y <= -0.4f)
                    number += "7";
                else if (startDir.x <= -0.36f && startDir.x >= -0.66f && startDir.y >= -0.95f && startDir.y <= -0.76f)
                    number += "8";
                else if (startDir.x <= 0.15f && startDir.x >= -0.2f && startDir.y >= -1.1f && startDir.y <= -0.95f)
                    number += "9";
                else if (startDir.x <= 0.57f && startDir.x >= 0.3f && startDir.y >= -0.95f && startDir.y <= -0.8f)
                    number += "0";
                else {
                    
                }

                Debug.Log("Update number: " + number);
            }
            return true;
        }  

        return false;
    }

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
        if (gesture.DeltaRotation > 0 && !addedNumber)
        {
            rotateAngle += Time.deltaTime * rotateSpeed;
            DialPhone();
        }

        //rotateAngle = Mathf.Min(maxAngle, Mathf.Max(minAngle, rotateAngle));
        //rotateAngle = rotateAngle % 360;
        transform.localEulerAngles = new Vector3(0, rotateAngle, 0);
    }

    private IEnumerator ResetWheel()
    {
       // Debug.Log(this.transform.localEulerAngles.y);
        while (wheelAngleY > 0)
        {
            wheelAngleY -= Time.deltaTime * resetSpeed;
            wheelAngleY = Mathf.Max(0, wheelAngleY);
            this.transform.localEulerAngles = new Vector3(0.0f, wheelAngleY, 0.0f);
			DialPhone ();
            yield return new WaitForEndOfFrame();
        }
    }

    public void ActivePhone()
    {
        isPhoneActived = true;
    }

    public void DisablePhone()
    {
        isPhoneActived = false;
    }

    public void SetTargetNumber(string newTarget)
    {
        targetNumber = newTarget;
    }

    public void PhoneRing()
    {
        phone.clip = phoneRing;
        phone.Play();
    }
}
