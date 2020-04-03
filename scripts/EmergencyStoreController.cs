using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmergencyStoreController : MonoBehaviour {

	[SerializeField]
	private GameObject emergencyStoreCanvas;

	[SerializeField]
	private Button invincibilityButton, superSockoButton, slowDownButton, heartButton;

	[SerializeField]
	private Text coinText, extraLifeText;

	[SerializeField]
	private GameController gameController;

	[SerializeField]
	private AudioSource purchase;

	float f;
	int lifeCost;

	// Use this for initialization
	void Start () {
		lifeCost = 25;
		f = 0f;
		extraLifeText.text = "EXTRA LIFE:\n" + lifeCost.ToString() + " Coins";
		if (MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			heartButton.interactable = false;
		}
		emergencyStoreCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(f > 0f) {
			f -= Time.unscaledDeltaTime;
		} else {
			coinText.resizeTextForBestFit = false;
			coinText.text = "Coins: " + gameController.GetComponent<GameController>().savedCoinScore.ToString();
			f = 0f;
		}
	}

	public void EnterEmergencyStoreMenu() {
		Time.timeScale = 0f;
		coinText.text = "Coins: " + gameController.GetComponent<GameController>().savedCoinScore.ToString();
		emergencyStoreCanvas.SetActive(true);
	}

	public void ExitEmergencyStoreMenu() {
		Time.timeScale = 1f;
		PlayerPrefs.SetInt(GamePreferences.CoinScore, gameController.GetComponent<GameController>().savedCoinScore);
		emergencyStoreCanvas.SetActive(false);
	}

	public void BuyInvincibility() {
		if(gameController.GetComponent<GameController>().savedCoinScore >= 5) {
			gameController.GetComponent<GameController>().savedCoinScore -= 5;
			gameController.PowerUp(Powerup.PowerupType.INVULNERNABILITY);
			AchievementsController.instance.SetPowerupBought();
			purchase.Play();
			ExitEmergencyStoreMenu();
		} else {
			f = 1f;
			coinText.text = "NOT ENOUGH COINS";
		}
	}

	public void BuySuperSocko() {
		if(gameController.GetComponent<GameController>().savedCoinScore >= 10) {
			gameController.GetComponent<GameController>().savedCoinScore -= 10;
			gameController.PowerUp(Powerup.PowerupType.SUPER_SOCKO);
			AchievementsController.instance.SetPowerupBought();
			purchase.Play();
			ExitEmergencyStoreMenu();
		} else {
			f = 1f;
			coinText.text = "NOT ENOUGH COINS";
		}
	}

	public void BuySlowDown() {
		if(gameController.GetComponent<GameController>().savedCoinScore >= 5) {
			gameController.GetComponent<GameController>().savedCoinScore -= 5;
			gameController.PowerUp(Powerup.PowerupType.SLOW);
			AchievementsController.instance.SetPowerupBought();
			purchase.Play();
			ExitEmergencyStoreMenu();
		} else {
			f = 1f;
			coinText.text = "NOT ENOUGH COINS";
		}
	}



	public void BuyHeart() {
		if(gameController.GetComponent<GameController>().savedCoinScore >= lifeCost) {
			gameController.GetComponent<GameController>().savedCoinScore -= lifeCost;
			gameController.PowerUp(Powerup.PowerupType.EXTRA_LIFE);
			lifeCost += 25;
			extraLifeText.text = "EXTRA LIFE:\n" + lifeCost.ToString() + " Coins";
			AchievementsController.instance.SetPowerupBought();
			purchase.Play();
			ExitEmergencyStoreMenu();
		} else {
			f = 1f;
			coinText.text = "NOT ENOUGH COINS";
		}
	}

	public void WatchUnityVideoToEarnBonusCoins() {
		AdsController.instance.ShowUnityAds();
	}

	public void VideoWatchedGiveBonusCoins() {
		gameController.savedCoinScore += 15;
		purchase.Play();
		f = 2f;
		coinText.resizeTextForBestFit = true;
		coinText.text = "+15 coins -- THANK YOU!";
	}

	public void VideoNotLoadedOrUserSkippedTheVideo() {
		f = 2f;
		coinText.resizeTextForBestFit = true;
		coinText.text = "VIDEO NOT FULLY WATCHED -- NO COINS AWARDED";
	}
}
