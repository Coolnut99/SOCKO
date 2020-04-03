using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YoureWinner : MonoBehaviour {

	public void GoToWinPage() {
		SceneManager.LoadScene("You Win");
	}
}
