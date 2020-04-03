using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePreferences {

	public static string EasyDifficulty = "EasyDifficulty";
	public static string MediumDifficulty = "MediumDifficulty";
	public static string HardDifficulty = "HardDifficulty";

	public static string EasyDifficultyScore = "EasyDifficultyScore";
	public static string MediumDifficultyScore = "MediumDifficultyScore";
	public static string HardDifficultyScore = "HardDifficultyScore";

	public static string EasyDifficultyScoreScoreAttack = "EasyDifficultyScoreScoreAttack";
	public static string MediumDifficultyScoreScoreAttack = "MediumDifficultyScoreScoreAttack";
	public static string HardDifficultyScoreScoreAttack = "HardDifficultyScoreScoreAttack";

	public static string CoinScore = "CoinScore";

	public static string IsMusicOn = "IsMusicOn";

	public static string ChallengeOn = "ChallengeOn";

	public static string QuipLevel = "QuipLevel";

	public static string GameReset = "GameReset";

	public static string MaxLives = "MaxLives";

	public static string TotalPlays = "Total Plays";

	public static string CanShowAds = "CanShowAds";

	public static string sawTheEnding = "SawTheEnding";

	public static string NumberOfWindows = "NumberOfWindows";
	public static string NumberOfSmileys = "NumberOfSmileys";

	public static string [] ChallengeName;
	public static string [] ChallengeScoreString;


	public static int [] ChallengeCompleted;
	public static float [] ChallengeHighScore;

	public static int numberOfChallenges = 36;

	public static string [] fistName;
	public static int [] fistUnlocked;

	public static int numberOfFists = 24;

	// NOTE we are going to use int to represent bool variables
	// 0 = false; 1 = true

	public static int GetMusicState() {
		return PlayerPrefs.GetInt(GamePreferences.IsMusicOn);
	}

	public static void SetMusicState(int state) {
		PlayerPrefs.SetInt(GamePreferences.IsMusicOn, state);
	}

	public static void SetEasyDifficulty(int difficulty) {
		PlayerPrefs.SetInt(GamePreferences.EasyDifficulty, difficulty);
	}

	public static int GetEasyDifficulty() {
		return PlayerPrefs.GetInt(GamePreferences.EasyDifficulty);
	}

	public static void SetMediumDifficulty(int difficulty) {
		PlayerPrefs.SetInt(GamePreferences.MediumDifficulty, difficulty);
	}

	public static int GetMediumDifficulty() {
		return PlayerPrefs.GetInt(GamePreferences.MediumDifficulty);
	}

	public static void SetHardDifficulty(int difficulty) {
		PlayerPrefs.SetInt(GamePreferences.HardDifficulty, difficulty);
	}

	public static int GetHardDifficulty() {
		return PlayerPrefs.GetInt(GamePreferences.HardDifficulty);
	}

	//PlayerPrefs.SetInt("Score", 10);
	//PlayerPrefs.GetInt("Score");
	//PlayerPrefs.SetString("Key". "Value");



}
