using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterControl : MonoBehaviour {

	// Add/load/etc. player prefs, configurations here
	public enum difficulty {EASY, MEDIUM, HARD, CHALLENGE};

	public difficulty set_difficulty;

	public int challengeNumber; //Ignore this if playing Unlimited mode

	public static MasterControl instance;

	public float timerSpeed;

	//For ads
	public bool adLoaded;
	public int canShowAds; // 1 = true, 0 = false

	//For when you start a game: the first and fifth game, and every fifth game thereafter, load an ad.
	public int numberOfAds;

	public int numberOfWindows, numberOfSmileys; //Number of windows and smileys hit in all games for purposes of achievements

	public bool scoreAttack; //If "score attack" is selected. (Easy, Medium, Hard only.)

	public static int numberOfAchievements = 32;

	public bool[] achievements;

	public int sawTheEnding;

	void Awake() {
		MakeSingleton();
		ResetTimerSpeed();
		set_difficulty = difficulty.EASY;
		achievements = new bool[numberOfAchievements];
	}

	// Use this for initialization
	void Start () {
		GamePreferences.ChallengeName = new string[GamePreferences.numberOfChallenges];
		GamePreferences.ChallengeScoreString = new string[GamePreferences.numberOfChallenges];
		GamePreferences.fistName = new string[GamePreferences.numberOfFists];

		canShowAds = PlayerPrefs.GetInt(GamePreferences.CanShowAds);

		sawTheEnding = PlayerPrefs.GetInt(GamePreferences.sawTheEnding);

		for (int i = 0; i < GamePreferences.numberOfChallenges; i++) {
			GamePreferences.ChallengeName[i] = "Challenge_" + (i+1).ToString();
			GamePreferences.ChallengeScoreString[i] = "ChallengeScore_" + (i+1).ToString();
		}

		for (int x = 0; x < GamePreferences.numberOfFists; x++) {
			GamePreferences.fistName[x] = "Fist_" + (x+1).ToString();
		}

		ResetChallenges();

		if(GamePreferences.GetMusicState() == 1) {
			AudioListener.volume = 1;
		} else { 
			AudioListener.volume = 0;
		}

		numberOfWindows = PlayerPrefs.GetInt(GamePreferences.NumberOfWindows);
		numberOfSmileys = PlayerPrefs.GetInt(GamePreferences.NumberOfSmileys);

		InitiateAchievements();

	}

	void ResetChallenges() {
		if(!PlayerPrefs.HasKey("Game Initialized")) {
			Debug.Log("Starting a new game");
			for (int i = 0; i < GamePreferences.numberOfChallenges; i++) {
				PlayerPrefs.SetInt(GamePreferences.ChallengeName[i], 0);
				PlayerPrefs.SetFloat(GamePreferences.ChallengeScoreString[i], 0f);
			}

			ResetAchievements();

			PlayerPrefs.SetFloat(GamePreferences.EasyDifficultyScore, 0f);
			PlayerPrefs.SetFloat(GamePreferences.MediumDifficultyScore, 0f);
			PlayerPrefs.SetFloat(GamePreferences.HardDifficultyScore, 0f);

			PlayerPrefs.SetFloat(GamePreferences.EasyDifficultyScoreScoreAttack, 0f);
			PlayerPrefs.SetFloat(GamePreferences.MediumDifficultyScoreScoreAttack, 0f);
			PlayerPrefs.SetFloat(GamePreferences.HardDifficultyScoreScoreAttack, 0f);

			GamePreferences.SetMusicState(1);
			PlayerPrefs.SetInt(GamePreferences.MaxLives, 3);

			PlayerPrefs.SetInt(GamePreferences.CoinScore, 0);

			PlayerPrefs.SetInt(GamePreferences.QuipLevel, 0);

			PlayerPrefs.SetInt(GamePreferences.ChallengeName[0], 1);

			PlayerPrefs.SetInt(GamePreferences.TotalPlays, 0);

			PlayerPrefs.SetInt(GamePreferences.fistName[0], 1);

			PlayerPrefs.SetInt(GamePreferences.CanShowAds, 1); // 1 = true

			PlayerPrefs.SetInt(GamePreferences.NumberOfWindows, 0);
			PlayerPrefs.SetInt(GamePreferences.NumberOfSmileys, 0);

			// TESTING PlayerPrefs, delete these once game is active
			//PlayerPrefs.SetInt(GamePreferences.ChallengeName[3], 1);

			//PlayerPrefs.SetFloat("ChallengeScore_12", 250f);
			//PlayerPrefs.SetFloat("ChallengeScore_1", 1000f);
			//PlayerPrefs.SetFloat(GamePreferences.ChallengeScoreString[16], 500f);

			//////

			PlayerPrefs.SetInt("Game Initialized", 713);
		}
	}

	public void SetSoundState() {
		if(GamePreferences.GetMusicState() == 1) {
			AudioListener.volume = 1;
		} else {
			AudioListener.volume = 0;
		}
	}

	public void SetTimerSpeed() {
		timerSpeed += 0.1f;
		AdjustTimerSpeed();
	}

	public void AdjustTimerSpeed() {
		if(timerSpeed < 1f) {
			timerSpeed = 1f;
		}

		if(timerSpeed > 2f) {
			timerSpeed = 2f;
		}
	}

	public void ResetTimerSpeed() {
		timerSpeed = 1f;
	}

	public void SetNumberOfSmileys() {
		PlayerPrefs.SetInt(GamePreferences.NumberOfSmileys, numberOfSmileys);
		//Debug.Log("Number of smileys SOCKOed:" + PlayerPrefs.GetInt(GamePreferences.NumberOfSmileys));
	}

	public void SetNumberOfWindows() {
		PlayerPrefs.SetInt(GamePreferences.NumberOfWindows, numberOfWindows);
		//Debug.Log("Number of windows SOCKOed:" + PlayerPrefs.GetInt(GamePreferences.NumberOfWindows));
	}

	public void AddNumberOfSmileys(int smileys) {
		numberOfSmileys += smileys;
		PlayerPrefs.SetInt(GamePreferences.NumberOfSmileys, numberOfSmileys);
		//Debug.Log("Number of smileys SOCKOed:" + PlayerPrefs.GetInt(GamePreferences.NumberOfSmileys));
	}

	public void AddNumberOfWindows(int windows) {
		numberOfWindows += windows;
		PlayerPrefs.SetInt(GamePreferences.NumberOfWindows, numberOfWindows);
		//Debug.Log("Number of windows SOCKOed:" + PlayerPrefs.GetInt(GamePreferences.NumberOfWindows));
	}

	public int AddNumberOfChallenges() {
		int n = 0;
		for (int i = 0; i < GamePreferences.numberOfChallenges; i++) {
			if(PlayerPrefs.GetFloat(GamePreferences.ChallengeScoreString[i]) > 0) {
				n++;
			}
		}
		return n;
	}

	public int AddNumberOfFists() {
		int n = 0;
		for(int i = 0; i < GamePreferences.numberOfFists; i++) {
			if (PlayerPrefs.GetInt("Fist_" + (i+1).ToString()) > 0) {
				n++;
			}
		}
		return n;
	}

	public void SetAchievements(int i) {
		if(i > 0 && i < numberOfAchievements) {
			achievements[i] = true;
			PlayerPrefs.SetInt("Achievement_" + i.ToString(), 1);
		} else {
			Debug.LogError("Number for SetAchievements is out of range. Pick from 0 to 31.");
		}
	}

	public void ResetAchievements() {
		for(int i = 0; i < numberOfAchievements; i++) {
			PlayerPrefs.SetInt("Achievement_" + i.ToString(), 0);
			//Debug.Log("Achievement_" + i.ToString());
		}
	}

	public void SaveAchievements() {
		for(int i = 0; i < numberOfAchievements; i++) {
			if(achievements[i] == true) {
				PlayerPrefs.SetInt("Achievement_" + i.ToString(), 1);
			} else {
				PlayerPrefs.SetInt("Achievement_" + i.ToString(), 0);
			}
		}
	}

	void InitiateAchievements() {
		for(int i = 0; i < numberOfAchievements; i++) {
			int a = PlayerPrefs.GetInt("Achievement_" + i.ToString());
			if(a == 0) {
				achievements[i] = false;
			} else {
				achievements[i] = true;
			}
		}
	}


	void MakeSingleton() {
		if(instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	/* --------------- Old version of "Start" -- delete when game goes into production ---------------
	void Start () {
		GamePreferences.ChallengeName = new string[GamePreferences.numberOfChallenges];
		GamePreferences.ChallengeScoreString = new string[GamePreferences.numberOfChallenges];
		GamePreferences.fistName = new string[GamePreferences.numberOfFists];


		//PlayerPrefs.SetInt(GamePreferences.NumberOfWindows, 0); //TEST
		//PlayerPrefs.SetInt(GamePreferences.NumberOfSmileys, 0); //TEST
		//ResetAchievements(); //TEST
		Debug.Log("Number of smileys SOCKOed: " + PlayerPrefs.GetInt(GamePreferences.NumberOfSmileys));
		Debug.Log("Number of windows SOCKOed: " + PlayerPrefs.GetInt(GamePreferences.NumberOfWindows));
		Debug.Log("Number of fists unlocked: " + AddNumberOfFists());
		Debug.Log("Number of achievements in MasterControl: " + achievements.Length);

		canShowAds = PlayerPrefs.GetInt(GamePreferences.CanShowAds);
		//PlayerPrefs.SetInt(GamePreferences.CanShowAds, 1); //TEST
		//PlayerPrefs.SetInt(GamePreferences.CoinScore, 9999); // TEST -- remove in final game
		//PlayerPrefs.SetInt("Challenge_20", 0);  //TEST


		for (int i = 0; i < GamePreferences.numberOfChallenges; i++) {
			GamePreferences.ChallengeName[i] = "Challenge_" + (i+1).ToString();
			GamePreferences.ChallengeScoreString[i] = "ChallengeScore_" + (i+1).ToString();
			//Debug.Log(GamePreferences.ChallengeName[i]);
			//Debug.Log(GamePreferences.ChallengeScoreString[i]);
		}

		for (int x = 0; x < GamePreferences.numberOfFists; x++) {
			GamePreferences.fistName[x] = "Fist_" + (x+1).ToString();
			//Debug.Log(GamePreferences.fistName[x]);
		}

		ResetChallenges();

		if(GamePreferences.GetMusicState() == 1) {
			AudioListener.volume = 1;
		} else { 
			AudioListener.volume = 0;
		}
		//PlayerPrefs.SetInt(GamePreferences.fistName[0], 1);


		numberOfWindows = PlayerPrefs.GetInt(GamePreferences.NumberOfWindows);
		numberOfSmileys = PlayerPrefs.GetInt(GamePreferences.NumberOfSmileys);

		InitiateAchievements();

		//PlayerPrefs.SetInt(GamePreferences.fistName[2], 0); //TEST
		//PlayerPrefs.SetInt(GamePreferences.fistName[3], 0); //TEST
		//PlayerPrefs.SetInt(GamePreferences.fistName[4], 0); //TEST
	}*/
}
