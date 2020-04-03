using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[SerializeField]
	public WindowSpawner windowSpawner;

	[SerializeField]
	private Text scoreText, livesText, soundText;

	[SerializeField]
	private GameOverController gameOverController;

	[SerializeField]
	private Animator animator, explosionAnimator, fistAnimator;

	[SerializeField]
	private GameObject gamePausedCanvas, gamePausedTextCanvas, getReadyCanvas, confirmExitCanvas, fadeBackground;

	[SerializeField]
	private CameraScript cameraScript;

	[SerializeField]
	private AudioSource explode, whapSound;

	[SerializeField]
	private GameObject Socko, superSockoAttack;
	GameObject sockoClone, superSockoClone;

	[SerializeField]
	private GameObject sockoFist;

	[SerializeField]
	public SoundController soundController;

	[SerializeField]
	private FaceController faceController;

	//private GameObject[] destroyWindows, destroyBombs; //May not be needed

	public float score, highScore;						// DUH.
	public float highScoreScoreAttack;					// "Score Attack Mode" high score
	public float smileyBonus, smileyMaxBonus;			// Bonus points for hitting smilies without losing a life.
	public float windowMultiplier, smileyMultiplier; 	// Point multiplier for certain fists

	public float textTimer;					// Timer in which text is displayed below.

	public int lives;						// Also DUH.
	public int difficulty;					// Not related to the enum, this is used to "adjust" the dynamic difficulty as the game progresses
	private float pixelsPerX, pixelsPerY;	// These six are used to calculate where to instantiate the fist and hit effects.
	private float worldHeight, worldWidth;	//
	private float mouseX, mouseY;			//

	private bool rechargeSocko;
	private bool collectedLifeWithFistSpecial14;
	private bool collectedLifeWithFistSpecial12;
	private float extraLifeForFist24;
	const float extraLife25000 = 25000f;

	public bool gameStarted;		//	Did the game start?
	public bool gamePaused;			// Is the game paused?
	public bool gameOver;			// Is the game over?
	public bool lifeLost;			// Is a life lost? If yes, make sure player doesn't lose another during losing animations
	public bool missedWindow;		// true if life is lost from a window; false otherwise
	public bool immunity;			// If you lose a life, allow a brief grace period in which you can't lose a life again
	public bool windowBroken; 		// Only for challenge level
	public bool smileyHit; 			// Only for challenge level
	public bool passChallenge;	 	// Only for challenge level
	public bool grunt;				// NNNNNHHHHHH!!!!!
	public int soundOn;				// Is the sound on? (PlayerPrefs)
	public int numberWindowsBroken; // Only for challenge level
	public int numberSmileysHit; 	// Only for challenge level
	public int numberOfBigWindowsHit;
	private int numberOfRapsberries;// Number of rapsberries from SOCKOed smileys

	public bool invincible, superSocko, slowDown, moneybag;	//Powerups (they are mutually exclusive)
	[SerializeField]
	private GameObject invincibleImage, superSockoImage, slowDownImage, moneybagImage;

	private float timeToRecharge = 0.1f;	// Default
	public float criticalPowerUp;			// Time before powerup "fades"

	public int coinScore, savedCoinScore, bonusCoinScore;
	private int timesPlayed;
	public int hitsBeforeGrunt;

	private Powerup.PowerupType currentPowerup;

	private Color normalColor, criticalColor;

	// Use this for initialization
	void Start () {
		score = 0f;
		if(MasterControl.instance.scoreAttack && MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
			lives = 3;
			Debug.Log("You are playing the SCORE ATTACK mode.");
		} else {
			lives = PlayerPrefs.GetInt(GamePreferences.MaxLives);
			Debug.Log("SCORE ATTACK not active.");
		}
		coinScore = 0;
		bonusCoinScore = 0;
		Time.timeScale = 0f;
		smileyBonus = 0f;
		smileyMaxBonus = 200f;
		CancelPowerups();
		savedCoinScore = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		gameStarted = false;
		gameOver = false;
		lifeLost = false;
		gamePaused = false;
		passChallenge = false;
		immunity = false;
		rechargeSocko = true;
		gamePausedCanvas.SetActive(false);
		gamePausedTextCanvas.SetActive(false);
		getReadyCanvas.SetActive(true);
		confirmExitCanvas.SetActive(false);
		//scoreText.text = "Score: " + score.ToString();
		soundOn = GamePreferences.GetMusicState();
		SetHitsBeforeGrunt();
		CheckSoundText();

		timeToRecharge = 1 / (FistTable.instance.fistRate + 1);

		windowMultiplier = 1f;
		smileyMultiplier = 1f;

		collectedLifeWithFistSpecial14 = false;
		collectedLifeWithFistSpecial12 = false;
		extraLifeForFist24 = extraLife25000;

		if(FistTable.instance.fistSpecial == 16) {
			windowMultiplier = 0.5f;
			smileyMultiplier = 2f;
		} else if (FistTable.instance.fistSpecial == 14) {
			windowMultiplier = 2f;
			smileyMultiplier = 2f;
			lives = 1;
		} else if (FistTable.instance.fistSpecial == 19) {
			windowMultiplier = 0f;
			smileyMultiplier = 1f;
		}

		UpdateScore();
		UpdateLives();
		normalColor = new Color (1f, 1f, 1f, 1f);
		criticalColor = new Color (1f, 1f, 1f, 0.3f);


		worldHeight = Camera.main.orthographicSize * 2f;
		worldWidth = worldHeight / Screen.height * Screen.width;
		pixelsPerX = Screen.width / worldWidth;
		pixelsPerY = Screen.height / worldHeight;

		timesPlayed = PlayerPrefs.GetInt(GamePreferences.TotalPlays);
		timesPlayed++;
		PlayerPrefs.SetInt(GamePreferences.TotalPlays, timesPlayed);

		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
			highScore = PlayerPrefs.GetFloat(GamePreferences.EasyDifficultyScore);
			highScoreScoreAttack = PlayerPrefs.GetFloat(GamePreferences.EasyDifficultyScoreScoreAttack);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
			highScore = PlayerPrefs.GetFloat(GamePreferences.MediumDifficultyScore);
			highScoreScoreAttack = PlayerPrefs.GetFloat(GamePreferences.MediumDifficultyScoreScoreAttack);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
			highScore = PlayerPrefs.GetFloat(GamePreferences.HardDifficultyScore);
			highScoreScoreAttack = PlayerPrefs.GetFloat(GamePreferences.HardDifficultyScoreScoreAttack);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			highScore = PlayerPrefs.GetFloat("ChallengeScore_" + MasterControl.instance.challengeNumber.ToString());
		}

		//
		//
		//Also play an ad here if this is the first or every fifth time playing
		//
		//
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && gameOver == false && lifeLost == false && gamePaused == false) {
			float tempX = Input.mousePosition.x / pixelsPerX;
			float tempY = Input.mousePosition.y / pixelsPerY;
			Debug.Log("TempY is: " + tempY);
			mouseX = tempX - (worldWidth / 2); // You COULD add Camera.main.transform.position.x but it is always 0
			mouseY = tempY - (worldHeight / 2) + Camera.main.transform.position.y;
			if(tempY > 1.28f) { //So you don't accidentally "punch" something below the status screen
				InstantiateSocko();
			}


		}
		if(lives <= 0 && gameOver == false) {
			gameOver = true;
			savedCoinScore += coinScore;
			if(FistTable.instance.fistSpecial == 17) {
				CalculateFist20BonusCoins();
			}
			faceController.SetArghImage(99f);
			if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
				bonusCoinScore++;	//Get 1 coin for losing as long as you "tried"
				savedCoinScore += bonusCoinScore;
			}

			if(MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
				CalculateBonusCoins();
				savedCoinScore += bonusCoinScore;
			}
			PlayerPrefs.SetInt(GamePreferences.CoinScore, savedCoinScore);
			if(score > highScore) {
				SetHighScore();
			}
			if(MasterControl.instance.scoreAttack && score > highScoreScoreAttack) {
				SetHighScoreScoreAttack();
			}
			gameOverController.GameOver();
		}

		//For fist specials 12 (100000 = 1 life), 14 (50000 = 1 life), and 15 (every 25000 = 1 life)
		if(score >= 100000f && collectedLifeWithFistSpecial14 == false && FistTable.instance.fistSpecial == 14) {
			collectedLifeWithFistSpecial14 = true;
			lives++;
			UpdateLives();
		} else if (score >= 50000f && collectedLifeWithFistSpecial12 == false && FistTable.instance.fistSpecial == 12) {
			collectedLifeWithFistSpecial12 = true;
			lives++;
			UpdateLives();
		} else if (score >= extraLifeForFist24 && extraLifeForFist24 < 1000000f && FistTable.instance.fistSpecial == 15) {
			extraLifeForFist24 += extraLife25000;
			lives++;
			UpdateLives();
		}
	}

	void FixedUpdate() {
		if(textTimer > 0f) {
			textTimer -= Time.deltaTime;
		} else {
			textTimer = 0f;
		}
	}

	void InstantiateSocko() {
		if (rechargeSocko == true && gameStarted == true && Time.timeScale > 0f) {
			sockoClone = Instantiate(Socko, transform.position, Quaternion.identity) as GameObject;
			sockoClone.transform.position = new Vector3(mouseX, mouseY, 0f);
			sockoFist.transform.position = new Vector3(mouseX, mouseY, 0f);
			sockoFist.gameObject.GetComponent<SpriteRenderer>().sprite = FistTable.instance.GetComponent<SpriteRenderer>().sprite;
			sockoFist.gameObject.GetComponent<SpriteRenderer>().color = FistTable.instance.GetComponent<SpriteRenderer>().color;
			if(FistTable.instance.fistSpriteType == 1) {
				sockoFist.transform.localScale = new Vector3(3f, 3f, 3f);
				sockoFist.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			} else if (FistTable.instance.fistSpriteType == 2) {
				sockoFist.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				sockoFist.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
			} else if (FistTable.instance.fistSpriteType == 3) { //The "bare fists" option
				sockoFist.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
			}
			fistAnimator.Play("fist appear");
			if (superSocko == true || FistTable.instance.fistSpecial == 13) {
				//Instantiate a SUPER SOCKO!
				superSockoClone = Instantiate(superSockoAttack, transform.position, Quaternion.identity) as GameObject;
				superSockoClone.transform.position = new Vector3(mouseX, mouseY, 0f);
			}
			soundController.PlaySwingSound();
			StartCoroutine(Recharge());
		}
	}

	public void UpdateScore() {
		scoreText.text = "Score: " + score.ToString();
	}

	public void UpdateLives() {
		livesText.text = "Lives: " + lives.ToString();
	}

	public void DestroyAllBombs() {
		if(lifeLost == false && immunity == false) {
			lifeLost = true;
			StartCoroutine(LoseALife());
		}
	}

	public void GameStart() {
		gameStarted = true;
		Time.timeScale = 1f;
		getReadyCanvas.SetActive(false);
	}

	public void GamePaused() {
		if(gamePaused == false){
			faceController.PauseFace();
			gamePaused = true;
			gamePausedCanvas.SetActive(true);
			gamePausedTextCanvas.SetActive(true);
			animator.Play("Pause active");
			Time.timeScale = 0f;
			fadeBackground.SetActive(true);
		} else {
			gamePaused = false;
			gamePausedCanvas.SetActive(false);
			gamePausedTextCanvas.SetActive(false);
			animator.Play("Pause idle");
			Time.timeScale = 1f;
			faceController.ResumeFace();
			fadeBackground.SetActive(false);
		}
	}

	public void YesToQuit() {
		gamePausedCanvas.SetActive(false);
		gamePausedTextCanvas.SetActive(false);
		confirmExitCanvas.SetActive(true);
	}

	public void NoToQuit() {
		gamePausedCanvas.SetActive(true);
		gamePausedTextCanvas.SetActive(true);
		confirmExitCanvas.SetActive(false);
	}

	public void QuitThisGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Main Menu");
	}

	public void LoseALifeFromWindow() {
		missedWindow = true;
		GameObject [] g = GameObject.FindGameObjectsWithTag("Window");

		foreach(GameObject x in g) {
			//x.GetComponent<Animator>().Play("Window broken");
			//x.GetComponent<WindowBreak>().windowBroken = true;
			if(x.GetComponent<WindowBreak>().isBigWindow == false) {
				x.GetComponent<WindowBreak>().SetWindowAsBroken();
			}
		}

		if (lifeLost == false && immunity == false) {
			lifeLost = true;
			faceController.SetOhNoImage(2f);
			explode.Play();
			StartCoroutine(LoseALife());
		}
	}

	public void ReturnToChallengeMenu() {
		Time.timeScale = 1f;
		int i = MasterControl.instance.AddNumberOfChallenges();
		if(i == 36 && MasterControl.instance.sawTheEnding == 0) {
			MasterControl.instance.sawTheEnding = 1;
			PlayerPrefs.SetInt(GamePreferences.sawTheEnding, 1);
			SceneManager.LoadScene("You Win");
		} else {
			SceneManager.LoadScene("Challenges");
		}
	}

	public void TryAgain() {
		Time.timeScale = 1f;
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			SceneManager.LoadScene("Challenges Gameplay");
		} else {
			SceneManager.LoadScene("Gameplay");
		}
	}

	public void ReturnToFists() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Select A Fist");
	}

	public void SetHighScore() {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
			PlayerPrefs.SetFloat(GamePreferences.EasyDifficultyScore, score);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
			PlayerPrefs.SetFloat(GamePreferences.MediumDifficultyScore, score);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
			PlayerPrefs.SetFloat(GamePreferences.HardDifficultyScore, score);
		}
	}

	public void SetHighScoreScoreAttack() {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
			PlayerPrefs.SetFloat(GamePreferences.EasyDifficultyScoreScoreAttack, score);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
			PlayerPrefs.SetFloat(GamePreferences.MediumDifficultyScoreScoreAttack, score);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
			PlayerPrefs.SetFloat(GamePreferences.HardDifficultyScoreScoreAttack, score);
		}
	}

	public void SetSmileyBonus() {
		if(smileyBonus >= smileyMaxBonus) {
			smileyBonus = smileyMaxBonus;
		} else {
			smileyBonus += 10f;
		}
		//Debug.Log("Smiley Bonus is now: " + smileyBonus);
	}

	public void PowerUp(Powerup.PowerupType powerUp) {
		CancelPowerups();
		faceController.SetSmugImage(2f);
		AchievementsController.instance.SetPowerupCollected();
		switch(powerUp) {
			case Powerup.PowerupType.EXTRA_LIFE:
			Debug.Log("EXTRA LIFE!");
			lives++;
			UpdateLives();
			break;

			case Powerup.PowerupType.INVULNERNABILITY:
			Debug.Log("You are invulnerable... FOR NOW!");
			invincible = true;
			invincibleImage.SetActive(true);
			currentPowerup = Powerup.PowerupType.INVULNERNABILITY;
			windowSpawner.GetComponent<WindowSpawner>().SetPowerUpTimer();
			break;

			case Powerup.PowerupType.MONEYBAG:
			Debug.Log("Money falls from the sky.");
			moneybag = true;
			moneybagImage.SetActive(true);
			currentPowerup = Powerup.PowerupType.MONEYBAG;
			if(FistTable.instance.fistSpecial == 9) {
				windowSpawner.GetComponent<WindowSpawner>().SetPowerUpTimer(1 / FistTable.instance.fistPowerupsScale, BonusMoneyBag(windowSpawner.timeAlive));
			} else {
				windowSpawner.GetComponent<WindowSpawner>().SetPowerUpTimer(1, BonusMoneyBag(windowSpawner.timeAlive));
			}
			break;

			case Powerup.PowerupType.SLOW:
			Debug.Log("Slowing down the screen.");
			slowDown = true;
			slowDownImage.SetActive(true);
			currentPowerup = Powerup.PowerupType.SLOW;
			cameraScript.GetComponent<CameraScript>().SetSlowSpeed(true);
			windowSpawner.GetComponent<WindowSpawner>().SetPowerUpTimer();
			break;

			case Powerup.PowerupType.SUPER_SOCKO:
			Debug.Log("SUPER SOCKO TIME!!!!!");
			superSocko = true;
			superSockoImage.SetActive(true);
			currentPowerup = Powerup.PowerupType.SUPER_SOCKO;
			windowSpawner.GetComponent<WindowSpawner>().SetPowerUpTimer();
			break;
		}
	}

	public void CancelPowerups() {
		invincible = false;
		superSocko = false;
		slowDown = false;
		moneybag = false;
		invincibleImage.GetComponent<Image>().color = normalColor;
		superSockoImage.GetComponent<Image>().color = normalColor;
		slowDownImage.GetComponent<Image>().color = normalColor;
		moneybagImage.GetComponent<Image>().color = normalColor;
		invincibleImage.SetActive(false);
		superSockoImage.SetActive(false);
		slowDownImage.SetActive(false);
		moneybagImage.SetActive(false);
		cameraScript.GetComponent<CameraScript>().SetSlowSpeed(false);
		currentPowerup = Powerup.PowerupType.NONE;
	}

	public void CriticalPowerUp() {
		switch (currentPowerup) {
			case Powerup.PowerupType.INVULNERNABILITY:
			invincibleImage.GetComponent<Image>().color = criticalColor;
			break;

			case Powerup.PowerupType.SUPER_SOCKO:
			superSockoImage.GetComponent<Image>().color = criticalColor;
			break;

			case Powerup.PowerupType.SLOW:
			slowDownImage.GetComponent<Image>().color = criticalColor;
			break;

			case Powerup.PowerupType.MONEYBAG:
			moneybagImage.GetComponent<Image>().color = criticalColor;
			break;
		}
	}

	void CalculateBonusCoins() {
		float tempScore1, tempScore2;
		tempScore1 = score;
		tempScore2 = score;
		if(score >= 1000) {
			bonusCoinScore +=1;
		}

		if(score >= 5000) {
			bonusCoinScore +=1;
		}

		while (tempScore1 >= 10000) {
			bonusCoinScore +=1;
			tempScore1 -= 10000;
		}

		while(tempScore2 >= 50000) {
			bonusCoinScore +=3;
			tempScore2 -= 50000;
		}

		if(FistTable.instance.fistSpecial == 1) {
			bonusCoinScore += 2;
		}
	}

	public void CalculateFist20BonusCoins() {
		float tempScore = score;

		while (tempScore >= 10000) {
			bonusCoinScore +=1;
			tempScore -= 10000;
		}
	}

	public void WinChallengeCoins() {
		savedCoinScore += coinScore;
		savedCoinScore += bonusCoinScore;
		PlayerPrefs.SetInt(GamePreferences.CoinScore, savedCoinScore);
	}

	public void SetHitsBeforeGrunt() {
		if(score > 0) {
			grunt = true;
		}
		hitsBeforeGrunt = 15 + Random.Range(1, 6);
	}

	public void ToggleSound() {
		if(soundOn == 1) {
			Debug.Log("Turning sound off.");
			soundOn = 0;
			AudioListener.volume = 0;
			soundText.text = "Sound OFF";
			GamePreferences.SetMusicState(soundOn);
		} else {
			Debug.Log("Turning sound on.");
			soundOn = 1;
			AudioListener.volume = 1;
			soundText.text = "Sound ON";
			whapSound.Play();
			GamePreferences.SetMusicState(soundOn);
		}
	}

	public void SetHighScoreForLeaderboards() {
		if(MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
			if(MasterControl.instance.scoreAttack) {
				if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
					AchievementsController.instance.ReportScore(2, score);
				} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
					AchievementsController.instance.ReportScore(4, score);
				} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
					AchievementsController.instance.ReportScore(6, score);
				}
			} //Score Attack scores should qualify for the regular scores as well
			if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
				AchievementsController.instance.ReportScore(1, score);
			} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
				AchievementsController.instance.ReportScore(3, score);
			} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
				AchievementsController.instance.ReportScore(5, score);
			}
		}
	}

	float BonusMoneyBag(float t) {
		if(t < 30f) {
			return 0;
		} else {
			Debug.Log("Bonus moneybag time: " + (Mathf.Round((t-30)/15) + 1));
			return (Mathf.Round((t-30)/15) + 1);
		}
	}

	void CheckSoundText() {
		if(soundOn == 1) {
			soundText.text = "Sound ON";
		} else {
			soundText.text = "Sound OFF";
		}
	}

	public void SlowDownFromHittingBigWindow() {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
			cameraScript.SetCameraSpeed(cameraScript.currentSpeed * 0.6f);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
			cameraScript.SetCameraSpeed(cameraScript.currentSpeed * 0.7f);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
			cameraScript.SetCameraSpeed(cameraScript.currentSpeed * 0.8f);
		}
	}

	IEnumerator Recharge() {
		rechargeSocko = false;
		yield return new WaitForSeconds(timeToRecharge);
		rechargeSocko = true;
	}

	IEnumerator LoseALife() {
		if(missedWindow == false) {
			faceController.SetArghImage(2f);
		} else {
			missedWindow = false;
		}
		smileyBonus = 0f;
		cameraScript.moveCamera = false;
		explosionAnimator.Play("Explosion image");
		CancelPowerups();
		Debug.Log("You were alive for " + windowSpawner.timeAlive + " seconds.");
		windowSpawner.timeAlive = 0f;
		if (immunity == false && lives > 0) {
			StartCoroutine(ImmuneToBombsOrWindow());
		}
		yield return new WaitForSeconds(0.8f); 

		ReduceRapsberries();

		cameraScript.ResetSpeed();
		if(lives > 0) {
			cameraScript.moveCamera = true;
		}

		if(FistTable.instance.fistSpecial == 18 && MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE && lives > 0) {
			windowSpawner.RandomPowerUpForFist21();
		}
		lifeLost = false;
	}

	IEnumerator ImmuneToBombsOrWindow() {
		immunity = true;
		lives--;
		UpdateLives();
		yield return new WaitForSeconds (4f);
		immunity = false;
	}

	public float SmileyRapsberryBonus() {
		numberOfRapsberries++;
		if(numberOfRapsberries < 10) {
			return 300f;
		} else if(numberOfRapsberries < 20) {
			return 500f;
		} else if(numberOfRapsberries < 30) {
			return 1000f;
		} else if(numberOfRapsberries < 40) {
			return 2000f;
		} else if(numberOfRapsberries < 50) {
			return 3000f;
		} else if(numberOfRapsberries < 75) {
			return 5000f;
		} else if(numberOfRapsberries < 100f) {
			return 7500f;
		} else {
			return 10000f;
		}
	}

	void ReduceRapsberries() {
		numberOfRapsberries -=10;
		if(numberOfRapsberries < 0) {
			numberOfRapsberries = 0;
		}
	}

	// This function may not be needed
	/*
	public void ResetScreen() {
		destroyWindows = GameObject.FindGameObjectsWithTag("Window");
		destroyBombs = GameObject.FindGameObjectsWithTag("Bomb");

		foreach(GameObject g in destroyWindows){
			Destroy(g);
		}

		foreach(GameObject d in destroyBombs) {
			Destroy(d);
		}
	}*/
}
