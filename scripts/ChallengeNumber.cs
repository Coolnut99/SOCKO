using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeNumber : MonoBehaviour {

	public int challengeNumber;
	public bool challengeLocked;
	public Button button;
	public string challengeName;

	[SerializeField]
	private Text numberText;

	[SerializeField]
	private GameObject lockImage, checkMark;

	private Color activatedTextColor;

	// Use this for initialization
	void Start () {
		checkMark.SetActive(false);
		if(PlayerPrefs.GetInt(challengeName) == 1) {
			challengeLocked = false;
			lockImage.SetActive(false);
		} else {
			challengeLocked = true;
			lockImage.SetActive(true);
		}

		activatedTextColor = numberText.color;
		if(challengeLocked == true) {
			//Disable button here
			button.interactable = false;
			numberText.color = new Color (0.3f, 0.3f, 0f, 1f);
		} else {
			//Enable button here
			button.interactable = true;
			numberText.color = activatedTextColor;
			if(PlayerPrefs.GetFloat("ChallengeScore_" + challengeNumber.ToString()) > 0){
				checkMark.SetActive(true);
			}
		}
	}

	public void GetChallengeNumber() {
		GameObject.Find("Challenge Controller").GetComponent<ChallengeController>().challengeNumber = challengeNumber;
		GameObject.Find("Challenge Controller").GetComponent<ChallengeController>().GetChallenge();
	}

	public void CheckChallenge() {
		if(PlayerPrefs.GetInt(challengeName) == 1) {
			challengeLocked = false;
			lockImage.SetActive(false);
		} else {
			challengeLocked = true;
			lockImage.SetActive(true);
		}

		if(challengeLocked == true) {
			//Disable button here
			button.interactable = false;
			numberText.color = new Color (0.3f, 0.3f, 0f, 1f);
		} else {
			//Enable button here
			button.interactable = true;
			numberText.color = activatedTextColor;
			if(PlayerPrefs.GetFloat("ChallengeScore_" + challengeNumber.ToString()) > 0){
				checkMark.SetActive(true);
			}
		}
	}
}
