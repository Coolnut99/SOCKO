using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinSmiley : MonoBehaviour {

	[SerializeField]
	private Sprite smileyRapsberry, smileyFace, sockoedSmiley;

	float timeToSwitchFace;

	bool rapsberry, smileyHit;

	// Use this for initialization
	void Start () {
		rapsberry = false;
		smileyHit = false;
		timeToSwitchFace = Random.Range(0f, 2f);
		GetComponent<SpriteRenderer>().sprite = smileyFace;
	}
	
	// Update is called once per frame
	void Update () {
		if(!smileyHit){
			GiveRapsberry();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "SOCKO ZONE") {
			smileyHit = true;
			GetComponent<SpriteRenderer>().sprite = sockoedSmiley;
		}
	}

	void GiveRapsberry() {
		timeToSwitchFace -= Time.deltaTime;
		if(timeToSwitchFace <= 0) {
			if(!rapsberry) {
				rapsberry = true;
				timeToSwitchFace = 1f;
				GetComponent<SpriteRenderer>().sprite = smileyRapsberry;
			} else {
				rapsberry = false;
				timeToSwitchFace = Random.Range(1f, 3f);
				GetComponent<SpriteRenderer>().sprite = smileyFace;
			}
		}
	}
}
