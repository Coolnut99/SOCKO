using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalController : MonoBehaviour {

	[SerializeField]
	private GameObject normalMedal, scoreAttackMedal;

	[SerializeField]
	private Sprite bronzeMedal, silverMedal, goldMedal;


	public enum medal_type {none, bronze, silver, gold};
	// Use this for initialization
	void Start () {
		normalMedal.SetActive(false);
		scoreAttackMedal.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMedal(medal_type medal) {
		switch(medal) {
			case medal_type.none:
			normalMedal.SetActive(false);
			break;

			case medal_type.bronze:
			normalMedal.GetComponent<Image>().sprite = bronzeMedal;
			normalMedal.SetActive(true);
			break;

			case medal_type.silver:
			normalMedal.GetComponent<Image>().sprite = silverMedal;
			normalMedal.SetActive(true);
			break;

			case medal_type.gold:
			normalMedal.GetComponent<Image>().sprite = goldMedal;
			normalMedal.SetActive(true);
			break;

			default:
			normalMedal.SetActive(false);
			break;
		}
	}

	public void SetScoreAttackMedal(medal_type medal) {
		switch(medal) {
			case medal_type.none:
			scoreAttackMedal.SetActive(false);
			break;

			case medal_type.bronze:
			scoreAttackMedal.GetComponent<Image>().sprite = bronzeMedal;
			scoreAttackMedal.SetActive(true);
			break;

			case medal_type.silver:
			scoreAttackMedal.GetComponent<Image>().sprite = silverMedal;
			scoreAttackMedal.SetActive(true);
			break;

			case medal_type.gold:
			scoreAttackMedal.GetComponent<Image>().sprite = goldMedal;
			scoreAttackMedal.SetActive(true);
			break;

			default:
			scoreAttackMedal.SetActive(false);
			break;
		}
	}


}
