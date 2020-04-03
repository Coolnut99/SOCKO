using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengesGameplay : MonoBehaviour {

	private int challengeNumber;
	private int windowsSockoed, smileysSockoed;
	private int windowsNeeded, smileysNeeded;
	public int numberOfLives;		// Ignored unless you get a "don't lose a life" level, where this variable is 1
	private bool levelPass, levelFail;
	private bool success;
	private bool fail;
	private bool hasCompletedBefore;
	private bool miss;				// TEST for now; Set to true if you lose a life
	private float scoreNeeded;
	private float challengeHighScore;
	private float timeToPass;		// Time in seconds used for "survival" stages -- set at 99999 if not needed
	private float timeToFail;		// Time in seconds used to complete a level -- set at 99999 if not needed
	private float timeElapsed;		// Time elapsed in seconds

	[SerializeField]
	private GameController gameController;

	[SerializeField]
	private GameOverController gameOverController;

	[SerializeField]
	private Text timeText, goalText, youWinText;

	[SerializeField]
	private GameObject fadeBackground, youWin, youWinCanvas, ReturnToStartButton;

	[SerializeField]
	private AudioSource youWinAudio;

	[SerializeField]
	private FaceController faceController;

	private Color tempColor;


	void Awake() {
		challengeNumber = GameObject.Find("Master Control").GetComponent<MasterControl>().challengeNumber;
		Debug.Log("The challenge you are on now is " + challengeNumber);
		MasterControl.instance.challengeNumber = challengeNumber;
		challengeHighScore = PlayerPrefs.GetFloat("ChallengeScore_" + challengeNumber.ToString());
		if(challengeHighScore == 0) {
			hasCompletedBefore = false;
		} else {
			hasCompletedBefore = true;
		}
		if(hasCompletedBefore == true) {
			Debug.Log("This challenge has been done.");
		} else {
			Debug.Log("This challenge has NOT been done.");
		}
	}

	// Use this for initialization
	void Start () {
		tempColor = youWinText.color;
		youWinText.color = new Color(0f, 0f, 0f, 0f);
		windowsSockoed = 0;
		smileysSockoed = 0;
		levelPass = false;
		levelFail = false;
		success = false;
		fail = false;
		fadeBackground.SetActive(false);
		youWin.SetActive(false);
		youWinCanvas.SetActive(false);
		ReturnToStartButton.SetActive(false);
		SetGoal();
		//StartCoroutine(StartChallenge());
	}

	void FixedUpdate() {
		CheckForGoals();
		PassOrFail();
		if (!levelFail && !levelPass && gameController.lives > 0) {
			if(timeToFail < 99999) {
				timeText.text = "Time Remaining: " + (Mathf.Round((timeToFail - timeElapsed)* 100)/100).ToString();
			} else if (timeToPass < 99999) {
				timeText.text = "Time Left: " + (Mathf.Round((timeToPass - timeElapsed)* 100)/100).ToString();
			} else {
				timeText.text = "Elapsed Time: " + (Mathf.Round(timeElapsed * 100) / 100).ToString();
			}
	
			if(gameController.windowBroken == true) {
				UpdateWindowGoal();
			}
			
			if(gameController.smileyHit == true) {
				UpdateSmileyGoal();
			}

			if(scoreNeeded < 999999f) {
				goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			}

		}
	}

	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
	}

	void CheckForGoals() {
		if (windowsSockoed >= windowsNeeded) {
			levelPass = true;
		}

		if (smileysSockoed >= smileysNeeded) {
			levelPass = true;
		}

		if (gameController.score >= scoreNeeded) {
			levelPass = true;
		}

		if (timeElapsed >= timeToPass) {
			levelPass = true;
		}

		if (gameController.lives <= 0) {
			timeToPass = 99999f;
		}

		if (timeElapsed >= timeToFail) {
			levelFail = true;
		}
	}

	void PassOrFail() {
		if(levelPass && success == false) {
			// You succeeded!
			// Show success panel and mark as complete
			gameController.GetComponent<GameController>().passChallenge = true;
			success = true;
			faceController.SetHappyImage(99f);
			StartCoroutine(LevelCompleteCoroutine());
			if (challengeNumber < 36) {
				PlayerPrefs.SetInt("Challenge_" + (challengeNumber+1).ToString(), 1);
			}
			//challengeHighScore = PlayerPrefs.GetFloat("ChallengeScore_" + challengeNumber.ToString());
			Debug.Log("Challenge High Score is: " + challengeHighScore);
			if (gameController.score > challengeHighScore) {
				PlayerPrefs.SetFloat("ChallengeScore_" + challengeNumber.ToString(), gameController.score);
			}

			// if score is higher than high score, mark in PlayerPrefs as the new high score
		} else if(levelFail && fail == false && gameController.lives > 0) {
			// GAME OVER BUDDY!
			// Exit and do NOT mark as complete
			fail = true;
			faceController.SetArghImage(99f);
			if(FistTable.instance.fistSpecial == 17) {
				gameController.CalculateFist20BonusCoins();
			}
			gameController.GetComponent<GameController>().savedCoinScore += gameController.GetComponent<GameController>().coinScore;
			gameController.GetComponent<GameController>().bonusCoinScore++;	//Get 1 coin for losing as long as you "tried"
			gameController.GetComponent<GameController>().savedCoinScore += gameController.GetComponent<GameController>().bonusCoinScore;
			PlayerPrefs.SetInt(GamePreferences.CoinScore, gameController.GetComponent<GameController>().savedCoinScore);
			gameOverController.GetComponent<GameOverController>().GameOver();
		}
	}

	void UpdateWindowGoal() {
		if (windowsNeeded < 9999) {
			windowsSockoed += gameController.numberWindowsBroken;
			gameController.numberWindowsBroken = 0;
			gameController.windowBroken = false;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
		}
	}

	void UpdateSmileyGoal() {
		if (smileysNeeded < 9999) {
			smileysSockoed += gameController.numberSmileysHit;
			gameController.numberSmileysHit = 0;
			gameController.smileyHit = false;
			goalText.text = "Smileys Needed: " + (smileysNeeded - smileysSockoed).ToString() + "/" + smileysNeeded.ToString();
		}
	}

	void SetGoal() {
		switch (challengeNumber) {

			case 1:
			windowsNeeded = 10;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			// Also initialize lives, speed, acceleration, bombs dropping, etc.
			break;

			case 2:
			windowsNeeded = 9999;
			smileysNeeded = 50;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 120f;
			goalText.text = "Smileys Needed: " + (smileysNeeded - smileysSockoed).ToString() + "/" + smileysNeeded.ToString();
			// speed
			// acceleration
			// number of smileys (3)
			// time left for smileys (1s)
			// time for windows
			// number of windows
			break;

			case 3: 
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 30f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "";
			break;

			case 4: 
			windowsNeeded = 20;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 5: 
			windowsNeeded = 30;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 3;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 6: 
			windowsNeeded = 10;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 7: 
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 10000f;
			timeToPass = 99999f;
			timeToFail = 45f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 8:
			windowsNeeded = 50;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 9: 
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 5000f;
			timeToPass = 99999f;
			timeToFail = 15f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;


			case 10:
			windowsNeeded = 1;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 11:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 90f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "";
			break;

			case 12:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 25000f;
			timeToPass = 99999f;
			timeToFail = 120f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 13:
			windowsNeeded = 100;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 14:
			windowsNeeded = 20;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 15:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 30f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "";
			break;

			case 16:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 120f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "";
			break;

			case 17:
			windowsNeeded = 9999;
			smileysNeeded = 150;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 180f;
			goalText.text = "Smileys Needed: " + (smileysNeeded - smileysSockoed).ToString() + "/" + smileysNeeded.ToString();
			break;

			case 18:
			windowsNeeded = 250;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 5;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 19:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 15000f;
			timeToPass = 99999f;
			timeToFail = 30f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 20:
			windowsNeeded = 9999;
			smileysNeeded = 200;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 180f;
			goalText.text = "Smileys Needed: " + (smileysNeeded - smileysSockoed).ToString() + "/" + smileysNeeded.ToString();
			break;

			case 21:
			windowsNeeded = 100;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 22:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 50000f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 23:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 10000f;
			timeToPass = 99999f;
			timeToFail = 15f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 24:
			windowsNeeded = 100;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 25:
			windowsNeeded = 20;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 26:
			windowsNeeded = 9999;
			smileysNeeded = 50;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 180f;
			goalText.text = "Smileys Needed: " + (smileysNeeded - smileysSockoed).ToString() + "/" + smileysNeeded.ToString();
			break;

			case 27:
			windowsNeeded = 300;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 28:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 100000f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 29:
			windowsNeeded = 250;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 30:
			windowsNeeded = 10;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "Windows Left: " + (windowsNeeded - windowsSockoed).ToString() + "/" + windowsNeeded.ToString();
			break;

			case 31:
			windowsNeeded = 9999;
			smileysNeeded = 250;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 180f;
			goalText.text = "Smileys Needed: " + (smileysNeeded - smileysSockoed).ToString() + "/" + smileysNeeded.ToString();
			break;

			case 32:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 240f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "";
			break;

			case 33:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 150000f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 34:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 480f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 3;
			goalText.text = "";
			break;

			case 35:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 200000f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			goalText.text = "Score Needed: " + (scoreNeeded - gameController.score).ToString() + "/" + scoreNeeded.ToString();
			break;

			case 36:
			windowsNeeded = 9999;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 360f;
			timeToFail = 99999f;
			gameController.GetComponent<GameController>().lives = 1;
			goalText.text = "";
			break;

			default:
			windowsNeeded = 10;
			smileysNeeded = 9999;
			scoreNeeded = 999999f;
			timeToPass = 99999f;
			timeToFail = 99999f;
			break;

		}
		gameController.GetComponent<GameController>().UpdateLives();
	}

	void CalculateBonusCoinsChallenge() {
		if(hasCompletedBefore == true) {
			gameController.GetComponent<GameController>().bonusCoinScore = Mathf.RoundToInt(challengeNumber / 12) + 2;
		} else {
			gameController.GetComponent<GameController>().bonusCoinScore = challengeNumber * 5; // Or make this 10?
		}
		if(FistTable.instance.fistSpecial == 17) {
			gameController.CalculateFist20BonusCoins();
		}
	}

	void CreateRandomSuccessQuip() {
		int i = Random.Range(0, 5);
		switch (i) {
			case 0:
			youWinText.text = "S - U - C - C - E - E - S, that's the way you spell success.";
			break;

			case 1:
			youWinText.text = "Way to go! You're an up and coming champ!";
			break;

			case 2:
			youWinText.text = "A WINNER IS YOU";
			break;

			case 3:
			youWinText.text = "BRAVO! YOU WIN!";
			break;

			case 4:
			youWinText.text = "PARTY ON!";
			break;

			default:
			youWinText.text = "S - U - C - C - E - E - S, that's the way you spell success.";
			break;


		}
	}

	IEnumerator StartChallenge() {
		yield return new WaitForSecondsRealtime(1f);
		SetGoal();
	}

	IEnumerator LevelCompleteCoroutine() {
		CalculateBonusCoinsChallenge();
		gameController.GetComponent<GameController>().WinChallengeCoins();
		Time.timeScale = 0f;
		youWinAudio.Play();
		fadeBackground.SetActive(true);
		youWin.SetActive(true);
		youWinCanvas.SetActive(true);
		yield return new WaitForSecondsRealtime(1f);
		CreateRandomSuccessQuip();
		youWinText.color = tempColor;
		yield return new WaitForSecondsRealtime(1f);
		gameOverController.GetComponent<GameOverController>().DisplayFinalStatus();
		ReturnToStartButton.SetActive(true);
		MasterControl.instance.SetNumberOfSmileys();
		MasterControl.instance.SetNumberOfWindows();
		AchievementsController.instance.SetScore(gameController.score);
	}
}
