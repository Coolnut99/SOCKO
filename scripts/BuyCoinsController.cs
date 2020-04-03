using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCoinsController : MonoBehaviour {

	[SerializeField]
	private BuyCoinsText buyCoinsText;

	[SerializeField]
	private NoAdsController noAdsController;

	[SerializeField]
	private GameObject thankYouPanel;

	[SerializeField]
	private AudioSource coinSound;

	int coins;

	void Start() {
		thankYouPanel.SetActive(false);
	}

	public void Buy450Coins() {
		Debug.Log("Going to Buy 450 Coins at the app store.");
		BuyItem();
	}

	public void Buy1000Coins() {
		Debug.Log("Going to Buy 1000 Coins at the app store.");
		BuyItem();
	}

	public void Buy2750Coins() {
		Debug.Log("Going to Buy 2750 Coins at the app store.");
		BuyItem();
	}

	public void Buy6000Coins() {
		Debug.Log("Going to Buy 6000 Coins at the app store.");
		BuyItem();
	}

	public void Buy10000Coins() {
		Debug.Log("Going to Buy 10000 Coins at the app store.");
		BuyItem();
		//TEST
		//coins = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		//coins +=10000;
		//PlayerPrefs.SetInt(GamePreferences.CoinScore, coins);
		//End test
		//buyCoinsText.SetCoinsText();
	}

	public void BuyNoAds() {
		Debug.Log("Going to Buy No-Ads at the app store.");
		BuyItem();
	}

	public void BuyItem() {
		SocialMediaController.instance.Buy(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
		Debug.Log(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
	}

	public void ItemPurchased(string id) {
		
		switch(id) {
		case "socko_450_coins":
		coins = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		coins += 450;
		PlayerPrefs.SetInt(GamePreferences.CoinScore, coins);
		break;

		case "socko_1000_coins":
		coins = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		coins += 1000;
		PlayerPrefs.SetInt(GamePreferences.CoinScore, coins);
		break;

		case "socko_2750_coins":
		coins = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		coins += 2750;
		PlayerPrefs.SetInt(GamePreferences.CoinScore, coins);
		break;

		case "socko_6000_coins":
		coins = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		coins += 6000;
		PlayerPrefs.SetInt(GamePreferences.CoinScore, coins);
		break;

		case "socko_10000_coins":
		coins = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		coins += 10000;
		PlayerPrefs.SetInt(GamePreferences.CoinScore, coins);
		break;

		case "socko_remove_ads":
		MasterControl.instance.canShowAds = 0;
		PlayerPrefs.SetInt(GamePreferences.CanShowAds, 0);
		noAdsController.SetToNoAds();
		Debug.Log("No-ads bought. Thank you for your purchase!");
		break;

		}
		StartCoroutine(ShowThankYouPanel());
		buyCoinsText.SetCoinsText();
	}

	IEnumerator ShowThankYouPanel() {
		coinSound.Play();
		thankYouPanel.SetActive(true);
		yield return new WaitForSecondsRealtime(3f);
		thankYouPanel.SetActive(false);
	}
}
