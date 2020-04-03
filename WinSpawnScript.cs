using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSpawnScript : MonoBehaviour {

	[SerializeField]
	private GameObject smiley, glassBurst, youWinTextBox;
	GameObject smileyClone, windowClone;

	[SerializeField]
	private GameObject[] randomWindows, randomBrokenWindows;

	[SerializeField]
	private YouWinCameraScript youWinCameraScript;

	[SerializeField]
	private Text text;

	float f, x;
	float maxX, minX;
	float timeforWindow;

	private string winString, takeGoodRest, thankYou;

	void Awake() {
		text.text = "";
		youWinTextBox.SetActive(false);
		winString = "You have proven to the world that you truly are the LORD OF SOCKO!!!!!";
		takeGoodRest = "\n\nTAKE GOOD REST!";
		thankYou = "\n\nThank you for playing!";
	}


	// Use this for initialization
	void Start () {
		timeforWindow = 7.5f;
		SetMinAndMaxX();
		f = 0.06f;
		x = f;
		StartCoroutine(WinShatteredWindow());
	}
	
	// Update is called once per frame
	void Update () {
		if(youWinCameraScript.moveCamera) {
			x -= Time.deltaTime;
			timeforWindow -= Time.deltaTime;
			if(x <= 0f && timeforWindow > 0f) {
				CreateSmiley();
				x = f;
			}
		}
	}

	void CreateSmiley() {
		float smileX = Random.Range(minX, maxX);
		smileyClone = Instantiate(smiley, transform.position, Quaternion.identity) as GameObject;
		smileyClone.transform.position = new Vector3(smileX, transform.position.y, 0f);
	}

	void SetMinAndMaxX() {
		Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

		maxX = bounds.x - 0.25f;
		minX = -bounds.x + 0.25f;
	}

	IEnumerator WinShatteredWindow() {
		yield return new WaitForSecondsRealtime(9.5f);
		int r = Random.Range(0, randomWindows.Length);
		windowClone = Instantiate(randomWindows[r], transform.position, Quaternion.identity) as GameObject;
		windowClone.transform.position = new Vector3(0f, transform.position.y, 0f);
		yield return new WaitForSecondsRealtime(2.5f);
		windowClone.GetComponent<SpriteRenderer>().sprite = randomBrokenWindows[r].GetComponent<SpriteRenderer>().sprite;
		InstantiateGlass();
		yield return new WaitForSecondsRealtime(1f);
		youWinTextBox.SetActive(true);
		text.text += winString;
		yield return new WaitForSecondsRealtime(1f);
		text.text += takeGoodRest;
		yield return new WaitForSecondsRealtime(1f);
		text.text += thankYou;
	}

	public void InstantiateGlass() {
		int x, y;
		for (x = -2; x < 3; x++) {
			for (y = -3; y < 2; y++) {
				Instantiate(glassBurst, new Vector3(x + windowClone.transform.position.x, y + windowClone.transform.position.y, 0f), Quaternion.identity);
				Debug.Log("WOOT WOOT WOOT WOOT");
			}
		}
	}
}
