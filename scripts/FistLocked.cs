using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FistLocked : MonoBehaviour {

	[SerializeField]
	private GameObject fistLock;

	[SerializeField]
	private FistStoreController fistStoreController;

	public string fistLockName;

	public int fistNumber;

	private int isUnlocked;

	private Color lockedColor, unlockedColor;

	void Awake() {
		lockedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
		unlockedColor = new Color(1f, 1f, 1f, 1f);
	}

	// Use this for initialization
	void Start () {
		SetUnlock();
	}

	public void SelectFist() {
		fistStoreController.GetComponent<FistStoreController>().selectFist = fistNumber;
		fistStoreController.GetComponent<FistStoreController>().BuySelectedFist();
	}

	public void SetUnlock() {
		isUnlocked = PlayerPrefs.GetInt(fistLockName);
		if(isUnlocked == 1) {
			fistLock.SetActive(false);
			GetComponent<Image>().color = unlockedColor;
		} else {
			fistLock.SetActive(true);
			GetComponent<Image>().color = lockedColor;
		}
	}
}
