using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class AchievementsController : MonoBehaviour {

	public static AchievementsController instance;

	private const string NNNNNNNHHHHHH = "CgkIwuqymqUNEAIQAQ";
	private const string easy_street = "CgkIwuqymqUNEAIQAg";
	private const string medium_hit_middle = "CgkIwuqymqUNEAIQAw";
	private const string hard_knocks = "CgkIwuqymqUNEAIQBA";
	private const string window_of_opportunity = "CgkIwuqymqUNEAIQBQ";
	private const string buy_something = "CgkIwuqymqUNEAIQBg";

	private const string score_50000_points = "CgkIwuqymqUNEAIQDw";
	private const string score_100000_points = "CgkIwuqymqUNEAIQEA";
	private const string score_250000_points = "CgkIwuqymqUNEAIQEQ";
	private const string score_500000_points = "CgkIwuqymqUNEAIQEg";

	private const string socko_100_smileys = "CgkIwuqymqUNEAIQEw";
	private const string socko_500_smileys = "CgkIwuqymqUNEAIQFA";
	private const string socko_1000_smileys = "CgkIwuqymqUNEAIQFQ";
	private const string socko_5000_smileys = "CgkIwuqymqUNEAIQFg";
	private const string socko_10000_smileys = "CgkIwuqymqUNEAIQFw";

	private const string socko_100_windows = "CgkIwuqymqUNEAIQGA";
	private const string socko_500_windows = "CgkIwuqymqUNEAIQGQ";
	private const string socko_1000_windows = "CgkIwuqymqUNEAIQGg";
	private const string socko_5000_windows = "CgkIwuqymqUNEAIQGw";
	private const string socko_10000_windows = "CgkIwuqymqUNEAIQHA";

	private const string clear_1_challenge = "CgkIwuqymqUNEAIQHQ";
	private const string clear_3_challenges = "CgkIwuqymqUNEAIQHg";
	private const string clear_6_challenges = "CgkIwuqymqUNEAIQHw";
	private const string clear_9_challenges = "CgkIwuqymqUNEAIQIA";
	private const string clear_18_challenges = "CgkIwuqymqUNEAIQIQ";
	private const string clear_27_challenges = "CgkIwuqymqUNEAIQIg";
	private const string clear_36_challenges = "CgkIwuqymqUNEAIQIw";

	private const string unlock_3_fists = "CgkIwuqymqUNEAIQJA";
	private const string unlock_6_fists = "CgkIwuqymqUNEAIQJQ";
	private const string unlock_12_fists = "CgkIwuqymqUNEAIQJg";
	private const string unlock_18_fists = "CgkIwuqymqUNEAIQJw";
	private const string unlock_24_fists = "CgkIwuqymqUNEAIQKA";

	private const string easy_mode_leaderboard = "CgkIwuqymqUNEAIQBw";
	private const string easy_mode_score_attack_leaderboard = "CgkIwuqymqUNEAIQCA";

	private const string medium_mode_leaderboard = "CgkIwuqymqUNEAIQCQ";
	private const string medium_mode_score_attack_leaderboard = "CgkIwuqymqUNEAIQCg";

	private const string hard_mode_leaderboard = "CgkIwuqymqUNEAIQCw";
	private const string hard_mode_score_attack_leaderboard = "CgkIwuqymqUNEAIQDA";

	private const string challenge_mode_leaderboard = "CgkIwuqymqUNEAIQDQ";

	private string[] achievements_names = {NNNNNNNHHHHHH, easy_street, medium_hit_middle, hard_knocks, window_of_opportunity, buy_something,
									 score_50000_points, score_100000_points, score_250000_points, score_500000_points,
									 socko_100_smileys, socko_500_smileys, socko_1000_smileys, socko_5000_smileys, socko_10000_smileys,
									 socko_100_windows, socko_500_windows, socko_1000_windows, socko_5000_windows, socko_10000_windows,
									 clear_1_challenge, clear_3_challenges, clear_6_challenges, clear_9_challenges, clear_18_challenges, clear_27_challenges, clear_36_challenges,
									 unlock_3_fists, unlock_6_fists, unlock_12_fists, unlock_18_fists, unlock_24_fists};


	private float achievementScore;				//Used to compare with leaderboards
	private int number_of_challenges;
	private int number_of_fists;

	private bool NNNNNNHHHHHH_said;
	private bool easy_mode_selected, medium_mode_selected, hard_mode_selected;
	private bool powerup_collected;
	private bool powerup_bought;

	private bool[] achievements;


	void Awake() {
		MakeSingleton();
	}

	// Use this for initialization
	void Start () {
        
		PlayGamesPlatform.Activate();
		InitiateAchievements();
		//Debug.Log("Number of achievements = " + achievements.Length);
		if(!Social.localUser.authenticated) {
			Social.localUser.Authenticate((bool success) => { });
		}
	}

	void OnLevelWasLoaded() {
		if(SceneManager.GetActiveScene().name == "Main Menu") {
			number_of_challenges = MasterControl.instance.AddNumberOfChallenges();
			number_of_fists = MasterControl.instance.AddNumberOfFists();
			CheckIfThereAreAnyUnlockedAchievements();
			//Debug.Log("Loaded main menu.");
		}
	}

	public void SetScore(float f) {
		achievementScore = f;
	}

	void InitiateAchievements() {
		achievements = MasterControl.instance.achievements;
		for(int i = 0; i < achievements.Length; i++) {
			if(!achievements[i]) {
				Social.ReportProgress(achievements_names[i], 0.0f, (bool success) => { });
			}
		}
	}

	public void NNNNNNHHHHHH() {
		NNNNNNHHHHHH_said = true;
	}

	public void SetNumberOfChallenges(int c) {
		number_of_challenges = c;
	}

	public void SetNumberOfFists(int f) {
		number_of_fists = f;
	}

	public void SetPowerupCollected() {
		powerup_collected = true;
	}

	public void SetPowerupBought() {
		powerup_bought = true;
	}

	public void ModeSelected(MasterControl.difficulty difficulty) {
		if(difficulty == MasterControl.difficulty.EASY) {
			easy_mode_selected = true;
		} else if (difficulty == MasterControl.difficulty.MEDIUM) {
			medium_mode_selected = true;
		} else if (difficulty == MasterControl.difficulty.HARD) {
			hard_mode_selected = true;
		}
	}

	// 1-2 is for easy mode and score attack
	// 3-4 is for medium mode and score attack
	// 5-6 is for hard mode and score attack
	// 7 is for challenge mode
	public void OpenLeaderboards(int leaderboardType) {
		if(Social.localUser.authenticated) {
			switch(leaderboardType) {
				case 1:
				PlayGamesPlatform.Instance.ShowLeaderboardUI(easy_mode_leaderboard);
				break;

				case 2:
				PlayGamesPlatform.Instance.ShowLeaderboardUI(easy_mode_score_attack_leaderboard);
				break;

				case 3:
				PlayGamesPlatform.Instance.ShowLeaderboardUI(medium_mode_leaderboard);
				break;

				case 4:
				PlayGamesPlatform.Instance.ShowLeaderboardUI(medium_mode_score_attack_leaderboard);
				break;

				case 5:
				PlayGamesPlatform.Instance.ShowLeaderboardUI(hard_mode_leaderboard);
				break;

				case 6:
				PlayGamesPlatform.Instance.ShowLeaderboardUI(hard_mode_score_attack_leaderboard);
				break;

				case 7:
				PlayGamesPlatform.Instance.ShowLeaderboardUI(challenge_mode_leaderboard);
				break;

				default:
				Debug.LogError("Number selected for OpenLeaderboards is out of range. Please make sure the number selected is from 1-7.");
				break;
			}
		}
	}


	public void OpenAchievements() {
		if(Social.localUser.authenticated) {
			Social.ShowAchievementsUI();

		}
	}

	public void LogInOrLogOutGoogleLeaderboards() {
		if(Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.SignOut();
		} else {
			Social.localUser.Authenticate((bool success) => { });
		}
	}

	// for int difficulty:
	// 1-2 is for easy mode and score attack
	// 3-4 is for medium mode and score attack
	// 5-6 is for hard mode and score attack
	// 7 is for challenge mode
	public void ReportScore(int difficulty, float f) {
		long score = (long) f;
		switch(difficulty) {
			case 1:
			Social.ReportScore(score, easy_mode_leaderboard, (bool success) => { });
			break;

			case 2:
			Social.ReportScore(score, easy_mode_score_attack_leaderboard, (bool success) => { });
			break;

			case 3:
			Social.ReportScore(score, medium_mode_leaderboard, (bool success) => { });
			break;

			case 4:
			Social.ReportScore(score, medium_mode_score_attack_leaderboard, (bool success) => { });
			break;

			case 5:
			Social.ReportScore(score, hard_mode_leaderboard, (bool success) => { });
			break;

			case 6:
			Social.ReportScore(score, hard_mode_score_attack_leaderboard, (bool success) => { });
			break;

			case 7:
			Social.ReportScore(score, challenge_mode_leaderboard, (bool success) => { });
			break;

			default:
			Debug.LogError("Number selected for ReportScore is out of range. Please make sure the number selected is from 1-7.");
			break;
		}
	}

	void UnlockAchievement(int index) {
		if(Social.localUser.authenticated) {
			Social.ReportProgress(achievements_names[index], 100.0f, (bool success) => {
				if(success) {
					achievements[index] = true;
					MasterControl.instance.achievements = achievements;
					MasterControl.instance.SaveAchievements();
				}
			});
		}
	}

	void CheckIfThereAreAnyUnlockedAchievements() {
		
		// Achievement 1 -- NNNNNNNHHHHHH!!!!!
		if(NNNNNNHHHHHH_said == true) {
			if(!achievements[0]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(0);
				}
			}
		}

		// Achievement 2 -- Easy difficulty selected
		if(easy_mode_selected == true) {
			if(!achievements[1]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(1);
				}
			}
		}

		// Achievement 3 -- Medium difficulty selected
		if(medium_mode_selected == true) {
			if(!achievements[2]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(2);
				}
			}
		}

		// Achievement 4 -- Hard difficulty selected
		if(hard_mode_selected == true) {
			if(!achievements[3]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(3);
				}
			}
		}

		// Achievement 5 -- Powerup collected
		if(powerup_collected == true) {
			if(!achievements[4]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(4);
				}
			}
		}

		// Achievement 6 -- Powerup bought
		if(powerup_bought == true) {
			if(!achievements[5]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(5);
				}
			}
		}

		// Achievement 7 -- Scored 50,000 points or more
		if(achievementScore >= 50000f) {
			if(!achievements[6]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(6);
				}
			}
		}

		//Achievement 8 -- Scored 100,000 points or more
		if(achievementScore >= 100000f) {
			if(!achievements[7]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(7);
				}
			}
		}

		// Achievement 9 -- Scored 250,000 points or more
		if(achievementScore >= 250000f) {
			if(!achievements[8]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(8);
				}
			}
		}

		// Achievement 10 -- Scored 500,000 points or more
		if(achievementScore >= 500000f) {
			if(!achievements[9]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(9);
				}
			}
		}

		// Achievement 11 -- SOCKOed 100 or more smileys
		if(MasterControl.instance.numberOfSmileys >= 100) {
			if(!achievements[10]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(10);
				}
			}
		}

		// Achievement 12 -- SOCKOed 500 or more smileys
		if(MasterControl.instance.numberOfSmileys >= 500) {
			if(!achievements[11]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(11);
				}
			}
		}

		// Achievement 13 -- SOCKOed 1000 or more smileys
		if(MasterControl.instance.numberOfSmileys >= 1000) {
			if(!achievements[12]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(12);
				}
			}
		}

		// Achievement 14 -- SOCKOed 5000 or more smileys
		if(MasterControl.instance.numberOfSmileys >= 5000) {
			if(!achievements[13]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(13);
				}
			}
		}

		// Achievement 15 -- SOCKOed 10000 or more smileys
		if(MasterControl.instance.numberOfSmileys >= 10000) {
			if(!achievements[14]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(14);
				}
			}
		}

		// Achievement 16 -- SOCKOed 100 or more windows
		if(MasterControl.instance.numberOfWindows >= 100) {
			if(!achievements[15]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(15);
				}
			}
		}

		// Achievement 17 -- SOCKOed 500 or more windows
		if(MasterControl.instance.numberOfWindows >= 500) {
			if(!achievements[16]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(16);
				}
			}
		}

		// Achievement 18 -- SOCKOed 1000 or more windows
		if(MasterControl.instance.numberOfWindows >= 1000) {
			if(!achievements[17]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(17);
				}
			}
		}

		// Achievement 19 -- SOCKOed 5000 or more windows
		if(MasterControl.instance.numberOfWindows >= 5000) {
			if(!achievements[18]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(18);
				}
			}
		}

		// Achievement 20 -- SOCKOed 10000 or more windows
		if(MasterControl.instance.numberOfWindows >= 10000) {
			if(!achievements[19]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(19);
				}
			}
		}

		// Achievement 21 -- Cleared 1 challenge
		if(number_of_challenges >= 1) {
			if(!achievements[20]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(20);
				}
			}
		}

		// Achievement 22 -- Cleared 3 challenges
		if(number_of_challenges >= 3) {
			if(!achievements[21]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(21);
				}
			}
		}

		// Achievement 23 -- Cleared 6 challenges
		if(number_of_challenges >= 6) {
			if(!achievements[22]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(22);
				}
			}
		}

		// Achievement 24 -- Cleared 9 challenges
		if(number_of_challenges >= 9) {
			if(!achievements[23]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(23);
				}
			}
		}

		// Achievement 25 -- Cleared 18 challenges
		if(number_of_challenges >= 18) {
			if(!achievements[24]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(24);
				}
			}
		}

		// Achievement 26 -- Cleared 27 challenges
		if(number_of_challenges >= 27) {
			if(!achievements[25]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(25);
				}
			}
		}

		// Achievement 27 -- Cleared 36 challenges
		if(number_of_challenges >= 36) {
			if(!achievements[26]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(26);
				}
			}
		}

		// Achievement 28 -- Unlock 3 fists
		if(number_of_fists >= 3) {
			if(!achievements[27]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(27);
				}
			}
		}

		// Achievement 29 -- Unlock 6 fists
		if(number_of_fists >= 6) {
			if(!achievements[28]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(28);
				}
			}
		}

		// Achievement 30 -- Unlock 12 fists
		if(number_of_fists >= 12) {
			if(!achievements[29]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(29);
				}
			}
		}

		// Achievement 31 -- Unlock 18 fists
		if(number_of_fists >= 18) {
			if(!achievements[30]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(30);
				}
			}
		}

		// Achievement 32 -- Unlock 24 fists
		if(number_of_fists >= 24) {
			if(!achievements[31]) {
				if(Social.localUser.authenticated) {
					UnlockAchievement(31);
				}
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
}
