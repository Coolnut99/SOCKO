using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAccessController : MonoBehaviour {

	int challengeSet1, challengeSet2, challengeSet3, challengeSet4, challengeSet5, challengeSet6;
	int coins;

	[SerializeField]
	private Button buyChallengeButton1, buyChallengeButton2, buyChallengeButton3, buyChallengeButton4, buyChallengeButton5, buyChallengeButton6;

	[SerializeField]
	private Text coinText, buyChallengeButton1Text, buyChallengeButton2Text, buyChallengeButton3Text, buyChallengeButton4Text, buyChallengeButton5Text, buyChallengeButton6Text;

	[SerializeField]
	private int [] buyChallengeCost;

	float textTimer;

	// Use this for initialization
	void Start () {
		textTimer = 0f;
		CheckChallenges();
		coins = PlayerPrefs.GetInt(GamePreferences.CoinScore);
		coinText.text = "COINS: " + coins.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(textTimer > 0f) {
			coinText.text = "NOT ENOUGH COINS!";
			textTimer -= Time.deltaTime;
		} else {
			textTimer = 0f;
			coinText.text = "COINS: " + coins.ToString();
		}
	}

	void CheckChallenges() {
		for (int i = 0; i < 6; i++) {
			challengeSet1 += PlayerPrefs.GetInt("Challenge_" + (i+1).ToString());
			challengeSet2 += PlayerPrefs.GetInt("Challenge_" + (i+7).ToString());
			challengeSet3 += PlayerPrefs.GetInt("Challenge_" + (i+13).ToString());
			challengeSet4 += PlayerPrefs.GetInt("Challenge_" + (i+19).ToString());
			challengeSet5 += PlayerPrefs.GetInt("Challenge_" + (i+25).ToString());
			challengeSet6 += PlayerPrefs.GetInt("Challenge_" + (i+31).ToString());
		}

		if(challengeSet1 >= 6) {
			buyChallengeButton1.interactable = false;
			buyChallengeButton1Text.color = new Color(1, 1, 50/255, 0.5f);
		}

		if(challengeSet2 >= 6) {
			buyChallengeButton2.interactable = false;
			buyChallengeButton2Text.color = new Color(1, 1, 50/255, 0.5f);
		}

		if(challengeSet3 >= 6) {
			buyChallengeButton3.interactable = false;
			buyChallengeButton3Text.color = new Color(1, 1, 50/255, 0.5f);
		}

		if(challengeSet4 >= 6) {
			buyChallengeButton4.interactable = false;
			buyChallengeButton4Text.color = new Color(1, 1, 50/255, 0.5f);
		}

		if(challengeSet5 >= 6) {
			buyChallengeButton5.interactable = false;
			buyChallengeButton5Text.color = new Color(1, 1, 50/255, 0.5f);
		}

		if(challengeSet6 >= 6) {
			buyChallengeButton6.interactable = false;
			buyChallengeButton6Text.color = new Color(1, 1, 50/255, 0.5f);
		}
	}

	public void BuyChallenge (int b) {
		if (coins < buyChallengeCost[b-1]) {
			SetCoinTextToNotEnoughCoins();
		} else {
			int x = (b-1) * 6;
				
			for (int i = 0; i < 6; i++) {
				PlayerPrefs.SetInt("Challenge_" + (i+x+1).ToString(), 1);
			}
			coins -= buyChallengeCost[b-1];
			PlayerPrefs.SetInt(GamePreferences.CoinScore, coins);
			coinText.text = "COINS: " + coins.ToString();
			CheckChallenges();
		}
	}

	void SetCoinTextToNotEnoughCoins() {
		textTimer = 1f;
	}
}
