using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreController : MonoBehaviour {

	[SerializeField]
	private Text coinText, buyLivesText, buyLivesLifeText, purchaseLivesText;
	private Color purchaseLivesTextColorOn, purchaseLivesTextColorOff;

	[SerializeField]
	private GameObject buyLivesCanvas;

	[SerializeField]
	private Animator buyLivesAnimator;

	[SerializeField]
	private Button buyLivesButton;

	[SerializeField]
	private AudioSource validateChoice;

	int lives;

	// Use this for initialization
	void Start () {
		lives = PlayerPrefs.GetInt(GamePreferences.MaxLives);
		coinText.text = "COINS: " + PlayerPrefs.GetInt(GamePreferences.CoinScore).ToString();
		buyLivesCanvas.SetActive(false);
		purchaseLivesTextColorOn = purchaseLivesText.color;
		purchaseLivesTextColorOff = new Color(purchaseLivesTextColorOn.r, purchaseLivesTextColorOn.g, purchaseLivesTextColorOn.b, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BuyLivesEnter() {
		validateChoice.Play();
		//Debug.Log("Going to Buy Lives Screen.");
		if (lives < 9) {
			buyLivesButton.interactable = true;
			purchaseLivesText.color = purchaseLivesTextColorOn;
			buyLivesCanvas.SetActive(true);

			buyLivesLifeText.text = "LIVES: " + lives.ToString();

			buyLivesText.text = "You can increase your max lives to " + (lives+1).ToString() + " for the low, low, LOW price of only " + SetLifeCost() + " coins!\n\n";
			buyLivesText.text += "YOU HAVE:\n" + PlayerPrefs.GetInt(GamePreferences.CoinScore).ToString() + " COINS";
		} else {	
			buyLivesButton.interactable = false;
			purchaseLivesText.color = purchaseLivesTextColorOff;
			buyLivesCanvas.SetActive(true);

			buyLivesLifeText.text = "LIVES: " + lives.ToString();

			buyLivesText.text = "You already have the maximum amount of lives. NO MORE FOR YOU!!!!!";
		}
		buyLivesAnimator.Play("Panel Appear");
	}

	public void BuyLivesExit() {
		validateChoice.Play();
		buyLivesCanvas.SetActive(false);
	}

	public void BuyLivesPurchase() {
		int i = SetLifeCost();
		int tempCoin = PlayerPrefs.GetInt(GamePreferences.CoinScore);

		if (tempCoin < i) {
			//Debug.Log("Not enough coins!");
			buyLivesText.text = "NOT ENOUGH COINS!\n\nPlay the game to get more, or buy them from the App store.";
			buyLivesButton.interactable = false;
			purchaseLivesText.color = purchaseLivesTextColorOff;
		} else {
			tempCoin -= i;
			PlayerPrefs.SetInt(GamePreferences.CoinScore, tempCoin);
			lives++;
			PlayerPrefs.SetInt(GamePreferences.MaxLives, lives);

			buyLivesButton.interactable = false;
			purchaseLivesText.color = purchaseLivesTextColorOff;

			buyLivesLifeText.text = "LIVES: " + lives.ToString();

			buyLivesText.text = "THANK YOU!";

			coinText.text = "COINS: " + PlayerPrefs.GetInt(GamePreferences.CoinScore).ToString();
		}
	}

	public void BuyFists() {
		validateChoice.Play();
		SceneManager.LoadScene("Buy Fists");
	}

	public void BuyCoins() {
		validateChoice.Play();
		SceneManager.LoadScene("Buy Coins");
	}

	public void WatchAnAdForCoins() {
		AdsController.instance.ShowChartboostChartboostVideo();
	}

	public void FailedToLoadTheVideo() {
		Debug.Log("Could not load video.");
	}

	public void VideoNotLoadedOrUserSkippedTheVideo() {
		Debug.Log("Video not loaded, or user skipped the video. No credit given. Please try again.");
	}

	public void WatchedVideoGiveAReward() {
		int tempCoin = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		tempCoin += 15;
		coinText.text = "COINS: " + tempCoin.ToString();
		PlayerPrefs.SetInt(GamePreferences.CoinScore, tempCoin);
		Debug.Log("You have earned 15 coins for watching a video.");
	}

	int SetLifeCost() {
		switch(lives){
			case 3:
			return 200;
			break;

			case 4:
			return 500;
			break;

			case 5:
			return 1000;
			break;

			case 6:
			return 2000;
			break;

			case 7:
			return 4000;
			break;

			case 8:
			return 9999;
			break;

			case 9:
			return 99999;
			break;

			default:
			Debug.LogWarning("Lives out of range -- Resetting to 3 lives.");
			PlayerPrefs.SetInt(GamePreferences.MaxLives, 3);
			lives = PlayerPrefs.GetInt(GamePreferences.MaxLives);
			return 0;
			break;
		}
	}
}
