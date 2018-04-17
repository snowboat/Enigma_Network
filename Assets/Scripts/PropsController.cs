using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PropsController : NetworkBehaviour {
    private GameObject clock;
    private GameObject radio;
    private GameObject phone;

    public AudioClip[] audioInUse;

	// Send message to clock
	[ClientRpc]
	public void RpcSendTimeInfoToClock(string month, int date, int hour, int min) {
		Debug.Log("set the clock");
		clock = GameObject.FindGameObjectWithTag("Clock");
		if (clock != null) {
			clock.GetComponent<Clock>().SetMonth(month);
			clock.GetComponent<Clock>().SetDate(date);
			clock.GetComponent<Clock>().SetTime(hour, min);
		}
		else {
			Debug.Log("no radio");
		}
	}

	// Send message to phone
	[ClientRpc]
    public void RpcActivePhone() {
		Debug.Log("active phone");
		phone = GameObject.FindGameObjectWithTag("PhoneWheel");
		if (phone != null) {
			phone.GetComponent<PhoneWheel>().ActivePhone();
		}
		else {
			Debug.Log("no phone");
		}
	}

	[ClientRpc]
    public void RpcSetPhoneNumber(string number) {
		Debug.Log("set phone number");
		phone = GameObject.FindGameObjectWithTag("PhoneWheel");
		if (phone != null) {
			phone.GetComponent<PhoneWheel>().SetTargetNumber(number);
		}
		else {
			Debug.Log("no phone");
		}
	}

	[ClientRpc]
	public void RpcSetPhoneClip(int audioClipNumber)
    {
        Debug.Log("set phone clip");
		phone = GameObject.FindGameObjectWithTag("PhoneWheel");
		if (phone != null) {
			phone.GetComponent<AudioSource>().clip = audioInUse[audioClipNumber];
		}
		else {
			Debug.Log("no phone");
		}
    }

	[ClientRpc]
    public void RpcDisablePhone()
    {
        Debug.Log("disable phone");
		phone = GameObject.FindGameObjectWithTag("PhoneWheel");
		if (phone != null) {
			phone.GetComponent<PhoneWheel>().DisablePhone();
		}
		else {
			Debug.Log("no phone");
		}
    }

	[ClientRpc]
    public void RpcPhonePlayClip(int audioClipNumber)
    {
        Debug.Log("phone play clip: "+audioInUse[audioClipNumber].name);
		phone = GameObject.FindGameObjectWithTag("PhoneWheel");
		if (phone != null) {
			phone.GetComponent<AudioSource>().clip = audioInUse[audioClipNumber];
			phone.GetComponent<AudioSource>().Play();
		}
		else {
			Debug.Log("no phone");
		}
    }

	[ClientRpc]
    public void RpcPhoneRing()
    {
        Debug.Log("phone ring");
		phone = GameObject.FindGameObjectWithTag("PhoneWheel");
		if (phone != null) {
			phone.GetComponent<PhoneWheel>().PhoneRing();
		}
		else {
			Debug.Log("no phone");
		}
    }

	// Send message to radio
	[ClientRpc]
	public void RpcDisableRadio() {
		Debug.Log("disable radio");
		radio = GameObject.FindGameObjectWithTag("Radio");
		if (radio != null) {
			radio.GetComponent<Radio>().DisableRadio();
		}
		else {
			Debug.Log("no radio");
		}
	}
    

}
