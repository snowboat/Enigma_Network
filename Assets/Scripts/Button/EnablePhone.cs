using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePhone : MonoBehaviour {

	
	public string phoneNumber;
	public int clipNumber;

	public void ActivePhone() {
		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 
		Debug.Log("button set radio");

		foreach (GameObject networkPrefab in networkPrefabs) {
			networkPrefab.GetComponent<PropsController>().RpcActivePhone();
			networkPrefab.GetComponent<PropsController>().RpcSetPhoneNumber(phoneNumber);
			networkPrefab.GetComponent<PropsController>().RpcSetPhoneClip(clipNumber);
		}
	}
}
