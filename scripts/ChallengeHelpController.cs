using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeHelpController : MonoBehaviour {

	[SerializeField]
	private Text challengeHelpText, pageText;

	private int page;
	private int maxPage;

	// Use this for initialization
	void Start () {
		page = 1;
		maxPage = 5;
		ChallengePage();
	}

	private void ChallengePage() {
		if (page < 1) {
			page = 1;
		}

		if (page > maxPage) {
			page = maxPage;
		}

		pageText.text = "Page " + page.ToString() + "/" + maxPage.ToString();

		switch(page) {

			case 1:
			challengeHelpText.text = "CHALLENGE:\n\nWe have many challenges for YOU to complete! Can you SOCKO your way through them?";
			break;

			case 2:
			challengeHelpText.text = "Select a number to pick that challenge. Complete it to unlock the next one. There are 36 in all!\n\nYour score is the total score of ALL completed challenges.";
			break;

			case 3:
			challengeHelpText.text = "You can redo a challenge for a higher score if you wish.\n\nYou get coins for completing a challenge -- but the reward is heavily reduced if you repeat a completed challenge.";
			break;

			case 4:
			challengeHelpText.text = "BUY ACCESS:\n\nIf a challenge is too hard for you, you can buy access to other challenges with coins. They are sold in sets of six.";
			break;

			case 5:
			challengeHelpText.text = "GOOD LUCK!\n\nYou'll need it.";
			break;
		}
	}

	public void PageUp() {
		page++;
		ChallengePage();
	}

	public void PageDown() {
		page--;
		ChallengePage();
	}
}
