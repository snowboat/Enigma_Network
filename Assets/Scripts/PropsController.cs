using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PropsController : NetworkBehaviour {
    private GameObject clock;
    private GameObject radio;
    private GameObject phone;

    public AudioClip[] audioInUse;

	// Send message to phone
	[ClientRpc]
    public void RpcActivePhoneWithNumberAndClip(string number, int audioClipNumber)
    {
        Debug.Log("active and set number: "+number);
		phone = GameObject.FindGameObjectWithTag("PhoneWheel");
		if (phone != null) {
			phone.GetComponent<PhoneWheel>().ActivePhone();
			phone.GetComponent<PhoneWheel>().SetTargetNumber(number);
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
    

}
