using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCoinsText : MonoBehaviour {

	[SerializeField]
	private Text text;

	// Use this for initialization
	void Start () {
		SetCoinsText();
	}
	
	// Update is called once per frame
	public void SetCoinsText() {
		text.text = "COINS: " + PlayerPrefs.GetInt(GamePreferences.CoinScore).ToString();
	}
}
