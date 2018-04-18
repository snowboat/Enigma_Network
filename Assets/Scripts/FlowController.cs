using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject[] cameraPositions;
	public GameObject selectCanvas;
	public GameObject selectPanel;
	public GameObject networkPanel;
	public GameObject faciCanvas;
	public GameObject[] scenes;
	public AudioClip[] themeMusicForEachScene;
	public AudioClip[] musicInUse;
	public string[] monthForEachScene;
	public int[] dayForEachScene;
	public int[] hourForEachScene;
	public int[] minForEachScene;
	public GameObject[] props;
	// public GameObject networkPrefab;
	public AudioSource mainAudioSource;
	public AudioSource successSoundSource;
	private int currentScene = 0;

	// Use this for initialization
	void Start () {
		selectCanvas.SetActive(true);
		selectPanel.SetActive(true);
		networkPanel.SetActive(false);
		faciCanvas.SetActive(false);
		// Set the active state of scenes in faci canvas
		if (scenes.Length > 0) {
			for (int i = 0; i < scenes.Length; i++) {
				scenes[i].SetActive(false);
			}
		}
	}

	public void chooseFaci() {
		selectCanvas.SetActive(false);
		faciCanvas.SetActive(true);
		// initiate first scene
		scenes[currentScene].SetActive(true);

		SetScene();
	}

	public void chooseProps() {
		selectPanel.SetActive(false);
		networkPanel.SetActive(true);
	}

	public void JoinAs(int num) {
		networkPanel.SetActive(false);
		// set the camera for props
		mainCamera.transform.position = cameraPositions[num].transform.position;
		mainCamera.transform.rotation = cameraPositions[num].transform.rotation;
		// enable the corresponding prop
		props[num].SetActive(true);
	}
	
	// "Next" button 
	public void NextScene() {
		if (scenes.Length > 0) {
			scenes[currentScene].SetActive(false);
			currentScene++;
			if (currentScene >= scenes.Length) {
				currentScene = 0;
			}
			scenes[currentScene].SetActive(true);

			SetScene();
		}
	}

	public void ChangeBackgroundMusic(int musicNumber) {
		if (musicNumber < musicInUse.Length) {
			Debug.Log("change background music");
			PlayBackgroundMusic(musicInUse[musicNumber]);
		} else {
			Debug.Log("no music");
		}
	}

	public void PlaySuccessSound() {
		if (successSoundSource.isPlaying) {
			successSoundSource.Stop();
		}
		successSoundSource.Play();
	}

	void PlayBackgroundMusic(AudioClip clip) {
		if (mainAudioSource.isPlaying) {
			mainAudioSource.Stop();
		}
		mainAudioSource.clip = clip;
		mainAudioSource.Play();
	}

	void SetScene() {
		// play theme song
		if (currentScene < themeMusicForEachScene.Length && themeMusicForEachScene[currentScene]!=null) {
			Debug.Log("next change music");
			PlayBackgroundMusic(themeMusicForEachScene[currentScene]);
		}

		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 

		foreach (GameObject networkPrefab in networkPrefabs) {
			// change clock
			if (currentScene < monthForEachScene.Length) {
				Debug.Log("next change clock");
				networkPrefab.GetComponent<PropsController>().RpcSendTimeInfoToClock(monthForEachScene[currentScene], dayForEachScene[currentScene], hourForEachScene[currentScene], minForEachScene[currentScene]);
			}
			// disable phone
			networkPrefab.GetComponent<PropsController>().RpcDisablePhone();
			// disable radio
			networkPrefab.GetComponent<PropsController>().RpcDisableRadio();
		}
	}
}
