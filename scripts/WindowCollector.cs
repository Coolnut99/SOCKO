using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowCollector : MonoBehaviour {

	// If the window collector collides with a smiley, coin, or bomb, destroy it
	// If it collides with a window, check if it is not broken; if not, then the player loses a life

	[SerializeField]
	private GameController gameController;

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Smiley" || collider.gameObject.tag == "Bomb" || collider.gameObject.tag == "Coin" || collider.gameObject.tag == "Powerup") {
			Destroy(collider.gameObject);
		} else if(collider.gameObject.tag == "Window") {
			if(collider.gameObject.GetComponent<WindowBreak>().windowBroken == false && collider.gameObject.GetComponent<WindowBreak>().isBigWindow == false) {
				Debug.Log("Window not broken!");
				gameController.LoseALifeFromWindow();
			}
			if(collider.gameObject.GetComponent<WindowBreak>().isBigWindow == false) {
				Destroy(collider.gameObject);
			}
		}
	}
}
