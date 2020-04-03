using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutTextController : MonoBehaviour {

	[SerializeField]
	private Text text;

	private int page;
	private float f;
	int maxPages = 3;

	// Use this for initialization
	void Start () {
		f = 0;
		TurnPage();
	}
	
	// Update is called once per frame
	void Update () {
		f += Time.deltaTime;
		if(f <= 1) {
			text.color = new Color(text.color.r, text.color.g, text.color.b, f);
		} else if(f <= 5){
			text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
		} else if(f > 5) {
			text.color = new Color(text.color.r, text.color.g, text.color.b, 6 - f);
		}

		if(f >= 6) {
			f = 0;
			TurnPage();
		}

		if(Input.GetMouseButtonDown(0) && f < 5) {
			f = 5;
		}
	}

	void TurnPage() {
		page++;
		if(page > maxPages) {
			page = 1;
		}
		switch (page) {
			case 1:
			text.text = "SOCKO: The Video Game\n\nProgram, Game Design, and Quips\nFred Delles\n\n"
					  + "Character Art\nchrisdarkerart\nhttp://www.fiverr.com/chrisdarkerart\n\n"
					  + "Window and Fist Art\nCyan Tomato Studio\nhttp://www.fiverr.com/tomatoslice\n\n"
					  + "Logos designed with cooltext.com\n\n"
					  + "Some art and sounds from\nopengameart.org";
			break;

			case 2:
			text.text = "Backgrounds from\nSketchup Texture Club\nhttp://www.sketchuptextureclub.com\n\n"
			          + "Sounds from:\nfreesound.org\nhttp://www.freesfx.co.uk\nsoundbible.com\n\n"
					  + "Menu and Game Over backgrounds by\nhttp://www.ironstarmedia.co.uk\n\n"
				 	  + "Special Thanks to\nAwesome Tuts\nhttp://www.awesometuts.com\nOur testers\nand MANY MORE!";
			break;

			case 3:
			text.text = "(c) 2017 Cool Nut Gaming\nAll Rights Reserved\n\n\n\n"
				   	  + "Visit us at\nhttp://coolnutgaming.wordpress.com\n\nand show your support if you want more games like this!";
			break;

		}

	}
}
