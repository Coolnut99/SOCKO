using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigWindow : MonoBehaviour {

	public GameObject bigWindowGlassBurst;
	GameObject glassBurstClone;

	[SerializeField]
	private Canvas canvas;

	[SerializeField]
	private Text text;

	private bool instantiatingGlass;

	// Use this for initialization
	void Start () {
		canvas.gameObject.SetActive(false);
		instantiatingGlass = false;
	}


	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Smiley") {
			Destroy(collider.gameObject);
		}
	}

	public void InstantiateGlass() {
		if(instantiatingGlass == false) {
			instantiatingGlass = true;
			//OhhhhYeahhhh();
			int x, y;
				for (x = -1; x < 2; x++) {
					for (y = -2; y < 1; y++) {
						glassBurstClone = Instantiate(bigWindowGlassBurst, transform.position, Quaternion.identity) as GameObject;
						glassBurstClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
						glassBurstClone.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0f);
				}
			}
		}
	}

	void OhhhhYeahhhh() {
		canvas.gameObject.SetActive(true);
	}

	public void SetBigWindowBonusText(float bonus) {
		canvas.gameObject.SetActive(true);
		text.text = "OHHHHHH YEAHHHHH!\nBonus + " + bonus.ToString();
	}
}
