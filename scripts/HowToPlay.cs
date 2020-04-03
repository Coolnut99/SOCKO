using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour {

	[SerializeField]
	private GameObject [] helpFile;

	[SerializeField]
	private Text pageText;

	private int pageNumber, maxPages;

	// Use this for initialization
	void Start () {
		Debug.Log(helpFile.Length);
		pageNumber = 0;
		maxPages = 11;
		for(int x = 0; x < helpFile.Length; x++) {
			helpFile[x].SetActive(false);
		}
		helpFile[0].SetActive(true);
		AdjustPageNumber();
	}

	public void PageUp() {
		if(pageNumber < helpFile.Length - 1) {
			helpFile[pageNumber].SetActive(false);
			pageNumber++;
			helpFile[pageNumber].SetActive(true);
			AdjustPageNumber();
		}
	}

	public void PageDown() {
		if(pageNumber > 0) {
			helpFile[pageNumber].SetActive(false);
			pageNumber--;
			helpFile[pageNumber].SetActive(true);
			AdjustPageNumber();
		}
	}

	void AdjustPageNumber() {
		int p;
		p = pageNumber + 1;
		if (p > maxPages) {
			p = maxPages;
		}
		pageText.text = "Page: " + p.ToString() + "/" + maxPages.ToString();
	}


}
