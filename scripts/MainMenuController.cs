using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private Animator animator, gameSelectAnimator, startAnimator, leaderboardsAnimator, statsAnimator;

	[SerializeField]
	private AudioSource selectChoice, validateChoice;

	[SerializeField]
	private Text highScoreText, highScoreScoreAttackText, quipLevelText, difficultyInfoText, quitGameText;

	[SerializeField]
	private GameObject Socko, darkenPanel, quitPanel;
	GameObject sockoClone;

	[SerializeField]
	private MedalController medalController;

	private float pixelsPerX, pixelsPerY;
	private float worldHeight, worldWidth;
	private float mouseX, mouseY;

	private float challengeTotalScore;

	private int numberOfChallengesCompleted;

	private float easyHighScore, mediumHighScore, hardHighScore, easyHighScoreScoreAttack, mediumHighScoreScoreAttack, hardHighScoreScoreAttack;

	// Use this for initialization
	void Start () {
		darkenPanel.SetActive(false);
		quitPanel.SetActive(false);
		challengeTotalScore = AddTotalScore();
		numberOfChallengesCompleted = MasterControl.instance.AddNumberOfChallenges();
		easyHighScore = PlayerPrefs.GetFloat(GamePreferences.EasyDifficultyScore);
		easyHighScoreScoreAttack = PlayerPrefs.GetFloat(GamePreferences.EasyDifficultyScoreScoreAttack);
		mediumHighScore = PlayerPrefs.GetFloat(GamePreferences.MediumDifficultyScore);
		mediumHighScoreScoreAttack = PlayerPrefs.GetFloat(GamePreferences.MediumDifficultyScoreScoreAttack);
		hardHighScore = PlayerPrefs.GetFloat(GamePreferences.HardDifficultyScore);
		hardHighScoreScoreAttack = PlayerPrefs.GetFloat(GamePreferences.HardDifficultyScoreScoreAttack);

		//Debug.Log("Screen Height:" + Screen.height);
		//Debug.Log("Screen Width:" + Screen.width);
		worldHeight = Camera.main.orthographicSize * 2f;
		worldWidth = worldHeight / Screen.height * Screen.width;
		//Debug.Log("World Height:" + worldHeight);
		//Debug.Log("World Width:" + worldWidth);
		pixelsPerX = Screen.width / worldWidth;
		pixelsPerY = Screen.height / worldHeight;
		//Debug.Log("Pixels Per X:" + pixelsPerX);
		//Debug.Log("Pixels Per Y:" + pixelsPerY);
		quipLevelText.text = "LEVEL - " + PlayerPrefs.GetInt(GamePreferences.QuipLevel).ToString();

		if(challengeTotalScore > 0) {
			AchievementsController.instance.ReportScore(7, challengeTotalScore);
		}
	}
	


	public void StartGame() {
		validateChoice.Play();
		animator.Play("SlideOut");
		StartCoroutine(StartGameCoroutine());
	}

	public void HowToPlay() {
		validateChoice.Play();
		animator.Play("SlideOut");
		StartCoroutine(HowToPlayCoroutine());
	}

	public void Settings() {
		Debug.Log("Moving to Configuration screen");
	}

	public void Store() {
		validateChoice.Play();
		animator.Play("SlideOut");
		StartCoroutine(StoreCoroutine());
	}

	public void About() {
		validateChoice.Play();
		animator.Play("SlideOut");
		StartCoroutine(AboutCoroutine());
	}

	public void QuitGame() {
		Debug.Log("Quitting Game");
		validateChoice.Play();
		Application.Quit();
	}

	public void ShowQuitPanel() {
		validateChoice.Play();
		GivePlayerSnark();
		quitPanel.SetActive(true);
	}

	public void CancelQuitPanel() {
		validateChoice.Play();
		quitPanel.SetActive(false);
	}

	public void SplashScreen() {
		validateChoice.Play();
		SceneManager.LoadScene("splash");
	}

	public void EasyMode() {
		validateChoice.Play();
		MasterControl.instance.set_difficulty = MasterControl.difficulty.EASY;
		SetDifficultyInfoText();
		SetHighScoreText();
		gameSelectAnimator.Play("Game Options SlideOut");
		StartCoroutine(EasyModeCoroutine());
	}

	public void NormalMode() {
		validateChoice.Play();
		MasterControl.instance.set_difficulty = MasterControl.difficulty.MEDIUM;
		SetDifficultyInfoText();
		SetHighScoreText();
		gameSelectAnimator.Play("Game Options SlideOut");
		StartCoroutine(NormalModeCoroutine());
	}

	public void HardMode() {
		validateChoice.Play();
		MasterControl.instance.set_difficulty = MasterControl.difficulty.HARD;
		SetDifficultyInfoText();
		SetHighScoreText();
		gameSelectAnimator.Play("Game Options SlideOut");
		StartCoroutine(HardModeCoroutine());
	}

	public void ChallengeMode() {
		validateChoice.Play();
		MasterControl.instance.set_difficulty = MasterControl.difficulty.CHALLENGE;
		SetDifficultyInfoText();
		SetHighScoreText();
		gameSelectAnimator.Play("Game Options SlideOut");
		StartCoroutine(ChallengeModeCoroutine());
	}

	public void GoBack() {
		validateChoice.Play();
		gameSelectAnimator.Play("Game Options SlideOut");
		StartCoroutine(GoBackCoroutine());
	}

	public void PlayGame() {
		validateChoice.Play();
		startAnimator.Play("Game Options SlideOut");
		StartCoroutine(PlayGameCoroutine());
	}

	public void GoBackToGameSelection() {
		validateChoice.Play();
		startAnimator.Play("Game Options SlideOut");
		StartCoroutine(GoBackToGameSelectionCoroutine());
	}

	public void SetHighScoreText() {
		if (MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
			highScoreText.text = "EASY HIGH SCORE:\n" + easyHighScore.ToString();
			highScoreScoreAttackText.text = "SCORE ATTACK HI-SCORE:\n" + easyHighScoreScoreAttack.ToString();
			SetMedal(easyHighScore, easyHighScoreScoreAttack);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.MEDIUM) {
			highScoreText.text = "NORMAL HIGH SCORE:\n" + mediumHighScore.ToString();
			highScoreScoreAttackText.text = "SCORE ATTACK HI-SCORE:\n" + mediumHighScoreScoreAttack.ToString();
			SetMedal(mediumHighScore, mediumHighScoreScoreAttack);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
			highScoreText.text = "HARD HIGH SCORE:\n" + hardHighScore.ToString();
			highScoreScoreAttackText.text = "SCORE ATTACK HI-SCORE:\n" + hardHighScoreScoreAttack.ToString();
			SetMedal(hardHighScore, hardHighScoreScoreAttack);
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			highScoreText.text = "CHALLENGE TOTAL:\n" + challengeTotalScore.ToString();
			highScoreScoreAttackText.text = "CHALLENGES COMPLETED:\n" + numberOfChallengesCompleted.ToString() + "/" + GamePreferences.numberOfChallenges.ToString();
			SetMedal();
		}
	}

	void SetMedal(float normalModeHighScore, float scoreAttackHighScore) {
		if(normalModeHighScore < 250000f) {
			medalController.SetMedal(MedalController.medal_type.none);
		} else if (normalModeHighScore < 500000f) {
			medalController.SetMedal(MedalController.medal_type.bronze);
		} else if (normalModeHighScore < 1000000f) {
			medalController.SetMedal(MedalController.medal_type.silver);
		} else {
			medalController.SetMedal(MedalController.medal_type.gold);
		}

		if(scoreAttackHighScore < 200000f) {
			medalController.SetScoreAttackMedal(MedalController.medal_type.none);
		} else if (scoreAttackHighScore < 375000f) {
			medalController.SetScoreAttackMedal(MedalController.medal_type.bronze);
		} else if (scoreAttackHighScore < 750000f) {
			medalController.SetScoreAttackMedal(MedalController.medal_type.silver);
		} else {
			medalController.SetScoreAttackMedal(MedalController.medal_type.gold);
		}

	}

	void SetMedal() {
		medalController.SetMedal(MedalController.medal_type.none);
		medalController.SetScoreAttackMedal(MedalController.medal_type.none);
	}

	void SetDifficultyInfoText() {
		difficultyInfoText.resizeTextForBestFit = false;
		switch(MasterControl.instance.set_difficulty) {
			case MasterControl.difficulty.EASY:
			difficultyInfoText.text = "Easy: Slow speed. More powerups, fewer bombs and coins.";
			break;

			case MasterControl.difficulty.MEDIUM:
			difficultyInfoText.text = "Normal: Medium speed, normal amound of powerups, bombs, coins.";
			break;

			case MasterControl.difficulty.HARD:
			difficultyInfoText.text = "Hard: Fast speed. Fewer powerups. More bombs but more coins.";
			break;

			case MasterControl.difficulty.CHALLENGE:
			difficultyInfoText.resizeTextForBestFit = true;
			difficultyInfoText.text = "Challenge: 36 different challenges for you to complete. Powerups may not appear. Score shown is cumulative.";
			break;

			default:
			difficultyInfoText.text = "Easy: Slow speed. More powerups, fewer bombs and coins.";
			break;
		}
	}

	float AddTotalScore() {
		float totalScore = 0;
		for (int i = 0; i < GamePreferences.numberOfChallenges; i++) {
			totalScore += PlayerPrefs.GetFloat("ChallengeScore_" + (i+1).ToString());
		}

		return totalScore;
	}

	public void LogIntoGoogleLeaderboards() {
		AchievementsController.instance.LogInOrLogOutGoogleLeaderboards();
	}

	public void OpenAchievements() {
		AchievementsController.instance.OpenAchievements();
	}

	public void OpenLeaderboards() {
		darkenPanel.SetActive(true);
		darkenPanel.GetComponent<Animator>().Play("darken panel");
		leaderboardsAnimator.Play("leaderboard down");
	}

	public void OpenStats() {
		darkenPanel.SetActive(true);
		darkenPanel.GetComponent<Animator>().Play("darken panel");
		statsAnimator.Play("leaderboard down");
	}

	public void CloseLeaderboards() {
		StartCoroutine(RaisePanel());
	}

	public void CloseStatsPanel() {
		StartCoroutine(RaiseStatsPanel());
	}

	public void OpenLeaderboards(int x){
		AchievementsController.instance.OpenLeaderboards(x);
	}

	public void RateUs() {
		SocialMediaController.instance.RateOurApp();
	}

	void GivePlayerSnark() {
		int i = Random.Range(0, 8);
		switch(i){
			case 0:
			quitGameText.text = "But we were having SO MUCH FUN!";
			break;

			case 1:
			quitGameText.text = "WUSS!!!";
			break;

			case 2:
			quitGameText.text = "You'll be back. Trust me.";
			break;

			case 3:
			quitGameText.text = "We'll be waiting. FOOL!";
			break;

			case 4:
			quitGameText.text = "You can go. But you will NEVER resist the temptation to SOCKO!";
			break;

			case 5:
			quitGameText.text = "But there are still windows everywhere. EVERYWHERE!!!";
			break;

			case 6:
			quitGameText.text = "OK, fine, go. See if I care.";
			break;

			case 7:
			quitGameText.text = "But why? We won't bill you for the windows you break.";
			break;

			default:
			quitGameText.text = "But we were having SO MUCH FUN!";
			break;

		}
	}


	IEnumerator StartGameCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		gameSelectAnimator.Play("Game Options SlideIn");
	}

	IEnumerator HowToPlayCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		SceneManager.LoadScene("How to Play");
	}

	IEnumerator StoreCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		SceneManager.LoadScene("Store");
	}

	IEnumerator AboutCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		SceneManager.LoadScene("About");
	}

	IEnumerator EasyModeCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		startAnimator.Play("Game Options SlideIn");
	}

	IEnumerator NormalModeCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		startAnimator.Play("Game Options SlideIn");
	}

	IEnumerator HardModeCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		startAnimator.Play("Game Options SlideIn");
	}

	IEnumerator ChallengeModeCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		startAnimator.Play("Game Options SlideIn");
		//SceneManager.LoadScene("Challenges");
	}

	IEnumerator GoBackCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		animator.Play("SlideIn");
	}

	IEnumerator PlayGameCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			SceneManager.LoadScene("Challenges");
		} else {
			AchievementsController.instance.ModeSelected(MasterControl.instance.set_difficulty);
			SceneManager.LoadScene("Select a Fist");
		}
	}

	IEnumerator GoBackToGameSelectionCoroutine() {
		yield return new WaitForSecondsRealtime(0.5f);
		gameSelectAnimator.Play("Game Options SlideIn");
	}

	IEnumerator RaisePanel() {
		leaderboardsAnimator.Play("leaderboard up");
		darkenPanel.GetComponent<Animator>().Play("lighten panel");
		yield return new WaitForSecondsRealtime(0.5f);
		darkenPanel.SetActive(false);
	}

	IEnumerator RaiseStatsPanel() {
		statsAnimator.Play("leaderboard up");
		darkenPanel.GetComponent<Animator>().Play("lighten panel");
		yield return new WaitForSecondsRealtime(0.5f);
		darkenPanel.SetActive(false);
	}

	// Update is called once per frame
	//Dummied out to clear up graphics
	/*
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			float tempX = Input.mousePosition.x / pixelsPerX;
			float tempY = Input.mousePosition.y / pixelsPerY;
			mouseX = tempX - (worldWidth / 2);
			mouseY = tempY - (worldHeight / 2);
			InstantiateSocko();
		}
	}

	void InstantiateSocko() {
		sockoClone = Instantiate(Socko, transform.position, Quaternion.identity) as GameObject;
		sockoClone.transform.position = new Vector3(mouseX, mouseY, 0f);
	} */
}
