using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	[SerializeField]
	private GameObject fadeBackground, gameOverImage, textCanvas, finalStatusBox, retryButton, selectFistsButton, ReturnToStartButton, gameController; // Add the final status box here and SetActive to false

	[SerializeField]
	private Text gameOverText, scoreText, highScoreText, newHighScoreText, bonusCoinText, coinText, totalCoinText;	//Add the final text parts here

	[SerializeField]
	private AudioSource gameOverSound, explosion;

	private Color tempColor;

	// Use this for initialization
	void Start () {
		tempColor = gameOverText.color;
		gameOverText.color = new Color(0f, 0f, 0f, 0f);
		ReturnToStartButton.SetActive(false);
		fadeBackground.SetActive(false);
		gameOverImage.SetActive(false);
		finalStatusBox.SetActive(false);
		textCanvas.SetActive(false);
	}

	public void GameOver() {
		GameOverMessage();
		StartCoroutine(GameOverCoroutine());
	}

	void GameOverMessage() {
		gameOverText.fontSize = 42;
		int x = Random.Range(0, 68);
		switch(x) {
			case 0:
			gameOverText.text = "GAAAAAME OOOOOOVER!!!!!!! buddey";
			break;

			case 1:
			gameOverText.text = "It's all over but the crying!";
			break;

			case 2:
			gameOverText.fontSize = 54;
			gameOverText.text = "GAME OVER YEAAAAAAAHHHHHH!!!!!";
			break;

			case 3:
			gameOverText.text = "Suck Methane";
			break;

			case 4:
			gameOverText.resizeTextForBestFit = true;
			gameOverText.text = "ouch";
			break;

			case 5:
			gameOverText.fontSize = 16;
			gameOverText.text = "ouch";
			break;

			case 6:
			gameOverText.text = "It looks like it's all over... FOR YOU!!!";
			break;

			case 7:
			gameOverText.fontSize = 50;
			gameOverText.text = "THOU ART MEATSAUCE!";
			break;

			case 8:
			gameOverText.fontSize = 84;
			gameOverText.text = "YOU FAILED!";
			break;

			case 9:
			gameOverText.text = "Another game over. Vote Yes On Lobotomies for App Designers.";
			break;

			case 10:
			gameOverText.text = "Yup. You're dead. Please try again.";
			break;

			case 11:
			gameOverText.text = "Well, if you keep THIS style of play up, I'll be a millionaire.";
			break;

			case 12:
			gameOverText.text = "Ya blew it kid.";
			break;

			case 13:
			gameOverText.text = "You call that a SOCKO? I laugh.";
			break;

			case 14:
			gameOverText.text = "Insert another quarter to continue.";
			break;

			case 15:
			gameOverText.text = "You're meatsauce bucko.";
			break;

			case 16:
			gameOverText.text = "We have no choice but to SOCKO you for poor play.";
			break;

			case 17:
			gameOverText.text = "You're busted.";
			break;

			case 18:
			gameOverText.text = "You thought this was a GAME? Well, it's OVER... for YOU!!!!!";
			break;

			case 19:
			gameOverText.text = "This is The End, my only friend.";
			break;

			case 20:
			gameOverText.text = "KAAAAAboom.";
			break;

			case 21:
			gameOverText.text = "That's it, you're grounded. Go to your room.";
			break;

			case 22:
			gameOverText.text = "We got you buster.";
			break;

			case 23:
			gameOverText.text = "Yes, you lost. What's your excuse?";
			break;

			case 24:
			gameOverText.text = "Your quest has ended... FOREVER!!!";
			break;

			case 25:
			gameOverText.text = "Sorry, buddy -- no take backs.";
			break;

			case 26:
			gameOverText.text = "You get NOTHING! YOU LOSE! GOOD DAY SIR!";
			break;

			case 27:
			gameOverText.text = "Back to the drawing board.";
			break;

			case 28:
			gameOverText.text = "Well, tough. We got no Wuss Mode here.";
			break;

			case 29:
			gameOverText.text = "YOU'RE FINISHED!!!";
			break;

			case 30:
			gameOverText.text = "You broke it. Now you must buy it.";
			break;

			case 31:
			gameOverText.text = "It's over. Perhaps you could try a different game?";
			break;

			case 32:
			gameOverText.text = "Game Over. I'm sure there's an app for your frustration.";
			break;

			case 33:
			gameOverText.text = "Your big fat reward? FORGET IT!";
			break;

			case 34:
			gameOverText.text = "The game is up, bub.";
			break;

			case 35:
			gameOverText.text = "Whoops!";
			break;

			case 36:
			gameOverText.text = "Soon this app will have TOMBSTONES as in-app purchases!";
			break;

			case 37:
			gameOverText.text = "You're doomed.";
			break;

			case 38:
			gameOverText.text = "You get NOTHING but a SOCKO to the NOSE!!!";
			break;

			case 39:
			gameOverText.text = "DUUUUUUUH.";
			break;

			case 40:
			gameOverText.text = "We'll ship the rest of you in two shoeboxes.";
			break;

			case 41:
			gameOverText.text = "Now there's a hole where your chest used to be.";
			break;

			case 42:
			gameOverText.text = "Wah-waaaaaah.";
			break;

			case 43:
			gameOverText.text = "Tough noogies.";
			break;

			case 44:
			gameOverText.text = "KABLAMMO!\nWe got you.";
			break;

			case 45:
			gameOverText.resizeTextForBestFit = true;
			gameOverText.text = "Yes, The End. You glance over your shoulder to check if anyone saw your silly little mistake.";
			break;

			case 46:
			gameOverText.text = "And it's over. Isn't this game a blast?";
			break;

			case 47:
			gameOverText.text = "Out with a bang.";
			break;

			case 48:
			gameOverText.resizeTextForBestFit = true;
			gameOverText.text = "BOOM!";
			break;

			case 49:
			gameOverText.text = "ARE YOU NOT ENTERTAINED???";
			break;

			case 50:
			gameOverText.text = "BRAVO! You're dead.";
			break;

			case 51:
			gameOverText.fontSize = 62;
			gameOverText.text = "WOW!\nYOU LOSE!";
			break;

			case 52:
			gameOverText.fontSize = 36;
			gameOverText.text = "IT'S OVER! YOU'RE FINISHED! YOU'RE THROUGH!";
			break;

			case 53:
			gameOverText.text = "Now you're a million (give or take a thousand) little pieces.";
			break;

			case 54:
			gameOverText.text = "Finally, the windows and smileys got their revenge.";
			break;

			case 55:
			gameOverText.text = "Just pick yourself up and glue yourself back together again.";
			break;

			case 56:
			gameOverText.text = "The horror.";
			break;

			case 57:
			gameOverText.text = "And with that, you are now deader than disco.";
			break;

			case 58:
			gameOverText.text = "Bits and pieces of you now cover a three-mile radius.";
			break;

			case 59:
			gameOverText.text = "We can't even use your remains as spare parts.";
			break;

			case 60:
			gameOverText.text = "That'll leave a mark.";
			break;

			case 61:
			gameOverText.fontSize = 32;
			gameOverText.text = "Look at what you did. Just wait 'til your father gets home!";
			break;

			case 62:
			gameOverText.text = "Want another hit?";
			break;

			case 63:
			gameOverText.text = "Masochistic, aren't we?";
			break;

			case 64:
			gameOverText.text = "DUUUUUUH. You are meatsauce because you are dumb.";
			break;

			case 65:
			gameOverText.text = "Hold on, Judy, there is no \"sock it to you\" tonight.";
			break;

			case 66:
			gameOverText.text = "Stupidity is not a crime, but in a game of SOCKO...";
			break;

			//Add this?
			case 67:
			gameOverText.text = "Cheer up! You still play better than a game journalist.";
			break;


			default:
			gameOverText.text = "GAAAAAME OVER!!!!!!! buddey";
			break;
		}
	}

	void NewHighScore() {
		if (MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE && !MasterControl.instance.scoreAttack) {
			if(gameController.GetComponent<GameController>().score >= gameController.GetComponent<GameController>().highScore) {
				newHighScoreText.text = "NEW HIGH SCORE!";
			} else if(gameController.GetComponent<GameController>().score == 0) {
				newHighScoreText.text = "DUDE, DID YOU EVEN TRY?????";
			} else if(gameController.GetComponent<GameController>().score < 100000) {
				newHighScoreText.text = "PLEASE TRY AGAIN!";
			} else if(gameController.GetComponent<GameController>().score < 250000) {
				newHighScoreText.text = "NICE! GOOD GOING!";
			} else if(gameController.GetComponent<GameController>().score < 500000) {
				newHighScoreText.text = "THAT WAS MOST EXCELLENT!";
			} else if(gameController.GetComponent<GameController>().score < 1000000) {
				newHighScoreText.text = "YOU ARE A REAL LORD OF SOCKO!";
			} else {
				newHighScoreText.text = "SOMEBODY PLEASE SHOOT ME!";
			}
		} else if (MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE && MasterControl.instance.scoreAttack) {
			if(gameController.GetComponent<GameController>().score >= gameController.GetComponent<GameController>().highScoreScoreAttack) {
				newHighScoreText.text = "NEW HIGH SCORE!";
			} else if(gameController.GetComponent<GameController>().score == 0) {
				newHighScoreText.text = "DUDE, DID YOU EVEN TRY?????";
			} else if(gameController.GetComponent<GameController>().score < 75000) {
				newHighScoreText.text = "PLEASE TRY AGAIN!";
			} else if(gameController.GetComponent<GameController>().score < 200000) {
				newHighScoreText.text = "NICE! GOOD GOING!";
			} else if(gameController.GetComponent<GameController>().score < 375000) {
				newHighScoreText.text = "WOOT WOOT! GREAT WORK!";
			} else if(gameController.GetComponent<GameController>().score < 750000) {
				newHighScoreText.text = "YOU ARE A REAL LORD OF SOCKO!";
			} else {
				newHighScoreText.text = "SOMEBODY PLEASE SHOOT ME!";
			}
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			if(gameController.GetComponent<GameController>().passChallenge == false) {
				newHighScoreText.text = "YOU HAVE FAILED!";
			} else if (gameController.GetComponent<GameController>().score >= gameController.GetComponent<GameController>().highScore) {
				newHighScoreText.text = "NEW HIGH SCORE!";
			} else {
				newHighScoreText.text = "NICE! BUT PLEASE TRY AGAIN!";
			}
		}
	}

	public void DisplayFinalStatus () {
		scoreText.text = "YOUR SCORE: " + gameController.GetComponent<GameController>().score.ToString();
		if(MasterControl.instance.scoreAttack) {
			highScoreText.text = "HIGH SCORE: " + gameController.GetComponent<GameController>().highScoreScoreAttack.ToString();
		} else {
			highScoreText.text = "HIGH SCORE: " + gameController.GetComponent<GameController>().highScore.ToString();
		}
		NewHighScore();
		coinText.text = "COINS EARNED: " + gameController.GetComponent<GameController>().coinScore.ToString();
		bonusCoinText.text = "BONUS COINS: " + gameController.GetComponent<GameController>().bonusCoinScore.ToString();
		totalCoinText.text = "TOTAL COINS: " + gameController.GetComponent<GameController>().savedCoinScore.ToString();
		finalStatusBox.SetActive(true);
	}

	IEnumerator GameOverCoroutine() {
		if(gameController.GetComponent<GameController>().gameOver == true) {
			yield return new WaitForSecondsRealtime(explosion.clip.length);
		}
		Time.timeScale = 0f;
		gameOverSound.Play();
		fadeBackground.SetActive(true);
		gameOverImage.SetActive(true);
		textCanvas.SetActive(true);
		retryButton.SetActive(false);
		selectFistsButton.SetActive(false);
		ReturnToStartButton.SetActive(false);
		yield return new WaitForSecondsRealtime(1.2f);
		gameOverText.color = tempColor;
		yield return new WaitForSecondsRealtime(gameOverSound.clip.length - 1.2f);
		//Run the final status functions here
		DisplayFinalStatus();
		retryButton.SetActive(true);
		selectFistsButton.SetActive(true);
		ReturnToStartButton.SetActive(true);
		MasterControl.instance.SetNumberOfSmileys();
		MasterControl.instance.SetNumberOfWindows();
		AchievementsController.instance.SetScore(gameController.GetComponent<GameController>().score);
		gameController.GetComponent<GameController>().SetHighScoreForLeaderboards();
	}
}
