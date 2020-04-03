using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	[SerializeField]
	private GameObject explosion;
	GameObject explosionClone;
	private AudioSource explosionSound;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		explosionSound = GameObject.Find("Explosion Sound").GetComponent<AudioSource>();
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
		//Find GameController component
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.lifeLost == true) {
			Destroy(gameObject);
		}
		//If a life is lost, destroy this gameObject
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "SOCKO" && gameController.invincible == false && FistTable.instance.fistSpecial != 19) { //&& gameController.immunity == false
			explosionClone = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
			explosionClone.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
			explosionSound.Play();
			Destroy(gameObject);
		}
	}
}
