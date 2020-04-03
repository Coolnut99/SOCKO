using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeToSockoController : MonoBehaviour {

	int i;
	int j;
	int k;
	float f;

	[SerializeField]
	private Text introText;

	// Use this for initialization
	void Start () {
		i = 1;
		j = 0;
		f = 0;
	}
	
	// Update is called once per frame
	void Update () {
		f += Time.deltaTime;
		if(f <= 1) {
			introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, f);
		} else if(f <= 4){
			introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 1);
		} else if(f > 4) {
			introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 5 - f);
		}

		if(f >= 5) {
			f = 0;
			SwitchCaption();
		}
	}

	public void SwitchCaption() {
		introText.resizeTextForBestFit = false;
		i++;
		j++;
		if(i > 7) {
			i = 1;
		}
		if (j < 35) {
			switch (i) {

				case 1:
				introText.text = "WELCOME TO SOCKO: THE GAME!";
				break;

				case 2:
				introText.text = "ARE YOU READY TO BREAK STUFF???";
				break;

				case 3:
				introText.resizeTextForBestFit = true;
				introText.text = "START GAME: Selects game modes Easy, Medium, Hard, Challenge";
				break;

				case 4:
				introText.resizeTextForBestFit = true;
				introText.text = "HOW TO PLAY: Game instructions and tips";
				break;

				case 5:
				introText.resizeTextForBestFit = true;
				introText.text = "STORE: Buy coins, fists, lives, etc. here";
				break;

				case 6:
				introText.text = "ABOUT GAME: Game credits";
				break;

				case 7:
				introText.resizeTextForBestFit = true;
				introText.text = "QUIT GAME: Do you REALLY want to quit this most awesome game?";
				break;
			}
		} else {
				i = 0;
				j = 0;
				k = Random.Range(0, 3);	//Add more later on?
					switch(k) {

						case 0:
						introText.resizeTextForBestFit = true;
						introText.text = "EAT AT JOE'S";
						break;

						case 1:
						introText.resizeTextForBestFit = true;
						introText.text = "COOLNUT IS WATCHING YOU";
						break;

						case 2:
						introText.resizeTextForBestFit = true;
						introText.text = "BUY MOAR KOINZZ";
						break;

						default:
						introText.resizeTextForBestFit = true;
						introText.text = "COOLNUT IS WATCHING YOU";
						break;
			}
		}
	}
}
