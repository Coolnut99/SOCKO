using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FistStoreController : MonoBehaviour {

	[SerializeField]
	private Text coinScoreText, fistNumberText, fistAttackRate, fistRadius, fistPowerups, fistSpecialText, fistPrice, fistName;
	//Add private Text for the text values and have BuySelectedFist() adjust them based off fistNumber with the switch command

	[SerializeField]
	private GameObject purchaseCanvas, loadingCanvas;

	[SerializeField]
	private Button buyButton;

	//[SerializeField]
	//private Image [] fistImageArray;

	private int coinScore;

	public int selectFist;

	public int fistUnlocked;

	//public Image fistImage; //Adjust based on what fist is to be bought

	[SerializeField]
	private Image fistImageShown;

	[SerializeField]
	private Slider sliderBar;

	private bool loadScene;

	void Awake() {
		loadScene = false;
	}

	// Use this for initialization
	void Start () {
		purchaseCanvas.SetActive(false);
		loadingCanvas.SetActive(false);
		coinScoreText.text = "COINS: " + PlayerPrefs.GetInt(GamePreferences.CoinScore).ToString();
	}

	public void BuySelectedFist() {
		//fistImage = GameObject.Find("Fist Image " + selectFist.ToString()).GetComponent<Image>();
		//fistImage.color = GameObject.Find("Fist Image " + selectFist.ToString()).GetComponent<Image>().color;
		//fistImageShown.sprite = fistImageArray[selectFist - 1].gameObject.GetComponent<Image>().sprite;
		//fistImageShown.color = fistImageArray[selectFist - 1].gameObject.GetComponent<Image>().color;

		fistImageShown.sprite = GameObject.Find("Fist Image " + selectFist.ToString()).GetComponent<Image>().sprite;
		fistImageShown.color = GameObject.Find("Fist Image " + selectFist.ToString()).GetComponent<Image>().color;

		FistTable.instance.FistStatTable(selectFist);
		fistNumberText.text = "Fist Number " + selectFist.ToString();
		fistAttackRate.text = "Rate: " + FistTable.instance.fistRate.ToString();
		fistRadius.text = "Radius: " + FistTable.instance.fistRadiusScale.ToString();
		fistPowerups.text = "Powerups:\n" + FistTable.instance.fistPowerupsScale.ToString() + "x Length";
		fistPrice.text = "Cost: " + FistTable.instance.fistCost.ToString() + " Coins";
		FistTable.instance.FistSpecialSwitch(FistTable.instance.fistSpecial);
		fistSpecialText.resizeTextForBestFit = true;
		fistSpecialText.text = "SPECIAL: " + FistTable.instance.fistSpecialDescription;
		fistUnlocked = PlayerPrefs.GetInt("Fist_" + selectFist.ToString());
		fistName.text = "\"" + FistTable.instance.fistName + "\"";
		if(fistUnlocked == 1) {
			buyButton.interactable = false;
			fistPrice.text = "Already Purchased";
		} else {
			buyButton.interactable = true;
		}
		purchaseCanvas.SetActive(true);
	}


	public void MakePurchase() {
		coinScore = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		if(FistTable.instance.fistCost <= coinScore) {
			PlayerPrefs.SetInt(GamePreferences.fistName[selectFist-1], 1);
			coinScore -= FistTable.instance.fistCost;
			PlayerPrefs.SetInt(GamePreferences.CoinScore, coinScore);
			coinScoreText.text = "COINS: " + PlayerPrefs.GetInt(GamePreferences.CoinScore).ToString();
			buyButton.interactable = false;
			fistPrice.text = "THANK YOU!";
		} else {
			fistPrice.text = "NOT ENOUGH COINS!";
			//Debug.Log("Not enough coins!");
		}
	}
	public void ReturnToBuyFists() {
		GameObject [] g = GameObject.FindGameObjectsWithTag("Buy Fist");
		foreach (GameObject x in g) {
			x.GetComponent<FistLocked>().SetUnlock();
		}
		purchaseCanvas.SetActive(false);
	}

	public void SelectFistToUse() {
		if(loadScene == false) {
			loadScene = true;
			loadingCanvas.SetActive(true);
			if(fistUnlocked == 1) {
				FistTable.instance.SetFist(fistImageShown.sprite, fistImageShown.color);
				MasterControl.instance.scoreAttack = false;
				if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {			
					//SceneManager.LoadScene("Challenges Gameplay");
					StartCoroutine(LoadGameChallengeMode());
				} else {
					//SceneManager.LoadScene("Gameplay");
					StartCoroutine(LoadGame());
				}
			} else {
				Debug.LogWarning("Cannot select this now!");
				loadScene = false;
				loadingCanvas.SetActive(false);
			}
		}
	}

	public void SelectFistToUseScoreAttack() {
		if(loadScene == false) {
			loadScene = true;
			loadingCanvas.SetActive(true);
			if(selectFist == 1) {
				FistTable.instance.SetFist(fistImageShown.sprite, fistImageShown.color);
				MasterControl.instance.scoreAttack = true;
				if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {			
					//SceneManager.LoadScene("Challenges Gameplay");
					StartCoroutine(LoadGameChallengeMode());
				} else {
					//SceneManager.LoadScene("Gameplay");
					StartCoroutine(LoadGame());
				}
			} else {
				Debug.LogWarning("Cannot select this now!");
				loadScene = false;
				loadingCanvas.SetActive(false);
			}
		}
	}

	IEnumerator LoadGame() {
		AsyncOperation async = SceneManager.LoadSceneAsync("Gameplay");
		while(!async.isDone) {
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			sliderBar.value = progress;
			yield return null;
		}
	}

	IEnumerator LoadGameChallengeMode() {
		AsyncOperation async = SceneManager.LoadSceneAsync("Challenges Gameplay");
		while(!async.isDone) {
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			sliderBar.value = progress;
			yield return null;
		}
	}
}
