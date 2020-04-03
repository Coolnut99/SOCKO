using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChallengeController : MonoBehaviour {

	public int challengeNumber;

	[SerializeField]
	private Canvas canvasController, helpCanvasController, buyAccessCanvas;

	[SerializeField]
	private Text challengeText, challengeDescriptionText, challengeHighScoreText, challengeTotalScoreText;

	[SerializeField]
	private AudioSource validateChoice;

	[SerializeField]
	private Animator panelAnimator, helpPanelAnimator, buyAccessAnimator;

	[SerializeField]
	private GameObject Socko;
	GameObject sockoClone;

	[SerializeField]
	private GameObject youreWinnerButton;

	private float pixelsPerX, pixelsPerY;
	private float worldHeight, worldWidth;
	private float mouseX, mouseY;

	float totalScore;


	void Awake() {
		int i = MasterControl.instance.AddNumberOfChallenges();
		if(i < 36) {
			youreWinnerButton.SetActive(false);
		} else {
			youreWinnerButton.SetActive(true);
		}
	}

	// Use this for initialization
	void Start () {
		canvasController.enabled = false;
		helpCanvasController.enabled = false;
		buyAccessCanvas.enabled = false;
		challengeTotalScoreText.text = "SCORE: " + AddTotalScore().ToString();
		worldHeight = Camera.main.orthographicSize * 2f;
		worldWidth = worldHeight / Screen.height * Screen.width;
		pixelsPerX = Screen.width / worldWidth;
		pixelsPerY = Screen.height / worldHeight;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			float tempX = Input.mousePosition.x / pixelsPerX;
			float tempY = Input.mousePosition.y / pixelsPerY;
			mouseX = tempX - (worldWidth / 2);
			mouseY = tempY - (worldHeight / 2);
			InstantiateSocko();
		}
	}

	void InstantiateSocko() {
		sockoClone = Instantiate(Socko, transform.position, Quaternion.identity) as GameObject;
		sockoClone.transform.position = new Vector3(mouseX, mouseY, 0f);
	}

	public void GetChallenge() {
		challengeDescriptionText.resizeTextForBestFit = false;
		validateChoice.Play();
		Debug.Log(challengeNumber);
		canvasController.enabled = true;
		panelAnimator.Play("Panel Appear");
		challengeText.text = "Challenge " + challengeNumber.ToString();
		challengeHighScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetFloat("ChallengeScore_" + challengeNumber.ToString());
	
		challengeDescriptionText.text = "CHALLENGE:\n\n";
		switch(challengeNumber) {
			case 1:
			challengeDescriptionText.text += "SOCKO 10 WINDOWS! AVOID BOMBS!";
			break;

			case 2:
			challengeDescriptionText.text += "SOCKO 50 SMILEYS BEFORE TIME RUNS OUT!";
			break;

			case 3:
			challengeDescriptionText.text += "Survive 30 seconds without losing a life!";
			break;

			case 4:
			challengeDescriptionText.text += "Socko 20 windows!";
			break;

			case 5:
			challengeDescriptionText.text += "Socko 30 windows with only 3 lives!";
			break;

			case 6:
			challengeDescriptionText.text += "This is fast!\n\nSocko 10 windows!";
			break;

			case 7:
			challengeDescriptionText.text += "Score 10,000 points in 45 seconds!";
			break;

			case 8:
			challengeDescriptionText.text += "Socko 50 windows!";
			break;

			case 9:
			challengeDescriptionText.text += "Score 5,000 points in 15 seconds!";
			break;

			case 10:
			challengeDescriptionText.text += "This is fast!\n\nSocko 1 window!";
			break;

			case 11:
			challengeDescriptionText.text += "Survive 90 seconds without losing a life!";
			break;

			case 12:
			challengeDescriptionText.text += "Score 25,000 points in 2 minutes!";
			break;

			case 13:
			challengeDescriptionText.text += "Socko 100 windows!";
			break;

			case 14:
			challengeDescriptionText.text += "This is fast!\n\nSocko 20 windows!";
			break;

			case 15:
			challengeDescriptionText.resizeTextForBestFit = true;
			challengeDescriptionText.text += "This is fast!\n\nSurvive 30 seconds without losing a life!";
			break;

			case 16:
			challengeDescriptionText.resizeTextForBestFit = true;
			challengeDescriptionText.text += "Survive 120 seconds without losing a life!";
			break;

			case 17:
			challengeDescriptionText.text += "Socko 150 smileys in 3 minutes!";
			break;

			case 18:
			challengeDescriptionText.text += "Socko 200 windows with 5 lives!";
			break;

			case 19:
			challengeDescriptionText.text += "Score 15,000 points in 30 seconds!";
			break;

			case 20:
			challengeDescriptionText.text += "Socko 200 smileys in 3 minutes!";
			break;

			case 21:
			challengeDescriptionText.text += "Socko 100 windows without losing a life!";
			break;

			case 22:
			challengeDescriptionText.text += "Score 50,000 points!";
			break;

			case 23:
			challengeDescriptionText.text += "Score 10,000 points in 15 seconds!";
			break;

			case 24:
			challengeDescriptionText.text += "This is fast!\n\nSocko 100 windows!";
			break;

			case 25:
			challengeDescriptionText.resizeTextForBestFit = true;
			challengeDescriptionText.text += "This is fast!\n\nSocko 20 windows without losing a life!";
			break;

			case 26:
			challengeDescriptionText.text += "Socko 50 smileys!";
			break;

			case 27:
			challengeDescriptionText.text += "Socko 300 windows!";
			break;

			case 28:
			challengeDescriptionText.text += "Score 100,000 points!";
			break;

			case 29:
			challengeDescriptionText.text += "Socko 250 windows without losing a life!";
			break;

			case 30:
			challengeDescriptionText.text += "This is fast!\n\nSocko 10 windows!";
			break;

			case 31:
			challengeDescriptionText.text += "Socko 250 smileys in 3 minutes!";
			break;

			case 32:
			challengeDescriptionText.text += "Survive 4 minutes without losing a life!";
			break;

			case 33:
			challengeDescriptionText.text += "Score 150,000 points!";
			break;

			case 34:
			challengeDescriptionText.text += "Survive 8 minutes!";
			break;

			case 35:
			challengeDescriptionText.text += "Score 200,000 points!";
			break;

			case 36:
			challengeDescriptionText.text += "Survive 6 minutes without losing a life!";
			break;

			default:
			challengeDescriptionText.text = "YOU GET NOTHING BUT A SOCKO TO THE NOSE!";
			break;
		}
	}

	public void StartChallenge() {
		GameObject.Find("Master Control").GetComponent<MasterControl>().challengeNumber = challengeNumber;
		Debug.Log(GameObject.Find("Master Control").GetComponent<MasterControl>().challengeNumber);
		//SceneManager.LoadScene("Challenges Gameplay");
		SceneManager.LoadScene("Select a Fist");
	}

	public void HelpSelect() {
		validateChoice.Play();
		helpCanvasController.enabled = true;
		helpPanelAnimator.Play("Panel Appear");
	}

	public void BuyAccessPlay() {
		validateChoice.Play();
		buyAccessCanvas.enabled = true;
		buyAccessAnimator.Play("Panel Appear");
	}

	public void BuyAccessGoBack() {
		validateChoice.Play();
		buyAccessCanvas.enabled = false;
		GameObject [] g = GameObject.FindGameObjectsWithTag("Challenge Button");
		foreach(GameObject x in g) {
			x.GetComponent<ChallengeNumber>().CheckChallenge();
		}
	}

	public void GoBack() {
		validateChoice.Play(); 
		canvasController.enabled = false;
	}

	public void HelpGoBack() {
		validateChoice.Play(); 
		helpCanvasController.enabled = false;
	}

	float AddTotalScore() {
		totalScore = 0;
		for (int i = 0; i < GamePreferences.numberOfChallenges; i++) {
			totalScore += PlayerPrefs.GetFloat("ChallengeScore_" + (i+1).ToString());
		}

		return totalScore;
	}
}
