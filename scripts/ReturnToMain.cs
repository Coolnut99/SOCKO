using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour {

	public void ReturnToStart() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("Main Menu");
	}

	public void ReturnToStore() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("Store");
	}

	public void ReturnToMainOrChallenges() {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.CHALLENGE) {
			SceneManager.LoadScene("Challenges");
		} else {
			SceneManager.LoadScene("Main Menu");
		}
	}
}
