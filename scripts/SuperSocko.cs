using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSocko : MonoBehaviour {

	//private AudioSource windowBreak, whapSound, coinSound; //powerupSound; //Remove powerupSound for now -- restore if there are any issues

	private GameController gameController;

	private SoundController soundController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
		soundController = GameObject.Find("Sounds").GetComponent<SoundController>();
	}

	public void DestroySocko() {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if(collider.gameObject.tag == "Window") {
			if(collider.gameObject.GetComponent<WindowBreak>().windowBroken == false) {
				//collider.gameObject.GetComponent<Animator>().Play("Window broken");
				collider.gameObject.GetComponent<WindowBreak>().SetWindowAsBroken();
				//soundController.PlayWindowBreak();
				//collider.gameObject.GetComponent<WindowBreak>().windowBroken = true;
				Instantiate(collider.gameObject.GetComponent<WindowBreak>().shrapnel, collider.gameObject.transform.position, Quaternion.identity);
				gameController.score += Mathf.Round((collider.gameObject.GetComponent<WindowBreak>().score + collider.gameObject.GetComponent<WindowBreak>().bonus) * gameController.windowMultiplier);
				if(collider.gameObject.GetComponent<WindowBreak>().isBigWindow == true) {
					gameController.numberOfBigWindowsHit++;
					gameController.score += (1000f * gameController.numberOfBigWindowsHit);
					gameController.SlowDownFromHittingBigWindow();
					collider.gameObject.GetComponent<BigWindow>().SetBigWindowBonusText(1000f * gameController.numberOfBigWindowsHit);
					collider.gameObject.GetComponent<BigWindow>().InstantiateGlass();
					gameController.soundController.PlayBigWindowBreak();
					AutoGrunt();
				} else {
					gameController.soundController.PlayWindowBreak();
				}
				//RandomGrunt();
				gameController.windowBroken = true;
				gameController.numberWindowsBroken++;
				gameController.UpdateScore();
				gameController.windowSpawner.giantWindowDistance -= 20f;
				MasterControl.instance.numberOfWindows++;
			}
		}

		if(collider.gameObject.tag == "Coin") {
			soundController.PlayCoinSound();
			if(FistTable.instance.fistSpecial == 2) {
				gameController.score += 200f;
			} else if (FistTable.instance.fistSpecial == 5) {
				gameController.score += 500f;
			} 
			gameController.score += collider.gameObject.GetComponent<Coin>().score;
			gameController.UpdateScore();
			if(FistTable.instance.fistSpecial == 8) {
				gameController.coinScore += 2;
			} else if (FistTable.instance.fistSpecial == 9){
				gameController.coinScore += 3;
			} else if (FistTable.instance.fistSpecial != 13 && FistTable.instance.fistSpecial != 15) {
				gameController.coinScore++;
			}
			Destroy(collider.gameObject);
		}
		/* Have it not affect powerups, for now
		if(collider.gameObject.tag == "Powerup") {
			powerupSound.Play();
			gameController.score += collider.gameObject.GetComponent<Coin>().score;
			gameController.UpdateScore();
			gameController.PowerUp(collider.gameObject.GetComponent<Powerup>().powerupType);
			Destroy(collider.gameObject);
		}*/

		if(collider.gameObject.tag == "Smiley") {
			if(collider.gameObject.GetComponent<smiley>().smileyHit == false) {
				if(FistTable.instance.fistSpecial == 4) {
					gameController.score += 100f;
				}
				collider.gameObject.GetComponent<smiley>().smileyHit = true;
				soundController.PlayWhapSound();
				gameController.score += Mathf.Round(collider.gameObject.GetComponent<smiley>().score + collider.gameObject.GetComponent<smiley>().bonus) + gameController.GetComponent<GameController>().smileyBonus;
				gameController.smileyHit = true;
				gameController.numberSmileysHit++;
				gameController.SetSmileyBonus();
				if(collider.gameObject.GetComponent<smiley>().rapsberry == true) {
					gameController.windowSpawner.giantWindowDistance -= 80f;
					Debug.Log("BONUS!");
					float f = gameController.SmileyRapsberryBonus();
					gameController.score += f;
					collider.gameObject.GetComponent<smiley>().ShowBonus(f);
				}
				gameController.UpdateScore();
				gameController.windowSpawner.giantWindowDistance -= 20f;
				MasterControl.instance.numberOfSmileys++;
				collider.gameObject.GetComponent<smiley>().FinishSocko();
			}
		}
	}

	void RandomGrunt() {
		gameController.hitsBeforeGrunt--;
		if(gameController.hitsBeforeGrunt <= 0) {
			gameController.soundController.PlayGruntSound();
			//soundController.PlayGruntSound();
			AchievementsController.instance.NNNNNNHHHHHH();
			gameController.SetHitsBeforeGrunt();
		}
	}

	void AutoGrunt() {
		gameController.hitsBeforeGrunt = 0;
	}
}
