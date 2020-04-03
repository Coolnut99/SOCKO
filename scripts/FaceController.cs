using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceController : MonoBehaviour {

	[SerializeField]
	private GameObject normalImage, happyImage, smugImage, arghImage, sleepingImage, ohNoImage;
	private float happyImageTime, smugImageTime, arghImageTime, ohNoTime;

	// Use this for initialization
	void Start () {
		ResetToDefaultImage();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(happyImageTime > 0f) {
			happyImageTime -= Time.deltaTime;
		} 

		if(smugImageTime > 0f) {
			smugImageTime -= Time.deltaTime;
		} 

		if(arghImageTime > 0f) {
			arghImageTime -= Time.deltaTime;
		}

		if(ohNoTime > 0f) {
			ohNoTime -= Time.deltaTime;
		}

		if(happyImageTime <= 0f && smugImageTime <= 0f && arghImageTime <= 0f && ohNoTime <= 0f) {
			ResetToDefaultImage();
		}

	}

	void ResetAllImages() {
		normalImage.SetActive(false);
		happyImage.SetActive(false);
		smugImage.SetActive(false);
		arghImage.SetActive(false);
		ohNoImage.SetActive(false);
		sleepingImage.SetActive(false);
		happyImageTime = 0f;
		smugImageTime = 0f;
		arghImageTime = 0f;
		ohNoTime = 0f;
	}

	public void SetHappyImage(float f) {
		ResetAllImages();
		happyImageTime = f;
		happyImage.SetActive(true);
	}

	public void SetSmugImage(float f) {
		ResetAllImages();
		smugImageTime = f;
		smugImage.SetActive(true);
	}

	public void SetArghImage(float f) {
		ResetAllImages();
		arghImageTime = f;
		arghImage.SetActive(true);
	}

	public void SetOhNoImage(float f) {
		ResetAllImages();
		ohNoTime = f;
		ohNoImage.SetActive(true);
	}


	public void ResetToDefaultImage() {
		if(normalImage.activeInHierarchy == false) {
		ResetAllImages();
		normalImage.SetActive(true);
		}
	}

	public void PauseFace() {
		normalImage.SetActive(false);
		happyImage.SetActive(false);
		smugImage.SetActive(false);
		arghImage.SetActive(false);
		ohNoImage.SetActive(false);
		sleepingImage.SetActive(true);
	}

	public void ResumeFace() {
		sleepingImage.SetActive(false);
		if(happyImageTime > 0f) {
			happyImage.SetActive(true);
		} else if(smugImageTime > 0f) {
			smugImage.SetActive(true);
		} else if(arghImageTime > 0f) {
			arghImage.SetActive(true);
		} else if(ohNoTime > 0f) {
			ohNoImage.SetActive(true);
		} else {
			ResetToDefaultImage();
		}
	}
}
