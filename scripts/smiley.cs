using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class smiley : MonoBehaviour {

	[SerializeField]
	private SpriteRenderer sockoedSmiley;

	[SerializeField]
	private Sprite smileyRapsberry, smileyFace;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private Text text;

	[SerializeField]
	private Canvas canvas;

	private Color smileySocko;

	private float timeToSwitchFace;

	public bool rapsberry;

	public AudioSource sockoedSmileySound;

	[HideInInspector]
	public float score, bonus;

	public bool smileyHit;

	// Use this for initialization
	void Start () {
		HideBonus();
		timeToSwitchFace = Random.Range(1.5f, 3f);
		rapsberry = false;
		score = 0f;
		bonus = 400f;
		smileyHit = false;
		sockoedSmileySound = GameObject.Find("Honk Sound").GetComponent<AudioSource>();
		smileySocko = sockoedSmiley.color;
		sockoedSmiley.color = new Color(0f, 0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(smileyHit == false) {
			GiveRapsberry();
		}
		if (bonus <= 0) {
			bonus = 0;
		} else {
			bonus -= (40 * Time.deltaTime);
		}
	}

	public void FinishSocko() {
		GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, 0f);
		GetComponent<CircleCollider2D>().radius = 0f;
		sockoedSmileySound.Play();
		sockoedSmiley.color = smileySocko; 
	}

	//Make the smileys taunt you occasionally
	void GiveRapsberry() {
		timeToSwitchFace -= Time.deltaTime;
		if(timeToSwitchFace <= 0) {
			if(!rapsberry) {
				rapsberry = true;
				timeToSwitchFace = 1f;
				GetComponent<SpriteRenderer>().sprite = smileyRapsberry;
			} else {
				rapsberry = false;
				timeToSwitchFace = Random.Range(2f, 3f);
				GetComponent<SpriteRenderer>().sprite = smileyFace;
			}
		}
	}

	//TEST for now
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.tag == "Window") {
			transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
			Debug.Log("Smiley collided with window");
		} else if (collider.gameObject.tag == "Smiley") {
			transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(0, 3f), transform.position.z);
			Debug.Log("Smiley collided with other smiley");
		}
	}

	public void ShowBonus(float bonus) {
		canvas.gameObject.SetActive(true);
		text.text = "BONUS! +" + bonus.ToString();
		animator.Play("bonus rise");
	}

	public void ShowBonus() {
		canvas.gameObject.SetActive(true);
		text.text = "BONUS!";
		animator.Play("bonus rise");
	}

	public void HideBonus() {
		text.text = "";
		canvas.gameObject.SetActive(false);
	}
}
