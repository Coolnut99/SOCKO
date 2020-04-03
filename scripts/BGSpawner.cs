using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour {

	private GameObject[] backgrounds;

	private float lastY;

	private float scaledN;

	// Use this for initialization
	void Start () {
		GetBackgroundsAndSetLastY();
		scaledN = backgrounds[0].GetComponent<BGScaler>().scaledNumber;
		Debug.Log(scaledN);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GetBackgroundsAndSetLastY() {
		backgrounds = GameObject.FindGameObjectsWithTag("Background");

		lastY = backgrounds[0].transform.position.y;

		for (int i = 1; i < backgrounds.Length; i++) {
			if(lastY < backgrounds[i].transform.position.y) {
				lastY = backgrounds[i].transform.position.y;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Background") {
			if(collider.transform.position.y == lastY) {
				Vector3 temp = collider.transform.position;
				float height = (((BoxCollider2D)collider).size.y * scaledN);
				Debug.Log(height);

				for (int i = 0; i < backgrounds.Length; i++) {
					if(!backgrounds[i].activeInHierarchy) {

					temp.y += height;
					lastY = temp.y;
					backgrounds[i].transform.position = temp;
					backgrounds[i].SetActive(true);
					}
				}
			}
		}
	}
}
