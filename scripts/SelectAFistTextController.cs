using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAFistTextController : MonoBehaviour {

	[SerializeField]
	private Text text;

	// Use this for initialization
	void Start () {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			text.resizeTextForBestFit = true;
			text.text = "RETURN TO CHALLENGES";
		} else {
			text.text = "RETURN TO MAIN";
		}
	}
}
