using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningImageScript : MonoBehaviour {

	[SerializeField]
	private Text text, NNNNHHHHText, ohnoText;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private GameController gameController;

	private bool lastLife;
	private bool ohNoBool;


	// Use this for initialization
	void Start () {
		lastLife = false;
		ohNoBool = false;
		text.text = "";
		NNNNHHHHText.text = "";
		ohnoText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		IsLastLife();
		SayNNNNHHHH();
		SayOhNo();
		if(gameController.textTimer <= 0f) {
			NNNNHHHHText.text = "";
			ohnoText.text = "";
			text.text = "";
		}
	}

	void IsLastLife() {
		if(!MasterControl.instance.scoreAttack) {
			if(gameController.lives == 1 && lastLife == false) {
				gameController.textTimer = 10f;
				lastLife = true;
				NNNNHHHHText.text = "";
				ohnoText.text = "";
				text.text = "TAP \"STORE\" ON BOTTOM RIGHT TO BUY EXTRA LIVES";
				animator.Play("warning text animation");
			}

			if(gameController.lives > 1 && lastLife == true) {
				gameController.textTimer = 0f;
				text.text = "";
				animator.Play("warning text idle");
				lastLife = false;
			}
	
			if(gameController.textTimer <= 0f && lastLife == true) {
				animator.Play("warning text idle");
				text.text = "";
			}
		}
	}

	void SayNNNNHHHH() {
		if(gameController.grunt == true) {
			gameController.grunt = false;
			if(gameController.textTimer <= 0f) {
				gameController.textTimer = 2f;
				text.text = "";
				NNNNHHHHText.resizeTextForBestFit = true;
				NNNNHHHHText.text = "NNNNNNNHHHHHH!!!!";
				ohnoText.text = "";
			}
		}
	}

	void SayOhNo() {
		if((!lastLife && gameController.lifeLost) || (MasterControl.instance.scoreAttack && gameController.lifeLost)) {
			gameController.textTimer = 2f;
			text.text = "";
			NNNNHHHHText.text = "";
			ohnoText.resizeTextForBestFit = true;
			if(ohnoText.text == "") {
				SetRandomText();
			}
		}
	}

	void SetRandomText() {
		int i = Random.Range(0, 6);

		switch(i) {
			case 0:
			ohnoText.text = "OH NO!";
			break;

			case 1:
			ohnoText.text = "WHOOPS!";
			break;

			case 2:
			ohnoText.text = "D'OH!";
			break;

			case 3:
			ohnoText.text = "GOTCHA!";
			break;

			case 4:
			ohnoText.text = "YOU FOOL!";
			break;

			case 5:
			ohnoText.text = "BWAHAHA!";
			break;

			default:
			ohnoText.text = "OH NO!";
			break;
		}
	}
}
