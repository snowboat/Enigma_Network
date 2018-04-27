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
    public float resetSpeed = 30.0f;

    AudioSource phone;
    public AudioClip dialing;
    public AudioClip wrongNumber;
    public AudioClip phone1;
    public AudioClip phoneRing;

    public int curNumber = -1;
    public GameObject curNumberObject;
    public bool reachStop = false;

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;

        phone = GetComponent<AudioSource>();
	}


    // Update is called once per frame
    void Update() {

        addDigit();

        if (gesture.State == TouchScript.Gestures.Gesture.GestureState.Changed && !gestureChanged)
        {
            gestureChanged = true;
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
            reachStop = false;
            rotateAngle = 0.0f;
            curNumber = -1;

            this.GetComponent<Transformer>().enabled = true;

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

        if (reachStop)
        {
            // Debug.Log(dotProduct);
            this.GetComponent<Transformer>().enabled = false;
            if (!addedNumber && curNumber >= 0)
            {
                addedNumber = true;
                number += curNumber;
                curNumber = -1;
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
        if (gesture.DeltaRotation < 0)
            this.GetComponent<Transformer>().enabled = false;
        else if (!reachStop)
            this.GetComponent<Transformer>().enabled = true;

        if (gesture.DeltaRotation > 0 && !addedNumber)
        {
           // rotateAngle += Time.deltaTime * rotateSpeed;
            rotateAngle = transform.localEulerAngles.y;
            DialPhone();
        }

        //rotateAngle = Mathf.Min(maxAngle, Mathf.Max(minAngle, rotateAngle));
        //rotateAngle = rotateAngle % 360;
        //transform.localEulerAngles = new Vector3(0, rotateAngle, 0);
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
