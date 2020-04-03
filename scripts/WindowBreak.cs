using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowBreak : MonoBehaviour {

	[HideInInspector]
	public bool windowBroken;

	public bool isBigWindow;

	[HideInInspector]
	public float score, bonus;

	[SerializeField]
	private Sprite windowIsIntact, windowIsBroken;

	public GameObject shrapnel;

	// Use this for initialization
	void Start () {
		score = 100f;
		bonus = 100f;
		GetComponent<SpriteRenderer>().sprite = windowIsIntact;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (bonus <= 0) {
			bonus = 0;
		} else {
			bonus -= (Time.deltaTime * 10);
		}
	}

	public void SetWindowAsBroken() {
		windowBroken = true;
		GetComponent<SpriteRenderer>().sprite = windowIsBroken;
	}

}
