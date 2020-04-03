using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinCameraScript : MonoBehaviour {

	const float baseMaxSpeed = 4f;

	private float startSpeed = 2f;
	public float currentSpeed;
	private float startAcceleration = 0f;
	private float currentAcceleration;
	private float maxSpeed = baseMaxSpeed;
	private float slowSpeed;

	public float currentY, lastY;
	public float beforeWindowY;

	[HideInInspector]
	public bool moveCamera;

	// Use this for initialization
	void Start () {
		ResetSpeed();
		moveCamera = true;
		slowSpeed = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (moveCamera) {
			MoveCamera();
		}	
	}

	public void ResetSpeed() {
		currentSpeed = startSpeed;
		currentAcceleration = startAcceleration;
	}

	public void SetCameraSpeed(float c_speed) {
		startSpeed = c_speed;
		ResetSpeed();
	}

	public void SetCameraSpeed(float c_speed, float c_acceleration) {
		startSpeed = c_speed;
		startAcceleration = c_acceleration;
		ResetSpeed();
	}

	public void SetCameraSpeed(float c_speed, float c_acceleration, float c_maxSpeed) {
		startSpeed = c_speed;
		startAcceleration = c_acceleration;
		maxSpeed = c_maxSpeed;
		ResetSpeed();
	}

	public void SetSlowSpeed(bool b) {
		if (b == true) {
			slowSpeed = 0.5f;
		} else {
			slowSpeed = 1f;
		}
	}



	void MoveCamera() {
		Vector3 temp = transform.position;

		float oldY = temp.y;
		lastY = oldY;
		float newY = temp.y + (currentSpeed * Time.deltaTime) * slowSpeed;
		currentY = newY;

		beforeWindowY += (currentY - lastY);

		temp.y = Mathf.Clamp(temp.y, newY, oldY);
		transform.position = temp;

		currentSpeed += currentAcceleration * Time.deltaTime;

		if(currentSpeed	< slowSpeed) {
			currentSpeed = slowSpeed;
		}

		if(currentSpeed > maxSpeed) {
			currentSpeed = maxSpeed;
		}
	}
}
