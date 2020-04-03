using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

	public float scaledNumber;
	// Use this for initialization
	void Awake () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Vector3 tempScale = transform.localScale;

		//float width = sr.sprite.bounds.size.x;  // Cut for now -- not being used at the moment. Restore if there are any issues.
		float height = sr.sprite.bounds.size.y;
		float worldHeight = Camera.main.orthographicSize * 2f;
		//float worldWidth = worldHeight / Screen.height * Screen.width; // Cut for now -- not being used at the moment. Restore if there are any issues.

		tempScale.y = worldHeight / height;
		tempScale.x = tempScale.y;

		transform.localScale = tempScale;
		scaledNumber = tempScale.y;

	}
	

}
