using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAFistScoreAttackButton : MonoBehaviour {

	[SerializeField]
	private GameObject selectButton, returnButton, selectButtonScoreAttack, scoreAttackButtonScoreAttack, returnButtonScoreAttack;

	[SerializeField]
	private FistStoreController fistStoreController;

	// Use this for initialization
	void Start () {
		SetScoreAttack();
	}

	void FixedUpdate() {
		if(fistStoreController.selectFist == 1 && MasterControl.instance.set_difficulty != MasterControl.difficulty.CHALLENGE) {
			SetScoreAttack();
		} else {
			DisableScoreAttack();
		}
	}

	public void SetScoreAttack() {
		selectButton.SetActive(false);
		returnButton.SetActive(false);
		selectButtonScoreAttack.SetActive(true);
		scoreAttackButtonScoreAttack.SetActive(true);
		returnButtonScoreAttack.SetActive(true);
	}

	public void DisableScoreAttack() {
		selectButton.SetActive(true);
		returnButton.SetActive(true);
		selectButtonScoreAttack.SetActive(false);
		scoreAttackButtonScoreAttack.SetActive(false);
		returnButtonScoreAttack.SetActive(false);
	}
}
