using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSpawner : MonoBehaviour {

	//Spawns windows, bombs, smiley faces, and coins
	[SerializeField]
	private GameObject bomb, smiley, coin, giantBomb; //Add bombs on window sills and coins later
	private GameObject windowClone, bombClone, smileyClone, coinClone, giantBombClone;

	[SerializeField]
	private GameObject[] window; //Random windows, size can be adjusted

	[SerializeField]
	private GameObject[] bigWindow; // Summons big windows
	private GameObject bigWindowClone;

	[SerializeField]
	private GameObject extraLife, superSocko, invulnerability, slowDown, moneyBag; // Add others here later
	GameObject powerUp;	//The powerup that will drop

	[SerializeField]
	private GameController gameController;

	[SerializeField]
	private CameraScript cameraScript;

	private int difficulty; // Difficulty goes from 1 to 10 and bombs, smileys, and coins all increase on higher difficulties.

	private float minX, maxX; //Minimum and maximum X to avoid having an object spawn too far left or right

	private float minVelocity, maxVelocity; // Min and max velocity for dropping bombs

	private float controlX;

	private float timeForWindow, timeForSmiley, timeforBomb, timeForCoin, timeForPowerup, timeForBonusPowerUp, timeforCoinMoneybag; // Time before one of these spawn -- timeForWindow is old
	private float timeLeftForWindow, timeLeftForSmiley, timeLeftForBomb, timeLeftForCoin, timeLeftForPowerup, timeLeftForBonusPowerup, timeLeftForCoinMoneybag; // Time remaining before one of these spawn
	private float timePlayed;
	private float checkDifficulty;
	private float gameDifficulty;

	private float maxDifficultyWindow, maxDifficultyBomb, maxDifficultySmiley;

	private float cameraSpeed, cameraAcceleration, cameraMaxSpeed;

	public float giantWindowDistance; //This decreases as more windows and smileys are hit
	public float giantWindowValue;

	private int numberOfWindows, numberOfBombs;

	private float powerupTime, powerupTimeLeft, powerupTimeMultiplier;

	public float beforeWindowY; //TEST
	public float timeLeftBeforeWindowY; //TEST

	public float timeAlive;	//Time player is alive; high numbers allow more coins and bombs to drop.

	public float timeforGiantBomb, timeLeftForGiantBomb;


	// Use this for initialization
	void Awake () {
		SetMinAndMaxX();
		timeForWindow = 2f; // OLD
		timeLeftForWindow = 2f;
		timeForSmiley = 3f;
		timeLeftForSmiley = 3f;
		timeforBomb = 0.5f;
		timeLeftForBomb = 0.5f;
		timeforGiantBomb = 3f;
		timeLeftForGiantBomb = 3f;
		timeForCoin = 3f;
		timeLeftForCoin = 3f;
		timeForPowerup = 7f;
		timeLeftForPowerup = 7f;
		difficulty = 1;
		numberOfWindows = 3;
		numberOfBombs = 1;
		timePlayed = 0f;

		maxDifficultyBomb = 0.07f;
		maxDifficultySmiley = 0.5f;
		maxDifficultyWindow = 1f;

		powerupTime = 7f;
		powerupTimeLeft = 0f;

		timeforCoinMoneybag = 1f;
		timeLeftForCoinMoneybag = 1f;

		beforeWindowY = 3f; //TEST
		giantWindowDistance = 1000f;
	}

	void Start() {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			AdjustDifficultyChallenge(timePlayed, MasterControl.instance.challengeNumber);
			gameDifficulty = 1.0f;
		}

		if(MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
			AdjustDifficulty(timePlayed);
			checkDifficulty = 30f;
			if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
				gameDifficulty = 1.3f;
			} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
				gameDifficulty = 1.0f;
			} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
				gameDifficulty = 0.8f;
			}
		}

		if(FistTable.instance.fistSpecial == 10) {
			timeForBonusPowerUp = 30f;
		} else {
			timeForBonusPowerUp = 99999f;
		}

		if(FistTable.instance.fistSpecial == 7) {
			powerupTimeMultiplier = 2f;
		} else {
			powerupTimeMultiplier = 1f;
		}

		timeLeftForBonusPowerup = timeForBonusPowerUp;
		timeLeftForWindow = timeForWindow * gameDifficulty; // OLD
		timeLeftForSmiley = timeForSmiley * gameDifficulty;
		timeLeftForCoin = timeForCoin * gameDifficulty;
		timeLeftForBomb = timeforBomb * gameDifficulty;
		timeLeftForPowerup = (timeForPowerup * powerupTimeMultiplier) / gameDifficulty;
		timeLeftBeforeWindowY = beforeWindowY * gameDifficulty; //TEST

		gameController.criticalPowerUp = powerupTime * gameDifficulty / 3;
	}

	void FixedUpdate() {
		if(MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE && checkDifficulty <= 0f) {
			AdjustDifficulty(timePlayed);
			checkDifficulty = 30f;
		}

		if (timeLeftForCoin <= 0f) {
			CreateCoin();
			timeLeftForCoin = timeForCoin * gameDifficulty;
		}

		if(timeLeftForCoinMoneybag <= 0f) {
			CreateCoin();
			timeLeftForCoinMoneybag = (timeforCoinMoneybag * gameDifficulty * 0.8f);
		}

		if (timeLeftForPowerup <= 0f && MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
			float tempX = Random.Range(minX, maxX);
			RandomPowerUp();
			Instantiate(powerUp, new Vector3(tempX, transform.position.y, 0f), Quaternion.identity);
			timeLeftForPowerup = ((timeForPowerup * powerupTimeMultiplier) / gameDifficulty) + Random.Range(-2f, 2f) + AdjustPowerUpTime();
		}

		if (timeLeftForBonusPowerup <= 0f) {
			float tempX = Random.Range(minX, maxX);
			RandomPowerUp();
			Debug.Log("Bonus powerup appears.");
			Instantiate(powerUp, new Vector3(tempX, transform.position.y, 0f), Quaternion.identity);
			timeLeftForBonusPowerup = timeForBonusPowerUp + Random.Range(-3f, 3f) + AdjustPowerUpTime();
		}


		if (powerupTimeLeft >= 0f) {
				powerupTimeLeft -= Time.deltaTime;
			} else {
				powerupTimeLeft = 0f;
				gameController.CancelPowerups();
		}

		if(powerupTimeLeft < gameController.criticalPowerUp && powerupTimeLeft > 0f) {
			gameController.CriticalPowerUp();
		}
	}

	// Update is called once per frame
	void Update () {
		if(gameController.lifeLost == false && gameController.gameStarted == true) {
			timeLeftForWindow -= Time.deltaTime;
			timeLeftForBomb -= Time.deltaTime;
			timeLeftForSmiley -= Time.deltaTime;
			timeLeftForCoin -= Time.deltaTime;
			timeLeftForPowerup -= Time.deltaTime;
			timeLeftBeforeWindowY -= cameraScript.beforeWindowY; //TEST
			cameraScript.beforeWindowY = 0; // TEST
			timeLeftForBonusPowerup -= Time.deltaTime;
			if(timeAlive > 57f && MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
				timeLeftForGiantBomb -= Time.deltaTime;
			} else {
				timeLeftForGiantBomb = timeforGiantBomb;
			}

			if(gameController.moneybag == true) {
				timeLeftForCoinMoneybag -= Time.deltaTime;
			}

			timeAlive += Time.deltaTime;
			timePlayed += Time.deltaTime;
			checkDifficulty -= Time.deltaTime;

		}

		if(giantWindowDistance <= 0f && MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
			timeLeftBeforeWindowY += 7f; //Adjust later
			CreateGiantWindow();
			giantWindowDistance = 1000f + (140 * gameController.numberOfBigWindowsHit / gameDifficulty);
			Debug.Log(giantWindowDistance);
		}

		if (timeLeftBeforeWindowY <= 0/*timeLeftForWindow <= 0*/) { //TEST
			CreateWindow();
			//timeLeftForWindow = (timeForWindow + Random.Range((timeForWindow / -10), (timeForWindow / 10))) * gameDifficulty; //Uncomment this if test does not work // OLD
			timeLeftBeforeWindowY = (beforeWindowY + Random.Range((beforeWindowY / -10), (beforeWindowY / 10))) * gameDifficulty; //TEST
			//Adjust difficulty based on time
		}

		if (timeLeftForBomb <= 0) {
			DropBomb(numberOfBombs);
			timeLeftForBomb = (timeforBomb + Random.Range((timeforBomb / -10f), (timeforBomb / 10f))) * gameDifficulty;
		}

		if(timeLeftForGiantBomb <= 0) {
			DropGiantBomb();
			timeLeftForGiantBomb = timeforGiantBomb - AdjustGiantBombTime();
		}



		if(timeLeftForSmiley <= 0f) {
			CreateSmiley();
			if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
				timeLeftForSmiley = (timeForSmiley + Random.Range((timeForSmiley / -10f), (timeForSmiley / 10f))) * gameDifficulty;
			} else {
				float f = timePlayed / 300f;
				timeLeftForSmiley = (timeForSmiley - f + Random.Range((timeForSmiley / -10f), (timeForSmiley / 10f))) * gameDifficulty;
				if(timeLeftForSmiley < maxDifficultySmiley) {
					timeLeftForSmiley = maxDifficultySmiley;
				}
			}
			//timeLeftForWindow += 0.5f;
		}
	}

	void SetMinAndMaxX() {
		Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

		maxX = bounds.x - 0.25f;
		minX = -bounds.x + 0.25f;
	}

	void CreateWindow() {
		float tempX = Random.Range(minX, maxX);
		int tempWindow = Random.Range(0, window.Length);
		Debug.Log(tempX);
		windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
		windowClone.transform.position = new Vector3(tempX, transform.position.y, 0f);


		// For two windows, if tempX is greater than 0, create a window to the left, otherwise one to the right
		/* For three windows:
		 If tempX is less than -1, create two windows to the right: one from 0 to tempX plus a fudge factor, the other from minX to tempX plus the same
		 If tempX greater than 1, create two windows to the left: one from tempX to 0 minus a fudge factor, the other from tempX to maxX the same
		 In between, create one window to the left and one to the right

		*/
		if (numberOfWindows == 2) {
			if (tempX > 0f) {
				float temp2X = Random.Range(minX, tempX - 0.5f);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
			} else if (tempX <= 0f) {
				float temp2X = Random.Range(tempX + 0.5f, maxX);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
			}
		} else if (numberOfWindows >= 3) {
			if (tempX < -1f) {
				float temp2X = Random.Range(1f, maxX);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
				temp2X = Random.Range(-0.5f, 0.5f);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
			} else if (tempX > 1f) {
				float temp2X = Random.Range(minX, -1f);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
				temp2X = Random.Range(-0.5f, 0.5f);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
			} else {
				float temp2X = Random.Range(minX, tempX - 0.5f);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
				temp2X = Random.Range(tempX + 0.5f, maxX);
				windowClone = Instantiate(window[tempWindow], transform.position, Quaternion.identity) as GameObject;
				windowClone.transform.position = new Vector3(temp2X, transform.position.y, 0f);
			}
		}
		//timeLeftForSmiley += (0.7f - cameraScript.GetComponent<CameraScript>().currentSpeed/3) ;
		//beforeWindowY = cameraScript.GetComponent<CameraScript>().beforeWindowY;
		//cameraScript.GetComponent<CameraScript>().beforeWindowY = 0f;
	}

	void CreateGiantWindow() {
		float tempX = Random.Range(minX + 1f, maxX - 1f);
		int tempWindow = Random.Range(0, bigWindow.Length);
		Debug.Log(tempX);
		bigWindowClone = Instantiate(bigWindow[tempWindow], transform.position, Quaternion.identity) as GameObject;
		bigWindowClone.transform.position = new Vector3(tempX, transform.position.y + 5f, 0f);
	}

	void CreateSmiley() {
		float smileX = Random.Range(minX, maxX);
		smileyClone = Instantiate(smiley, transform.position, Quaternion.identity) as GameObject;
		smileyClone.transform.position = new Vector3(smileX, transform.position.y, 0f);
	}

	void CreateCoin() {
		float coinX = Random.Range(minX, maxX);
		coinClone = Instantiate(coin, transform.position, Quaternion.identity) as GameObject;
		coinClone.transform.position = new Vector3(coinX, transform.position.y, 0f);
	}

	void DropBomb(int n) {
		for (int i = 0; i < n; i++) {
			bombClone = Instantiate(bomb, transform.position, Quaternion.identity) as GameObject;
			bombClone.transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y + Random.Range(0f, 0.1f), 0f);
		}
	}

	void DropGiantBomb() {
		Debug.Log("A giant bomb is dropping!!!! LOOK OUT!!!!!");
		giantBombClone = Instantiate(giantBomb, transform.position, Quaternion.identity) as GameObject;
		giantBombClone.transform.position = new Vector3(Random.Range(minX + 0.5f, maxX-0.5f), transform.position.y, 0f);
	}

	float AdjustGiantBombTime() {
		float f = (timeAlive - 60f)/60f;
		if(f < 0) {
			return 0;
		} else if (f > 2) {
			return 2;
		} else {
			return f;
		}
	}

	float AdjustPowerUpTime() {
		float f = (timePlayed - 300f)/30f;
		if(f < 0) {
			return 0;
		} else if (f > 30) {
			return 30;
		} else {
			return f;
		}
	}

	//Adjust difficulty here
	public void AdjustDifficulty(float t) {
		int i;
		if (t <= 30f) {
			i = 0;
		} else if (t > 30f && t <= 60f) {
			i = 1;
		} else if (t > 60f && t <= 90f) {
			i = 2;
		} else if (t > 90f && t <= 120f) {
			i = 3;
		} else if (t > 120f && t <= 150f) {
			i = 4;
		} else if (t > 150f && t <= 180f) {
			i = 5;
		} else if (t > 180f && t <= 210f) {
			i = 6;
		} else if (t > 210f && t <= 240f) {
			i = 7;
		} else if (t > 240f && t <= 270f) {
			i = 8;
		} else if (t > 270f && t <= 300f) {
			i = 9;
		} else {
			i = 10;
		}

		switch (i) {

			case 0:
			beforeWindowY = 3f;
			timeForWindow = 2f;
			timeForSmiley = 3f;
			timeforBomb = 0.5f;
			timeForCoin = 15f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			break;

			case 1:
			beforeWindowY = 2.7f;
			timeForWindow = 1.5f;
			timeForSmiley = 2.8f;
			timeforBomb = 0.45f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			break;

			case 2:
			beforeWindowY = 2.5f;
			timeForWindow = 2f;
			timeForSmiley = 2.6f;
			timeforBomb = 0.4f;
			timeForCoin = 12f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			break;

			case 3:
			beforeWindowY = 4f;
			timeForWindow = 1.75f;
			timeForSmiley = 2.6f;
			timeforBomb = 0.35f;
			timeForCoin = 11f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			break;

			case 4:
			beforeWindowY = 3.5f;
			timeForWindow = 1.5f;
			timeForSmiley = 2.5f;
			timeforBomb = 0.3f;
			timeForCoin = 11f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			break;

			case 5:
			beforeWindowY = 3f;
			timeForWindow = 1.4f;
			timeForSmiley = 2.5f;
			timeforBomb = 0.45f;
			timeForCoin = 10f;
			numberOfWindows = 2;
			numberOfBombs = 2;
			break;

			case 6:
			beforeWindowY = 2.8f;
			timeForWindow = 1.3f;
			timeForSmiley = 2.5f;
			timeforBomb = 0.4f;
			timeForCoin = 10f;
			numberOfWindows = 2;
			numberOfBombs = 2;
			break;

			case 7:
			beforeWindowY = 4f;
			timeForWindow = 2f;
			timeForSmiley = 2.4f;
			timeforBomb = 0.3f;
			timeForCoin = 9f;
			numberOfWindows = 3;
			numberOfBombs = 2;
			break;

			case 8:
			beforeWindowY = 3.6f;
			timeForWindow = 1.6f;
			timeForSmiley = 2.3f;
			timeforBomb = 0.4f;
			timeForCoin = 8f;
			numberOfWindows = 3;
			numberOfBombs = 3;
			break;

			case 9:
			beforeWindowY = 3.2f;
			timeForWindow = 1.1f;
			timeForSmiley = 2.1f;
			timeforBomb = 0.3f;
			timeForCoin = 7f;
			numberOfWindows = 3;
			numberOfBombs = 3;
			break;

			case 10:
			beforeWindowY = 2.8f;
			timeForWindow = 1f;
			timeForSmiley = 2f;
			timeforBomb = 0.25f;
			timeForCoin = 6f;
			numberOfWindows = 3;
			numberOfBombs = 4;
			break;

			default:
			beforeWindowY = 3f;
			timeForWindow = 2f;
			timeForSmiley = 3f;
			timeforBomb = 0.5f;
			timeForCoin = 15f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			break;

		}
	}

	public void AdjustDifficultyChallenge(float t, int chal) {
		switch (chal) {
			case 1:
			beforeWindowY = 3f;
			timeForWindow = 2f;
			timeForSmiley = 3f;
			timeforBomb = 0.5f;
			timeForCoin = 15f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			break;

			case 2:
			beforeWindowY = 21f;
			timeForWindow = 15f;
			timeForSmiley = 1f;
			timeforBomb = 0.45f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(2f, 0f, 10f);
			break;

			case 3:
			beforeWindowY = 2.1f;
			timeForWindow = 1.5f;
			timeForSmiley = 0.4f;
			timeforBomb = 0.45f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.1f, 10f);
			break;

			case 4:
			beforeWindowY = 1.5f;
			timeForWindow = 1f;
			timeForSmiley = 1.8f;
			timeforBomb = 0.5f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.1f, 10f);
			break;

			case 5:
			beforeWindowY = 3f;
			timeForWindow = 2f;
			timeForSmiley = 1.65f;
			timeforBomb = 0.45f;
			timeForCoin = 13f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.1f, 10f);
			break;

			case 6:
			beforeWindowY = 20f;
			timeForWindow = 2f;
			timeForSmiley = 0.4f;
			timeforBomb = 0.5f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(10f, 0f, 10f);
			break;

			case 7:
			beforeWindowY = 8f;
			timeForWindow = 5f;
			timeForSmiley = 1f;
			timeforBomb = 0.38f;
			timeForCoin = 13f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(5f, 0.1f, 13f);
			break;

			case 8:
			beforeWindowY = 2.1f;
			timeForWindow = 1.5f;
			timeForSmiley = 1.4f;
			timeforBomb = 0.38f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(2f, 0.1f, 13f);
			break;

			case 9:
			beforeWindowY = 2.1f;
			timeForWindow = 1.5f;
			timeForSmiley = 1.4f;
			timeforBomb = 0.35f;
			timeForCoin = 13f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.1f, 10f);
			break;

			case 10:
			beforeWindowY = 100f;
			timeForWindow = 5f;
			timeForSmiley = 0.34f;
			timeforBomb = 0.3f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(15f, 0f, 20f);
			break;

			case 11:
			beforeWindowY = 2.1f;
			timeForWindow = 1.5f;
			timeForSmiley = 1.4f;
			timeforBomb = 1f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.04f, 10f);
			break;

			case 12:
			beforeWindowY = 60f;
			timeForWindow = 8f;
			timeForSmiley = 1f;
			timeforBomb = 0.35f;
			timeForCoin = 13f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(8f, 0f, 12f);
			break;

			case 13:
			beforeWindowY = 4f;
			timeForWindow = 2.2f;
			timeForSmiley = 1.15f;
			timeforBomb = 0.3f;
			timeForCoin = 13f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.08f, 10f);
			break;

			case 14:
			beforeWindowY = 7f;
			timeForWindow = 1f;
			timeForSmiley = 0.4f;
			timeforBomb = 0.4f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(12f, 0f, 12f);
			break;

			case 15:
			beforeWindowY = 80f;
			timeForWindow = 5f;
			timeForSmiley = 0.34f;
			timeforBomb = 0.3f;
			timeForCoin = 13f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(14f, 0.1f, 20f);
			break;

			case 16:
			beforeWindowY = 3f;
			timeForWindow = 2f;
			timeForSmiley = 1.55f;
			timeforBomb = 0.8f;
			timeForCoin = 13f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.03f, 10f);
			break;

			case 17:
			beforeWindowY = 3000f;
			timeForWindow = 12f;
			timeForSmiley = 0.8f;
			timeforBomb = 0.25f;
			timeForCoin = 13f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(2f, 0.2f, 10f);
			break;

			case 18:
			beforeWindowY = 2.8f;
			timeForWindow = 1.6f;
			timeForSmiley = 1.55f;
			timeforBomb = 0.8f;
			timeForCoin = 10f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.05f, 10f);
			break;

			case 19:
			beforeWindowY = 9999f;
			timeForWindow = 1.0f;
			timeForSmiley = 0.5f;
			timeforBomb = 0.6f;
			timeForCoin = 9f;
			numberOfWindows = 1;
			numberOfBombs = 2;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(4f, 0.1f, 10f);
			break;

			case 20:
			beforeWindowY = 3f;
			timeForWindow = 2f;
			timeForSmiley = 0.6f;
			timeforBomb = 0.3f;
			timeForCoin = 9f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(3f, 0.01f, 10f);
			break;

			case 21:
			beforeWindowY = 3f;
			timeForWindow = 2f;
			timeForSmiley = 2f;
			timeforBomb = 1f;
			timeForCoin = 9f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(2f, 0f, 10f);
			break;

			case 22:
			timeForWindow = 2f;
			timeForSmiley = 3f;
			timeforBomb = 0.3f;
			timeForCoin = 9f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(3f, 0f, 10f);
			break;

			case 23:
			timeForWindow = 1f;
			timeForSmiley = 0.2f;
			timeforBomb = 0.4f;
			timeForCoin = 9f;
			numberOfWindows = 3;
			numberOfBombs = 3;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(4f, 0f, 10f);
			break;

			case 24:
			beforeWindowY = 20f;
			timeForWindow = 1f;
			timeForSmiley = 0.5f;
			timeforBomb = 0.3f;
			timeForCoin = 9f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(10f, 0f, 10f);
			break;

			case 25:
			timeForWindow = 2f;
			timeForSmiley = 0.3f;
			timeforBomb = 0.3f;
			timeForCoin = 9f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(10f, 0f, 10f);
			break;

			case 26:
			timeForWindow = 2f;
			timeForSmiley = 3f;
			timeforBomb = 0.2f;
			timeForCoin = 9f;
			numberOfWindows = 1;
			numberOfBombs = 3;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.02f, 10f);
			break;

			case 27:
			timeForWindow = 3f;
			timeForSmiley = 1.5f;
			timeforBomb = 3f;
			timeForCoin = 8f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.1f, 10f);
			break;

			case 28:
			timeForWindow = 2.6f;
			timeForSmiley = 1.4f;
			timeforBomb = 2f;
			timeForCoin = 8f;
			numberOfWindows = 2;
			numberOfBombs = 2;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.1f, 10f);
			break;

			case 29:
			timeForWindow = 3f;
			timeForSmiley = 1.5f;
			timeforBomb = 11f;
			timeForCoin = 8f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(3f, 0f, 10f);
			break;

			case 30:
			beforeWindowY = 60f;
			timeForWindow = 4f;
			timeForSmiley = 0.3f;
			timeforBomb = 0.1f;
			timeForCoin = 6f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(15f, 0f, 20f);
			break;

			case 31:
			timeForWindow = 2f;
			timeForSmiley = 0.2f;
			timeforBomb = 0.1f;
			timeForCoin = 8f;
			numberOfWindows = 1;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(2f, 0.2f, 10f);
			break;

			case 32:
			timeForWindow = 1.3f;
			timeForSmiley = 2.2f;
			timeforBomb = 0.4f;
			timeForCoin = 6f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.01f, 10f);
			break;

			case 33:
			timeForWindow = 1.3f;
			timeForSmiley = 2.2f;
			timeforBomb = 0.5f;
			timeForCoin = 7f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.04f, 10f);
			break;

			case 34:
			beforeWindowY = 6f;
			timeForWindow = 1.3f;
			timeForSmiley = 2f;
			timeforBomb = 0.3f;
			timeForCoin = 6f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.012f, 10f);
			break;

			case 35:
			timeForWindow = 1.3f;
			timeForSmiley = 2.2f;
			timeforBomb = 0.34f;
			timeForCoin = 6f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.045f, 10f);
			break;

			case 36:
			beforeWindowY = 6f;
			timeForWindow = 1.3f;
			timeForSmiley = 2f;
			timeforBomb = 0.3f;
			timeForCoin = 5f;
			numberOfWindows = 2;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.005f, 10f);
			break;

			default:
			timeForWindow = 5f;
			timeForSmiley = 0.5f;
			timeforBomb = 0.5f;
			timeForCoin = 15f;
			numberOfWindows = 3;
			numberOfBombs = 1;
			cameraScript.GetComponent<CameraScript>().SetCameraSpeed(1f, 0.1f, 10f);
			break;


		}
	}

	public void RandomPowerUp() {
		float i = Random.Range(0, 100);

		if (gameController.lives < 3) {
			if(timePlayed > 400f) {
				//Hard powerups
				if(i > 97) {
					//Spawn extra life
					powerUp = extraLife;
				} else if (i > 85) {
					//Spawn invulnerability
					powerUp = invulnerability;
				} else if (i > 70) {
					//Spawn SUPER SOCKO
					powerUp = superSocko;
				} else if (i > 50) {
					//Spawn slowdown
					powerUp = slowDown;
				} else {
					//Spawn moneybag
					powerUp = moneyBag;
				}
			} else if (timePlayed > 200f) {
				//Normal powerups
				if(i > 95) {
					//Spawn extra life
					powerUp = extraLife;
				} else if (i > 80) {
					//Spawn invulnerability
					powerUp = invulnerability;
				} else if (i > 55) {
					//Spawn SUPER SOCKO
					powerUp = superSocko;
				} else if (i > 35) {
					//Spawn slowdown
					powerUp = slowDown;
				} else {
					//Spawn moneybag
					powerUp = moneyBag;
				}

			} else {
				//Easy powerups
				if(i > 90) {
					//Spawn extra life
					powerUp = extraLife;
				} else if (i > 75) {
					//Spawn invulnerability
					powerUp = invulnerability;
				} else if (i > 50) {
					//Spawn SUPER SOCKO
					powerUp = superSocko;
				} else if (i > 25) {
					//Spawn slowdown
					powerUp = slowDown;
				} else {
					//Spawn moneybag
					powerUp = moneyBag;
				}
			}
		} else {
			//Do NOT spawn an extra life here
			if(timePlayed > 400f) {
				//Hard powerups
				if (i > 85) {
					//Spawn invincibility
					powerUp = invulnerability;
				} else if (i > 70) {
					//Spawn SUPER SOCKO
					powerUp = superSocko;
				} else if (i > 50) {
					//Spawn slowdown
					powerUp = slowDown;
				} else {
					//Spawn moneybag
					powerUp = moneyBag;
				}
			} else if (timePlayed > 200f) {
				//Normal powerups
				if (i > 80) {
					//Spawn invincibility
					powerUp = invulnerability;
				} else if (i > 60) {
					//Spawn SUPER SOCKO
					powerUp = superSocko;
				} else if (i > 40) {
					//Spawn slowdown
					powerUp = slowDown;
				} else {
					//Spawn moneybag
					powerUp = moneyBag;
				}
			} else {
				//Easy powerups
				if (i > 75) {
					//Spawn invincibility
					powerUp = invulnerability;
				} else if (i > 50) {
					//Spawn SUPER SOCKO
					powerUp = superSocko;
				} else if (i > 25) {
					//Spawn slowdown
					powerUp = slowDown;
				} else {
					//Spawn moneybag
					powerUp = moneyBag;
				}

			}
		}
	}

	public void SetPowerUpTimer() {
		powerupTimeLeft = powerupTime * gameDifficulty * FistTable.instance.fistPowerupsScale;
	}

	public void SetPowerUpTimer(float multiplier) {
		powerupTimeLeft = powerupTime * gameDifficulty * FistTable.instance.fistPowerupsScale * multiplier;
	}

	public void SetPowerUpTimer(float multiplier, float additionalTime) {
		powerupTimeLeft = (powerupTime * gameDifficulty * FistTable.instance.fistPowerupsScale * multiplier) + additionalTime;
	}

	public void RandomPowerUpForFist21() {
		float tempX = Random.Range(minX, maxX);
		RandomPowerUp();
		Instantiate(powerUp, new Vector3(tempX, transform.position.y, 0f), Quaternion.identity);
	}
}
