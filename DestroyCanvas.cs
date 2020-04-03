using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCanvas : MonoBehaviour {

	[SerializeField]
	private smiley smileyScript;

	void Start() {
		
	}

	public void DestroyThis() {
		smileyScript.HideBonus();
	}
}
