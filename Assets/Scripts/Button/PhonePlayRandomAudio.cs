using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePlayRandomAudio : MonoBehaviour {

	public int firstClipNumber;
	public int lastClipNumber;

	public void PlayRandomAudio() {
		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 
		Debug.Log("button phone play random audio");

		int realClipNumber = Random.Range(firstClipNumber, lastClipNumber+1);

		foreach (GameObject networkPrefab in networkPrefabs) {
			networkPrefab.GetComponent<PropsController>().RpcPhonePlayClip(realClipNumber);
		}
	}
}
