using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePlayAudio : MonoBehaviour {

	public int clipNumber;

	public void PlayAudio() {
		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 
		Debug.Log("button set radio");

		foreach (GameObject networkPrefab in networkPrefabs) {
			networkPrefab.GetComponent<PropsController>().RpcPhonePlayClip(clipNumber);
		}
	}
}
