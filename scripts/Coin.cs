using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	[SerializeField]
	private GameObject explosion;
	GameObject explosionClone;

	[HideInInspector]
	public float score, bonus;

	// Use this for initialization
	void Start () {
		score = 1000f;
		if (FistTable.instance.fistSpecial == 14) {
			score *= 2f;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "SOCKO") {
			explosionClone = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
			explosionClone.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		}
	}
}
