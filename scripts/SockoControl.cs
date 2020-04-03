using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockoControl : MonoBehaviour {

	[SerializeField]
	private GameObject sockoBurst, sockoParticleAnimation, sockoParticleAnimation2;

	public static SockoControl sockoInstance;

	private float pixelsPerX, pixelsPerY;
	private float worldHeight, worldWidth;
	private float mouseX, mouseY;

	public bool gameStarted;

	public bool gamePaused;

	bool updatedFPS;	//TEST -- remove on final release
	float fps = 0.0f;	//TEST -- remove on final release

	void Awake() {
		MakeSingleton();
	}

	// Use this for initialization
	void Start () {
		//StartCoroutine(UpdateFps()); //TEST -- remove on final release
		gameStarted = false;
		gamePaused = false;
		worldHeight = Camera.main.orthographicSize * 2f;
		worldWidth = worldHeight / Screen.height * Screen.width;
		pixelsPerX = Screen.width / worldWidth;
		pixelsPerY = Screen.height / worldHeight;
	}
	
	// Update is called once per frame
	void Update () {
		//TEST -- remove this statement on final release
		/*
		if(updatedFPS == true) {
			StartCoroutine(UpdateFps());
		}*/
		//End of TEST

		if(Input.GetMouseButtonDown(0) && gameStarted == false && gamePaused == false) {
			float tempX = Input.mousePosition.x / pixelsPerX;
			float tempY = Input.mousePosition.y / pixelsPerY;
			mouseX = tempX - (worldWidth / 2);
			mouseY = tempY - (worldHeight / 2);
			InstantiateSocko();
		}
	}

	void InstantiateSocko() {
		Instantiate(sockoBurst, new Vector3(mouseX, mouseY + Camera.main.transform.position.y), Quaternion.identity);
		Instantiate(sockoParticleAnimation, new Vector3(mouseX, mouseY + Camera.main.transform.position.y), Quaternion.identity);
		Instantiate(sockoParticleAnimation2, new Vector3(mouseX, mouseY + Camera.main.transform.position.y), Quaternion.identity);

	}

	void MakeSingleton() {
		if(sockoInstance != null) {
			Destroy(gameObject);
		} else {
			sockoInstance = this;
			DontDestroyOnLoad(gameObject);
		}
	}


	//TEST -- remove all below on final release
	/*
	IEnumerator UpdateFps ()
	{
		updatedFPS = false;
		fps = 1/Time.deltaTime;
		yield return new WaitForSeconds(2f);
		updatedFPS = true;
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (8, 8, 256, 128), string.Format("<size=24>fps{0}</size>", fps));
	}*/
}
