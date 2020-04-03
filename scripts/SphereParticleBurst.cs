using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereParticleBurst : MonoBehaviour {
	// Use this for initialization
	void Start () {
	//	Destroy(gameObject, 1f);
		StartCoroutine(DestroyBurst());
	}

	IEnumerator DestroyBurst() {
		yield return new WaitForSecondsRealtime(1f);
		Destroy(gameObject);
	}
}
