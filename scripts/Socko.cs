using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Socko : MonoBehaviour {

	private GameController gameController;

	//private SoundController soundController;

	//private CameraScript cameraScript;

	// Use this for initialization
	void Awake () { //Change to Start() if things go wrong
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
		//soundController = gameController.Find("Sound Controller").GetComponent<SoundController>();
		//cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
		GetComponent<CircleCollider2D>().radius *= FistTable.instance.fistRadiusScale;
	}

	void DestroySocko() {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "UI Background") {
			Destroy(gameObject);
		}

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
				RandomGrunt();
				gameController.windowBroken = true;
				gameController.numberWindowsBroken++;
				gameController.UpdateScore();
				gameController.windowSpawner.giantWindowDistance -= 10f;
				MasterControl.instance.numberOfWindows++;
			}
		}

		if(collider.gameObject.tag == "Coin") {
			//soundController.PlayCoinSound();
			gameController.soundController.PlayCoinSound();
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

		if(collider.gameObject.tag == "Powerup") {
			//soundController.PlayPowerupSound();
			gameController.soundController.PlayPowerupSound();
			if(FistTable.instance.fistSpecial == 3) {
				gameController.score += 200f;
			} else if (FistTable.instance.fistSpecial == 6) {
				gameController.score += 500f;
			}
			gameController.score += collider.gameObject.GetComponent<Coin>().score;
			gameController.UpdateScore();
			gameController.PowerUp(collider.gameObject.GetComponent<Powerup>().powerupType);
			Destroy(collider.gameObject);
		}

		if(collider.gameObject.tag == "Smiley") {
			if(collider.gameObject.GetComponent<smiley>().smileyHit == false) {
				if(FistTable.instance.fistSpecial == 4) {
					gameController.score += 100f;
				}
				collider.gameObject.GetComponent<smiley>().smileyHit = true;
				gameController.soundController.PlayWhapSound();
				RandomGrunt();
				gameController.score += Mathf.Round(((collider.gameObject.GetComponent<smiley>().score + collider.gameObject.GetComponent<smiley>().bonus) + gameController.smileyBonus) * gameController.smileyMultiplier);
				gameController.smileyHit = true;
				gameController.numberSmileysHit++;
				gameController.SetSmileyBonus();
				MasterControl.instance.numberOfSmileys++;
				if(collider.gameObject.GetComponent<smiley>().rapsberry == true) {
					gameController.windowSpawner.giantWindowDistance -= 100f;
					Debug.Log("BONUS!");
					float f = gameController.SmileyRapsberryBonus();
					gameController.score += f;
					collider.gameObject.GetComponent<smiley>().ShowBonus(f);
				}
				gameController.UpdateScore();
				gameController.windowSpawner.giantWindowDistance -= 20f;
				collider.gameObject.GetComponent<smiley>().FinishSocko();
			}
		}
		if(collider.gameObject.tag == "Bomb" && gameController.GetComponent<GameController>().invincible == false && gameController.immunity == false && FistTable.instance.fistSpecial != 19) {
			gameController.soundController.PlayWhapSound();
			//soundController.PlayWhapSound();
			//gameController.lives -= 1;
			//gameController.UpdateLives();
			gameController.DestroyAllBombs();
			//Uncomment this if it creates issues
			//if(gameController.lives <= 0) {
				//gameController.GetComponent<CameraScript>().moveCamera = false;
				//cameraScript.moveCamera = false;
			//}
		}
	}

	void RandomGrunt() {
		gameController.hitsBeforeGrunt--;
		if(gameController.hitsBeforeGrunt <= 0) {
			gameController.soundController.PlayGruntSound();
			AchievementsController.instance.NNNNNNHHHHHH();
			gameController.SetHitsBeforeGrunt();
		}
	}

	void AutoGrunt() {
		gameController.hitsBeforeGrunt = 0;
	}
}
