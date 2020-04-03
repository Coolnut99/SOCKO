using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {

	[SerializeField]
	private GameObject explosion, glassBurst, intactWindow, shatteredWindow;

	[SerializeField]
	private GameObject[] randomWindow, randomShatteredWindow;

	[SerializeField]
	private AudioSource audioSource, glassShatter;

	[SerializeField]
	private Text theVideoGameText, pressStartText, randomQuoteText;

	[SerializeField]
	private Image explosionBackground, buttonBackground, loadingPanelBackground;

	[SerializeField]
	private Slider sliderBar;

	[SerializeField]
	private GameObject privacyPolicyButton;

	//[SerializeField]
	//private Ga

	private Color theVideoGameTextColor, pressStartTextColor, randomQuoteTextColor, explosionBackgroundColor, buttonBackgroundColor;

	private bool canLoad, videoGameTextAppearBool;

	private int quipLevel, quipNumber, timesPlayed;

	public float textAppearSpeed = 1f;
	private float theVideoGameTextAlpha = 0f;

	private bool loadingNextScene;

	void Awake() {
		privacyPolicyButton.SetActive(false);
		loadingNextScene = false;
		int x = Random.Range(0, 5);
		canLoad = false;
		videoGameTextAppearBool = false;
		intactWindow.GetComponent<Image>().sprite = randomWindow[x].GetComponent<Image>().sprite;
		shatteredWindow.GetComponent<Image>().sprite = randomShatteredWindow[x].GetComponent<Image>().sprite;
		intactWindow.SetActive(false);
		shatteredWindow.SetActive(false);
		theVideoGameTextColor = theVideoGameText.color;
		randomQuoteTextColor = randomQuoteText.color;
		pressStartTextColor = pressStartText.color;
		explosionBackgroundColor = explosionBackground.color;
		buttonBackgroundColor = buttonBackground.color;
	}

	// Use this for initialization
	void Start () {
		loadingPanelBackground.gameObject.SetActive(false);
		theVideoGameText.color = new Color (0f, 0f, 0f, 0f);
		randomQuoteText.color = new Color (0f, 0f, 0f, 0f);
		pressStartText.color = new Color (0f, 0f, 0f, 0f);
		explosionBackground.color = new Color (0f, 0f, 0f, 0f);
		buttonBackground.color = new Color (0f, 0f, 0f, 0f);
		quipLevel = PlayerPrefs.GetInt(GamePreferences.QuipLevel);
		SetQuipLevel();
	}

	void Update() {
		TheVideoGameTextAppear();
	}

	public void PlayExplosion() {
		audioSource.Play();
	}

	public void RevealText() {
		//Create random text here
		CreateRandomText();
		StartCoroutine(RevealTextCoroutine());
	}

	public void LoadNextLevel() {
		if(canLoad == true && loadingNextScene == false) {
			loadingNextScene = true;
			loadingPanelBackground.gameObject.SetActive(true);
			GetComponent<SpriteRenderer>().enabled = false;
			//SceneManager.LoadScene("Main Menu");
			//GameObject[] g = GameObject.FindGameObjectsWithTag("SOCKO Glass Burst");
			//if(g != null) {
			//	foreach(GameObject x in g) {
				//	Destroy(gameObject);
			//	}
		//	//}
			StartCoroutine(LoadMainMenuCoroutine());
		}
	}

	IEnumerator LoadMainMenuCoroutine() {
		//yield return new WaitForSecondsRealtime(3f);
		AsyncOperation async = SceneManager.LoadSceneAsync("Main Menu");
		while(!async.isDone) {
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			sliderBar.value = progress;
			yield return null;
		}
	}

	void CreateRandomText() {
		int i = Random.Range(0, quipNumber);
		switch(i) {
			case 0:
			randomQuoteText.text = "If it is a kick in the pants, it's a SOCKO to the nose.";
			break;

			case 1:
			randomQuoteText.text = "Never SOCKO someone that will SOCKO you back.";
			break;

			case 2:
			randomQuoteText.text = "Look, a game!\nLOOK, A FIST!";
			break;

			case 3:
			randomQuoteText.text = "Keep those fists flyin'.";
			break;

			case 4:
			randomQuoteText.text = "NNNNNNNNNHHHHHHHHH!";
			break;

			case 5:
			randomQuoteText.text = "It's not the Way of the Intercepting Fist, but it'll do.";
			break;

			case 6:
			randomQuoteText.text = "Not responsible for shattered screens or button finger.";
			break;

			case 7:
			randomQuoteText.text = "SOCKO: the number one choice in breaking evil windows everywhere!";
			break;

			case 8:
			randomQuoteText.text = "A SOCKO to the nose is worth two in your pocket.";
			break;

			case 9:
			randomQuoteText.text = "Just crumple your fingers in and put your thumb around and under your index and middle knuckles.";
			break;

			case 10:
			randomQuoteText.text = "Not responsible if you injure your hand hitting a REAL window.";
			break;

			case 11:
			randomQuoteText.fontSize = 96;
			randomQuoteText.text = "POW!";
			break;

			case 12:
			randomQuoteText.text = "Careful! Those evil windows are a real pane in the glass.";
			break;

			case 13:
			randomQuoteText.text = "We hope you own stock in a glass manufacturer.";
			break;

			case 14:
			randomQuoteText.text = "When the SOCKO is outlawed, only outlaws will... yeah, you know.";
			break;

			case 15:
			randomQuoteText.text = "Fair warning: ABC Bomb Company cuts no slack.";
			break;

			case 16:
			randomQuoteText.text = "The windows are all out screaming, \"YOU WANT A PIECE OF ME? I'LL CUT YOU!\"";
			break;

			case 17:
			randomQuoteText.text = "These windows have more than just the blue screen of death.";
			break;

			case 18:
			randomQuoteText.text = "Nice time for a break, don't you think?";
			break;

			case 19:
			randomQuoteText.text = "SOCKO: 100% guaranteed to make you pleased as punch.";
			break;

			case 20:
			randomQuoteText.text = "Speak softly and carry a big SOCKO.";
			break;

			case 21:
			randomQuoteText.text = "You want this fist? COME AND TAKE IT!";
			break;

			case 22:
			randomQuoteText.text = "It's a living.";
			break;

			case 23:
			randomQuoteText.text = "Good luck on not bombing this attempt.";
			break;

			case 24:
			randomQuoteText.text = "SOCKO will disprove the broken windows fallacy.";
			break;

			case 25:
			randomQuoteText.text = "We serve the best knuckle sandwiches in town.";
			break;

			case 26:
			randomQuoteText.text = "World-renowned and don't you forget it.";
			break;

			case 27:
			randomQuoteText.text = "The Most Glorious Fist chose YOU.";
			break;

			case 28:
			randomQuoteText.text = "Strong flesh.\nWeak smileys.";
			break;

			case 29:
			randomQuoteText.text = "BOMBS AWAY!";
			break;

			case 30:
			randomQuoteText.text = "Judy not necessary.";
			break;

			case 31:
			randomQuoteText.text = "True to thy SOCKO thro' all our days.";
			break;

			case 32:
			randomQuoteText.text = "No, not the cloth stockings you put on your feet.";
			break;

			case 33:
			randomQuoteText.text = "If you're thinking of WASHING those windows, you're in the wrong game.";
			break;

			case 34:
			randomQuoteText.text = "Wham. Bam. SLAM!";
			break;

			case 35:
			randomQuoteText.text = "Missed a spot.\nSMASH!";
			break;

			case 36:
			randomQuoteText.text = "There's the good, the bad, and the SOCKO.";
			break;

			case 37:
			randomQuoteText.text = "Break time has...\nBEGUN!";
			break;

			case 38:
			randomQuoteText.text = "Feel THESE fists of fury.";
			break;

			case 39:
			randomQuoteText.text = "You (probably) have two hands. Don't lose 'em.";
			break;

			case 40: 
			randomQuoteText.text = "Hey, what's that?\nBOOOOOM!";
			break;

			case 41:
			randomQuoteText.text = "Prediction?\nPAIN.";
			break;

			case 42:
			randomQuoteText.text = "It ain't no tropical drink.";
			break;

			case 43:
			randomQuoteText.text = "You must break them.";
			break;

			case 44:
			randomQuoteText.fontSize = 84;
			randomQuoteText.text = "WHAP!";
			break;

			case 45:
			randomQuoteText.text = "We put the \"pain\" in \"window pain\".";
			break;

			case 46:
			randomQuoteText.text = "Broken windows are real cut-ups.";
			break;

			case 47:
			randomQuoteText.text = "Each bomb comes from the recent box office.";
			break;

			case 48:
			randomQuoteText.text = "Play! Play! Play!  Coolnut needs a new computer!";
			break;

			case 49:
			randomQuoteText.text = "A real powder keg, the challenges are.";
			break;

			case 50:
			randomQuoteText.text = "Join the revolution (and not just by RAISING that fist)";
			break;

			case 51:
			randomQuoteText.text = "Billions and billions... of pieces.";
			break;

			case 52:
			randomQuoteText.text = "When life gives you lemons, you SOCKO them into...flat lemons.";
			break;

			case 53:
			randomQuoteText.text = "You think you're gonna play \"punch bug\" here? I think NOT!";
			break;

			case 54:
			randomQuoteText.text = "A fistful of knuckles.";
			break;

			case 55:
			randomQuoteText.text = "SOCKO early and SOCKO often.";
			break;

			case 56:
			randomQuoteText.text = "Peace through random punching.";
			break;

			case 57:
			randomQuoteText.text = "Don't miss that window of opportunity.";
			break;

			case 58:
			randomQuoteText.text = "Smile! You're gonna get punched.";
			break;

			case 59:
			randomQuoteText.text = "\"OUCH\" is only the beginning.";
			break;

			case 60:
			randomQuoteText.text = "They fall down go boom.";
			break;

			case 61:
			randomQuoteText.text = "I'm no hero. I just like to punch people in the nose.";
			break;

			case 62:
			randomQuoteText.text = "Life, liberty, and the pursuit of SOCKO.";
			break;

			case 63:
			randomQuoteText.text = "Your fist IS the game-breaker.";
			break;

			case 64:
			randomQuoteText.text = "This isn't blackjack, so be careful of saying \"Hit me\"";
			break;

			case 65:
			randomQuoteText.text = "You haven't heard our greatest hits yet.";
			break;

			case 66:
			randomQuoteText.text = "You can't spell \"SOCKO\" without K.O.";
			break;

			case 67:
			randomQuoteText.text = "Hope you're feeling punchy today.";
			break;

			case 68:
			randomQuoteText.text = "Don't do drugs.\nDo SOCKO!";
			break;

			case 69:
			randomQuoteText.fontSize = 84;
			randomQuoteText.text = "OOOOF!";
			break;

			case 70:
			randomQuoteText.text = "Don't worry. These hits have no long-term health effects.";
			break;

			case 71:
			randomQuoteText.text = "Feeling broken? Play another game of SOCKO.";
			break;

			case 72:
			randomQuoteText.text = "Number one in stress relief.";
			break;

			case 73:
			randomQuoteText.text = "Everybody needs something to punch every now and then.";
			break;

			case 74:
			randomQuoteText.text = "WE PULL PUNCHES FOR NOBODY.";
			break;

			default:
			randomQuoteText.text = "If you're reading this, either the game is hacked or Coolnut blew it!";
			break;

		}
	}

	void SetQuipLevel() {
		timesPlayed = PlayerPrefs.GetInt(GamePreferences.TotalPlays);
		if(timesPlayed <= 1) {
			quipLevel = 0;
		} else if (timesPlayed >= 2 && timesPlayed <= 9) {
			quipLevel = 1;
		} else if (timesPlayed >= 10 && timesPlayed <= 20) {
			quipLevel = 2;
		} else if (timesPlayed >= 21 && timesPlayed <= 35) {
			quipLevel = 3;
		} else if (timesPlayed >= 36 && timesPlayed <= 50) {
			quipLevel = 4;
		} else if (timesPlayed >= 51 && timesPlayed <= 65) {
			quipLevel = 5;
		} else if (timesPlayed >= 66 && timesPlayed <= 80) {
			quipLevel = 6;
		} else if (timesPlayed >= 81 && timesPlayed <= 100) {
			quipLevel = 7;
		} else if (timesPlayed >= 101 && timesPlayed <= 119) {
			quipLevel = 8;
		} else if (timesPlayed >= 120 && timesPlayed <= 155) {
			quipLevel = 9;
		} else {
			quipLevel = 10;
		}
		switch(quipLevel) {
			case 0:
			quipNumber = 1;
			break;

			case 1:
			quipNumber = 5;
			break;

			case 2:
			quipNumber = 10;
			break;

			case 3:
			quipNumber = 15;
			break;

			case 4:
			quipNumber = 20;
			break;

			case 5:
			quipNumber = 25;
			break;

			case 6:
			quipNumber = 32;
			break;

			case 7:
			quipNumber = 40;
			break;

			case 8:
			quipNumber = 50;
			break;

			case 9:
			quipNumber = 60;
			break;

			case 10:
			quipNumber = 75;
			break;

			default:
			quipNumber = 1;
			break;
		}
		PlayerPrefs.SetInt(GamePreferences.QuipLevel, quipLevel);
	}

	void TheVideoGameTextAppear() {
		if(videoGameTextAppearBool) {
			theVideoGameTextAlpha += Time.deltaTime;
			if(theVideoGameTextAlpha <= 1f) {
				theVideoGameText.color = new Color(theVideoGameTextColor.r, theVideoGameTextColor.g, theVideoGameTextColor.b, theVideoGameTextAlpha); 
			} else {
				theVideoGameTextAlpha = 1f;
			}
		}
	}

	//Allow user to turn this option on/off
	public void InstantiateGlass() {
		int x, y;
		for (x = -1; x < 2; x++) {
			for (y = -2; y < 1; y++) {
				Instantiate(glassBurst, new Vector3(x, y, 0f), Quaternion.identity);
			}
		}
	}

	public void InstantiateExplosion() {
		Instantiate(explosion, GetComponent<SpriteRenderer>().transform.position, Quaternion.identity);
		//Instantiate(explosion);
	}

	IEnumerator RevealTextCoroutine() {
		//theVideoGameText.color = theVideoGameTextColor;
		yield return new WaitForSecondsRealtime(0.5f); 
		videoGameTextAppearBool = true;
		yield return new WaitForSecondsRealtime(0.75f);
		intactWindow.SetActive(true);
		yield return new WaitForSecondsRealtime(0.75f);
		randomQuoteText.color = randomQuoteTextColor;
		explosionBackground.color = explosionBackgroundColor;
		intactWindow.SetActive(false);
		shatteredWindow.SetActive(true);
		glassShatter.Play();
		InstantiateGlass();
		yield return new WaitForSecondsRealtime(1f);
		pressStartText.color = pressStartTextColor;
		buttonBackground.color = buttonBackgroundColor;
		privacyPolicyButton.SetActive(true);
		canLoad = true;
	}
}
