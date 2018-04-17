using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRadio : MonoBehaviour {

	public int clipNumber;
	public int channelNumber;

	public void ActiveRadio() {
		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 
		Debug.Log("button set radio");

		foreach (GameObject networkPrefab in networkPrefabs) {
			networkPrefab.GetComponent<PropsController>().RpcSetRadioClip(clipNumber);
			networkPrefab.GetComponent<PropsController>().RpcActiveRadioChannel(channelNumber);
		}
	}
}
