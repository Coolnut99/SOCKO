using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoAdsController : MonoBehaviour {

	[SerializeField]
	private Button button;

	// Use this for initialization
	void Start () {
		SetToNoAds();
	}

	public void SetToNoAds() {
		int i = PlayerPrefs.GetInt(GamePreferences.CanShowAds);
		if (i == 0) {
			button.interactable = false;
		}
	}
}
