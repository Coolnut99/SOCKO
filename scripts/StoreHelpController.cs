using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHelpController : MonoBehaviour {

	[SerializeField]
	private Text storeHelpText, pageText;

	[SerializeField]
	private Canvas helpCanvas;

	[SerializeField]
	private AudioSource validateChoice;

	[SerializeField]
	private Animator helpCanvasAnimator;

	private int page;
	private int maxPage;

	// Use this for initialization
	void Start () {
		helpCanvas.enabled = false;
		page = 1;
		maxPage = 7;
		StoreHelpPage();

	}

	private void StoreHelpPage() {
		if (page < 1) {
			page = 1;
		}

		if (page > maxPage) {
			page = maxPage;
		}

		pageText.text = "Page " + page.ToString() + "/" + maxPage.ToString();

		switch(page) {

			case 1:
			storeHelpText.text = "STORE:\n\nThis is where you can buy lives, fists, or get coins.\n\nUse these for an edge in your SOCKO-ing!";
			break;

			case 2:
			storeHelpText.text = "Each coin you SOCKO in the game adds one to your total. Coins are retained even if you lose or fail a mission.";
			break;

			case 3:
			storeHelpText.text = "BUY COINS\\REMOVE ADS:\n\nBuy coins from the app store with real-world money. You can also PERMANENTLY remove all ads (except optional ads for rewards) for a very small fee.";
			break;

			case 4:
			storeHelpText.text = "CAUTION: If you purchase something from the store, there are no refunds except under certain conditions.";
			break;

			case 5:
			storeHelpText.text = "BUY LIVES:\n\nYou can buy an extension of your total lives here, up to a maximum of nine. They are yours to keep regardless.";
			break;

			case 6:
			storeHelpText.text = "BUY FISTS:\n\nBuy various fists here. Some are cosmetic, but some have different rate of attack and radius.";
			break;

			case 7:
			storeHelpText.text = "WATCH AN AD:\n\nWatch an ad for a free 15 coins. There is a certain limit per day you can do this.";
			break;
		}
	}

	public void PageUp() {
		page++;
		StoreHelpPage();
	}

	public void PageDown() {
		page--;
		StoreHelpPage();
	}

	public void HelpSelect() {
		validateChoice.Play();
		helpCanvas.enabled = true;
		helpCanvasAnimator.Play("Panel Appear");
	}

	public void GoBack() {
		validateChoice.Play(); 
		helpCanvas.enabled = false;
	}
}
