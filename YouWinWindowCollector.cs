using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinWindowCollector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Smiley" || collider.gameObject.tag == "Bomb" || collider.gameObject.tag == "Coin" || collider.gameObject.tag == "Powerup") {
			Destroy(collider.gameObject);
		} 
	}
}
