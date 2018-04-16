using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMaster : MonoBehaviour {

	// start canvas
	public Camera mainCamera;
	public Transform[] cameraPositions;
	public GameObject startPanel;
	public GameObject endPanel;
	public Text roleText;

	// facilitator canvas
	public GameObject faciCanvas;
	public GameObject guidePanel;
	public GameObject faciNarrativePanel;
	public GameObject faciPuzzlePanel;
	public GameObject faciFeedbackPanel;

	// player canvas
	public GameObject playerCanvas;
	public GameObject rolePanel;
	public GameObject codePanel;
	public InputField codeInput;
	public GameObject playerPuzzlePanel;
	public GameObject playerFeedbackPanel;

	// common fuctions
	private void SetRoleTo(Database.Roles role) {
		Database.role = role;
	}

	public void ChooseFacilitator() {
		startPanel.SetActive(false);
		faciCanvas.SetActive(true);
	    guidePanel.SetActive(true);
		SetRoleTo(Database.Roles.Facilitator);
	}

	public void ChoosePlayer() {
		startPanel.SetActive(false);
		playerCanvas.SetActive(true);
		rolePanel.SetActive(true);
	}

	// facilitator canvas functions
	public void FinishGuide() {
		guidePanel.SetActive(false);
		faciNarrativePanel.SetActive(true);
	}
	
	public void FinishNarrativeFaci() {
		faciNarrativePanel.SetActive(false);
		faciPuzzlePanel.SetActive(true);
	}

	public void FinishPuzzleFaci() {
		faciPuzzlePanel.SetActive(false);
		faciFeedbackPanel.SetActive(true);
	}

	public void FinishFeedbackFaci() {
		faciFeedbackPanel.SetActive(false);
		Database.currentScene++;
		if (Database.currentScene < Database.totalNumOfScene) {
			faciNarrativePanel.SetActive(true);
			mainCamera.transform.position = cameraPositions[Database.currentScene].position;
			mainCamera.transform.rotation = cameraPositions[Database.currentScene].rotation;
		} else {
			endPanel.SetActive(true);
		}
	}

	// player canvas functions
	public void ChoosePlayerRole(string roleString) {
		try {
            Database.Roles roleValue = (Database.Roles) Enum.Parse(typeof(Database.Roles), roleString);        
            if (Enum.IsDefined(typeof(Database.Roles), roleValue))  {
				SetRoleTo(roleValue);
				Console.WriteLine("The role of the player is '{0}'.",  roleValue.ToString());
			}
            else
               Console.WriteLine("Wrong input");
        }
        catch (ArgumentException) {
        	Console.WriteLine("Wrong input");
        }
		rolePanel.SetActive(false);
		playerPuzzlePanel.SetActive(true);
	}

	public void FinishPuzzlePlayer() {
		playerPuzzlePanel.SetActive(false);
		playerFeedbackPanel.SetActive(true);
	}

	public void FinishFeedbackPlayer() {
		playerFeedbackPanel.SetActive(false);
		Database.currentScene++;
		if (Database.currentScene < Database.totalNumOfScene) {
			playerPuzzlePanel.SetActive(true);
			mainCamera.transform.position = cameraPositions[Database.currentScene].position;
			mainCamera.transform.rotation = cameraPositions[Database.currentScene].rotation;
		} else {
			endPanel.SetActive(true);
		}
	}

	private void Start() {
		Screen.orientation = ScreenOrientation.Landscape;
		startPanel.SetActive(true);
	}

	private void Update() {
		if (Database.role.ToString() != "None") {
			roleText.text = "You are playing as a " + Database.role.ToString();
		}
	}
}
