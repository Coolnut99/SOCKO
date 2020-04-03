using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YouWinFadePanel : MonoBehaviour {

	[SerializeField]
	private Image panelImage;

	[SerializeField]
	private GameObject congratsImage;

	[SerializeField]
	private YouWinCameraScript youWinCameraScript;

	[SerializeField]
	private AudioSource youWinMusic;

	private Color imageColor;

	float f;

	bool musicStarted, canExit;

	void Awake() {
		canExit = false;
	}

	// Use this for initialization
	void Start () {
		congratsImage.SetActive(false);
		musicStarted = false;
		youWinMusic.PlayDelayed(1f);
		imageColor = panelImage.color;
		f = 1f;
		StartCoroutine(FinishYouWin());
	}
	
	// Update is called once per frame
	void Update () {
		f -= Time.deltaTime;
		if(f >= 0f) {
			panelImage.color = new Color(0f, 0f, 0f, f);
		} else {
			f = 0f;
		}
		if(Input.GetMouseButtonDown(0) && canExit == true) {
			SceneManager.LoadScene("Challenges");
		}
	}

	IEnumerator FinishYouWin() {
		yield return new WaitForSecondsRealtime(11f);
		congratsImage.SetActive(true);
		youWinCameraScript.moveCamera = false;
		yield return new WaitForSecondsRealtime(4f);
		canExit = true;
	}
}
