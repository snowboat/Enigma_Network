using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	This script is for Phone Rings button.
*/

public class PhoneRing : MonoBehaviour {

	public void PlayPhoneRing() {
		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 
		Debug.Log("button phone ring");

		foreach (GameObject networkPrefab in networkPrefabs) {
			// phone ring
			networkPrefab.GetComponent<PropsController>().RpcPhoneRing();
		}
	}
}
