using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	[SerializeField]
	private AudioSource whapSound, swingSound, coinSound, powerupSound, gruntSound;

	[SerializeField]
	private AudioSource [] windowBreak;

	[SerializeField]
	private AudioSource [] bigWindowBreak;

	public void PlayWhapSound() {
		whapSound.Play();
	}

	public void PlaySwingSound() {
		swingSound.Play();
	}

	public void PlayCoinSound() {
		coinSound.Play();
	}

	public void PlayPowerupSound() {
		powerupSound.Play();
	}

	public void PlayWindowBreak() {
		int i = Random.Range(0, windowBreak.Length);
		windowBreak[i].Play();
	}

	public void PlayBigWindowBreak() {
		int i = Random.Range(0, bigWindowBreak.Length);
		bigWindowBreak[i].Play();
	}

	public void PlayGruntSound() {
		gruntSound.Play();
	}
}
