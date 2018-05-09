using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	This script is the main flow controller for the whole app.
*/

public class FlowController : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject[] cameraPositions;
	public GameObject selectCanvas;
	public GameObject selectPanel;
	public GameObject networkPanel;
	public GameObject faciCanvas;
	public GameObject musicPanel;
	public GameObject musicOnButton;
	public GameObject musicOffButton;
	public GameObject[] scenes;
	public AudioClip[] themeMusicForEachScene;
	public AudioClip[] musicInUse;
	public string[] monthForEachScene;
	public int[] dayForEachScene;
	public int[] hourForEachScene;
	public int[] minForEachScene;
	public GameObject[] props;
	// private bool switchOfMainAudioSource = true;
	// public Text signOfMainAudioSource;
	public AudioSource mainAudioSource;
	
	public AudioSource successSoundSource;
	
	private int currentScene = 0;

	// Use this for initialization
	void Start () {
		selectCanvas.SetActive(true);
		selectPanel.SetActive(true);
		networkPanel.SetActive(false);
		faciCanvas.SetActive(false);
		musicPanel.SetActive(true);
		musicOnButton.SetActive(false);
		musicOffButton.SetActive(true);
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

	// Join the game as a prop
	public void JoinAs(int num) {
		networkPanel.SetActive(false);
		// set the camera for props
		mainCamera.transform.position = cameraPositions[num].transform.position;
		mainCamera.transform.rotation = cameraPositions[num].transform.rotation;
		// enable the corresponding prop
		props[num].SetActive(true);
	}
	
	// "Next" button to enter the next scene
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

	// Change the background music of facilitator app
	public void ChangeBackgroundMusic(int musicNumber) {
		if (musicNumber < musicInUse.Length) {
			Debug.Log("change background music");
			PlayBackgroundMusic(musicInUse[musicNumber]);
		} else {
			Debug.Log("no music");
		}
	}

	// Play the success sound through success sound source
	public void PlaySuccessSound() {
		if (successSoundSource.isPlaying) {
			successSoundSource.Stop();
		}
		successSoundSource.Play();
	}

	// Stop the previous background music and play the new one
	void PlayBackgroundMusic(AudioClip clip) {
		if (mainAudioSource.isPlaying) {
			mainAudioSource.Stop();
			Debug.Log("stop the music");
		}
		if (clip!=null) {
			mainAudioSource.clip = clip;
			mainAudioSource.Play();
			Debug.Log("change the music");
		}
	}

	// Change scene information when the scene changes
	void SetScene() {
		// play theme song
		if (currentScene < themeMusicForEachScene.Length) {
			Debug.Log("next change music");
			PlayBackgroundMusic(themeMusicForEachScene[currentScene]);
		}

		GameObject[] networkPrefabs = GameObject.FindGameObjectsWithTag("NetworkPrefab"); 

		foreach (GameObject networkPrefab in networkPrefabs) {
			// change clock
			if (currentScene < monthForEachScene.Length && monthForEachScene[currentScene] != "NONE") {
				Debug.Log("next change clock");
				networkPrefab.GetComponent<PropsController>().RpcSendTimeInfoToClock(monthForEachScene[currentScene], dayForEachScene[currentScene], hourForEachScene[currentScene], minForEachScene[currentScene]);
			}
			// disable phone
			networkPrefab.GetComponent<PropsController>().RpcDisablePhone();
			// disable radio
			networkPrefab.GetComponent<PropsController>().RpcDisableRadio();
		}
	}

	// turn on/off the background music using same button, works in editor but not work in Android build
	/*
	public void turnMusic() {
		if (switchOfMainAudioSource) {
			mainAudioSource.volume = 0;
			signOfMainAudioSource.text = "Music\nOff";
			switchOfMainAudioSource = false;
		} else {
			mainAudioSource.volume = 1;
			signOfMainAudioSource.text = "Music\nOn";
			switchOfMainAudioSource = true;
		}
	}
	*/

	// 2 functions to turn on/off the background music using seperate buttons
	public void turnOnMusic() {
		musicOnButton.SetActive(true);
		mainAudioSource.volume = 1;
		musicOffButton.SetActive(false);
	}

	public void turnOffMusic() {
		musicOffButton.SetActive(true);
		mainAudioSource.volume = 0;
		musicOnButton.SetActive(false);
	}
}
