using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWindowCollector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Window") {
			Destroy(collider.gameObject);
		}
	}
}
