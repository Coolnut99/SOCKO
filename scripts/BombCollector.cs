using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Bomb") {
			Destroy(collider.gameObject);
		}
	}
}
