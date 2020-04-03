using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

	[SerializeField]
	private Animator animator;

	// Use this for initialization
	void Start () {
		StartCoroutine(GoToNextScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator GoToNextScene() {
		yield return new WaitForSecondsRealtime(4f);
		animator.Play("Intro Fade Out");
		yield return new WaitForSecondsRealtime(1f);
		LoadSplash();
	}

	public void LoadSplash() {
		SceneManager.LoadScene("splash");
	}
}
