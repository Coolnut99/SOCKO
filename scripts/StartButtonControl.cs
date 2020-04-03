using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonControl : MonoBehaviour {

	[SerializeField]
	private Button button;

	[SerializeField]
	private FistStoreController fistStoreController;

	// Use this for initialization
	void Start () {
		SetButton();
	}
	
	// Update is called once per frame
	void Update () {
		SetButton();
	}

	public void SetButton() {
		if(fistStoreController.fistUnlocked == 1) {
			button.interactable = true;
		} else {
			button.interactable = false;
		}
	}
}
