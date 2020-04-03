using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistTable : MonoBehaviour {

	//This is a singleton, have this run in the "Splash" page

	public static FistTable instance;

	public int fistNumber;
	public float fistRate, fistRadiusScale, fistPowerupsScale;
	public int fistSpecial;	//0 means no special power
	public int fistCost;
	public int fistSpriteType; // Which fist is being used
	public string fistSpecialDescription;
	public string fistName;

	void Awake() {
		MakeSingleton();
	}

	public void SetFist(Sprite s, Color c) {
		GetComponent<SpriteRenderer>().sprite = s;
		GetComponent<SpriteRenderer>().color = c;
	}

	public void FistStatTable(int SelectFist) {
		switch (SelectFist) {
			case 1:
			fistNumber = 1;
			fistRate = 5f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 1f;
			fistSpecial = 0;
			fistSpriteType = 3;
			fistCost = 0;
			fistName = "SUPER STANDARD SOCKO";
			break;

			case 2:
			fistNumber = 2;
			fistRate = 4f;
			fistRadiusScale = 1.2f;
			fistPowerupsScale = 1f;
			fistSpecial = 2;
			fistSpriteType = 3;
			fistCost = 100;
			fistName = "THE PYRITE STANDARD";
			break;

			case 3:
			fistNumber = 3;
			fistRate = 4f;
			fistRadiusScale = 1.2f;
			fistPowerupsScale = 1f;
			fistSpecial = 3;
			fistSpriteType = 3;
			fistCost = 100;
			fistName = "BRASS KICKER";
			break;

			case 4:
			fistNumber = 4;
			fistRate = 5f;
			fistRadiusScale = 2f;
			fistPowerupsScale = 0.8f;
			fistSpecial = 0;
			fistSpriteType = 3;
			fistCost = 200;
			fistName = "DUDE HAND";
			break;

			case 5:
			fistNumber = 5;
			fistRate = 5f;
			fistRadiusScale = 1.5f;
			fistPowerupsScale = 0.8f;
			fistSpecial = 4;
			fistSpriteType = 3;
			fistCost = 300;
			fistName = "FIST OF BLOCKBUSTING";
			break;

			case 6:
			fistNumber = 6;
			fistRate = 4f;
			fistRadiusScale = 1.5f;
			fistPowerupsScale = 1f;
			fistSpecial = 1;
			fistSpriteType = 3;
			fistCost = 400;
			fistName = "FIGHTING SPECIAL";
			break;

			case 7:
			fistNumber = 7;
			fistRate = 6f;
			fistRadiusScale = 2.2f;
			fistPowerupsScale = 0.6f;
			fistSpecial = 0;
			fistSpriteType = 3;
			fistCost = 400;
			fistName = "RED ALERT";
			break;

			case 8:
			fistNumber = 8;
			fistRate = 3f;
			fistRadiusScale = 3.5f;
			fistPowerupsScale = 1.2f;
			fistSpecial = 11;
			fistSpriteType = 3;
			fistCost = 400;
			fistName = "BLUE BUSTER";
			break;

			case 9:
			fistNumber = 9;
			fistRate = 7f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 1f;
			fistSpecial = 0;
			fistSpriteType = 3;
			fistCost = 500;
			fistName = "THE GREEN MACHINE";
			break;

			case 10:
			fistNumber = 10;
			fistRate = 5f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 1.5f;
			fistSpecial = 5;
			fistSpriteType = 3;
			fistCost = 500;
			fistName = "BRONZOID";
			break;

			case 11:
			fistNumber = 11;
			fistRate = 4f;
			fistRadiusScale = 2f;
			fistPowerupsScale = 1f;
			fistSpecial = 8;
			fistSpriteType = 3;
			fistCost = 600;
			fistName = "THE STINGER";
			break;

			case 12:
			fistNumber = 12;
			fistRate = 5f;
			fistRadiusScale = 1.5f;
			fistPowerupsScale = 1.2f;
			fistSpecial = 6;
			fistSpriteType = 3;
			fistCost = 750;
			fistName = "SILVER STREAK";
			break;

			case 13:
			fistNumber = 13;
			fistRate = 4f;
			fistRadiusScale = 2f;
			fistPowerupsScale = 1.5f;
			fistSpecial = 10;
			fistSpriteType = 3;
			fistCost = 800;
			fistName = "GLOVE SLAP";
			break;

			case 14:
			fistNumber = 14;
			fistRate = 5f;
			fistRadiusScale = 2f;
			fistPowerupsScale = 1f;
			fistSpecial = 16;
			fistSpriteType = 3;
			fistCost = 800;
			fistName = "MOTORMAN MASSACRE";
			break;

			case 15:
			fistNumber = 15;
			fistRate = 5f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 2f;
			fistSpecial = 14;
			fistSpriteType = 3;
			fistCost = 800;
			fistName = "PURPLE NURPLE GIVER";
			break;

			case 16:
			fistNumber = 16;
			fistRate = 5f;
			fistRadiusScale = 1.2f;
			fistPowerupsScale = 1.1f;
			fistSpecial = 12;
			fistSpriteType = 3;
			fistCost = 900;
			fistName = "THE BIG ONE";
			break;

			case 17:
			fistNumber = 17;
			fistRate = 5f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 3f;
			fistSpecial = 7;
			fistSpriteType = 3;
			fistCost = 900;
			fistName = "SUPERHEAVY";
			break;

			case 18:
			fistNumber = 18;
			fistRate = 6f;
			fistRadiusScale = 4.5f;
			fistPowerupsScale = 1.5f;
			fistSpecial = 11;
			fistSpriteType = 3;
			fistCost = 1000;
			fistName = "HAMHOCK'D";
			break;

			case 19:
			fistNumber = 19;
			fistRate = 6f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 0.1f;
			fistSpecial = 9;
			fistSpriteType = 3;
			fistCost = 1000;
			fistName = "ALMOST-DEAD MAN'S HAND";
			break;

			case 20:
			fistNumber = 20;
			fistRate = 4f;
			fistRadiusScale = 2f;
			fistPowerupsScale = 0.8f;
			fistSpecial = 17;
			fistSpriteType = 3;
			fistCost = 1000;
			fistName = "RED BONE";
			break;

			case 21:
			fistNumber = 21;
			fistRate = 4f;
			fistRadiusScale = 2f;
			fistPowerupsScale = 1f;
			fistSpecial = 18;
			fistSpriteType = 3;
			fistCost = 1200;
			fistName = "FOAM FINGERS OF DEATH";
			break;

			case 22:
			fistNumber = 22;
			fistRate = 5f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 0.5f;
			fistSpecial = 19;
			fistSpriteType = 3;
			fistCost = 1500;
			fistName = "THE FUSTIGATOR";
			break;

			case 23:
			fistNumber = 23;
			fistRate = 1f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 1f;
			fistSpecial = 13;
			fistSpriteType = 3;
			fistCost = 2000;
			fistName = "THE SOCKONOID";
			break;

			case 24:
			fistNumber = 24;
			fistRate = 5f;
			fistRadiusScale = 3f;
			fistPowerupsScale = 0.3f;
			fistSpecial = 15;
			fistSpriteType = 3;
			fistCost = 2500;
			fistName = "THE END AND THEIRS";
			break;

			default:
			fistNumber = 1;
			fistRate = 10f;
			fistRadiusScale = 1f;
			fistPowerupsScale = 1f;
			fistSpecial = 0;
			fistCost = 0;
			fistName = "SUPER STANDARD SOCKO";
			break;


		}
	}

	public void FistSpecialSwitch(int f) {
		switch(f) {
			case 0:
			fistSpecialDescription = "NONE";
			break;

			case 1:
			fistSpecialDescription = "Score 50,000 or more for 2 free coins!";
			break;

			case 2:
			fistSpecialDescription = "+200 points for each coin!";
			break;

			case 3:
			fistSpecialDescription = "+200 points for each powerup!";
			break;

			case 4:
			fistSpecialDescription = "+100 points per smiley hit!";
			break;

			case 5:
			fistSpecialDescription = "+500 points for each coin!";
			break;

			case 6:
			fistSpecialDescription = "+500 points for each powerup!";
			break;

			case 7:
			fistSpecialDescription = "Powerups appear half as often."; //length of powerups is increased 3x
			break;

			case 8:
			fistSpecialDescription = "Coins are now doubled.";
			break;

			case 9:
			fistSpecialDescription = "Coins are now tripled. Powerups except moneybags give next to no effect.";
			break;

			case 10:
			fistSpecialDescription = "Bonus powerups appear occasionally."; //1 every 30 seconds
			break;

			case 11:
			fistSpecialDescription = "Radius is much greater but watch out for bombs!"; //Super socko-sized; show effect if possible
			break;

			case 12:
			fistSpecialDescription = "Score 50,000 points for an extra life!"; //Yes, you can use this on challenges
			break;

			case 13:
			fistSpecialDescription = "Permanent SUPER SOCKO! But coins do not give anything but points.";
			break;

			case 14:
			fistSpecialDescription = "Points (except bonuses) are doubled. Score 100,000 points for an extra life. You have 1 life.";
			break;

			case 15:
			fistSpecialDescription = "Every 25,000 points is an extra life. Coins only give points.";
			break;

			case 16:
			fistSpecialDescription = "Smiley values are doubled. Window values are halved.";
			break;

			case 17:
			fistSpecialDescription = "Each 10,000 points scored gives one extra coin.";
			break;

			case 18:
			fistSpecialDescription = "Random powerup drops if you lose a life. (Not in challenge mode)";
			break;

			case 19:
			fistSpecialDescription = "Permanent bombproof! Windows don't score points.";
			break;

			default:
			fistSpecialDescription = "NONE";
			break;

		}
	}

	void MakeSingleton() {
		if(instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
}
