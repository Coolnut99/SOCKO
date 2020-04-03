using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBurst : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 5f);
		//StartCoroutine(DestroyBurst());
	}
	/*
	IEnumerator DestroyBurst() {
		yield return new WaitForSecondsRealtime(5f);
		Destroy(gameObject, 5f);
	}*/
}
