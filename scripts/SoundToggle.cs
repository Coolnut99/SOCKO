using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {

	[SerializeField]
	private Sprite onSprite, offSprite;
	private int soundOn;

	[SerializeField]
	private AudioSource sockoSound;

	private Color onColor, offColor;

	// Use this for initialization
	void Awake () {
		soundOn = GamePreferences.GetMusicState();
		onColor = new Color (1f, 1f, 1f, 1f);
		offColor = new Color (0.5f, 0.5f, 0.5f, 1f);
		SetSound();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleSound() {
		if(soundOn == 1) {
			Debug.Log("Turning sound off.");
			//GetComponent<Button>().image.sprite = offSprite;
			GetComponent<Image>().sprite = offSprite;
			GetComponent<Button>().image.color = offColor;
			soundOn = 0;
			AudioListener.volume = 0;
			GamePreferences.SetMusicState(soundOn);
		} else {
			Debug.Log("Turning sound on.");
			//GetComponent<Button>().image.sprite = onSprite;
			GetComponent<Image>().sprite = onSprite;
			GetComponent<Button>().image.color = onColor;
			soundOn = 1;
			AudioListener.volume = 1;
			sockoSound.Play();
			GamePreferences.SetMusicState(soundOn);
		}
	}

	public void SetSound() {
		if(soundOn == 1) {
			GetComponent<Image>().sprite = onSprite;
			//GetComponent<Button>().image.sprite = onSprite;
			GetComponent<Button>().image.color = onColor;
		} else {
			GetComponent<Image>().sprite = offSprite;
			//GetComponent<Button>().image.sprite = offSprite;
			GetComponent<Button>().image.color = offColor;
		}
	}
}
