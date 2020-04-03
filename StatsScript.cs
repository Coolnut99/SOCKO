using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour {

	[SerializeField]
	private Text timesPlayedText, quipLevelText, numberOfSmileysHit, numberOfWindowsHit, adsOnDisplay;

	int timesPlayed, quipNumber;

	// Use this for initialization
	void Start () {
		timesPlayed = PlayerPrefs.GetInt(GamePreferences.TotalPlays);
		timesPlayedText.text = "TIMES PLAYED: " + timesPlayed.ToString();
		quipNumber = PlayerPrefs.GetInt(GamePreferences.QuipLevel);
		quipLevelText.text = "QUIP LEVEL: " + quipNumber.ToString();
		numberOfSmileysHit.text = "SMILEYS SOCKOED: " + MasterControl.instance.numberOfSmileys.ToString();
		numberOfWindowsHit.text = "WINDOWS HIT: " + MasterControl.instance.numberOfWindows.ToString();
		int i = PlayerPrefs.GetInt(GamePreferences.CanShowAds);
		if(i > 0) {
			adsOnDisplay.text = "ADS ACTIVE: TRUE";
		} else {
			adsOnDisplay.text = "ADS ACTIVE: FALSE\nTHANK YOU!";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
