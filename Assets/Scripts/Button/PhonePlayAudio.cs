using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	This script is for Phone Audio button.
*/

public class PhonePlayAudio : MonoBehaviour {

	public int clipNumber;

	public void PlayAudio() {
		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 
		Debug.Log("button phone play audio");

		foreach (GameObject networkPrefab in networkPrefabs) {
			networkPrefab.GetComponent<PropsController>().RpcPhonePlayClip(clipNumber);
		}
	}
}
