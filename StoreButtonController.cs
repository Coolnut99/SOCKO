using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreButtonController : MonoBehaviour {

	[SerializeField]
	private GameObject button, pauseButton;

	[SerializeField]
	private GameController gameController;

	// Use this for initialization
	void Start () {
		if(MasterControl.instance.scoreAttack) {
			button.SetActive(false);
		} else {
			button.SetActive(true);
		}
	}

	void FixedUpdate() {
		if(gameController.lives <= 0) {
			button.GetComponent<Button>().interactable = false;
			pauseButton.GetComponent<Button>().interactable = false;
		} 
	}
}
