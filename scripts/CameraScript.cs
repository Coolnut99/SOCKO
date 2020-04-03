using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	const float baseMaxSpeed = 4f;

	private float startSpeed = 1f;
	public float currentSpeed;
	private float startAcceleration = 0.04f;
	private float currentAcceleration;
	private float maxSpeed = baseMaxSpeed;
	private float slowSpeed;

	public float currentY, lastY;
	public float beforeWindowY;

	[HideInInspector]
	public bool moveCamera;

	// Use this for initialization
	void Start () {
		SetMaxSpeed();
		ResetSpeed();
		moveCamera = true;
		SetSlowSpeed();
	}
	
	// Update is called once per frame
	void Update () {
		if (moveCamera) {
			MoveCamera();
		}	
	}

	public void ResetSpeed() {
		if(startSpeed < slowSpeed) {
			startSpeed = slowSpeed;
		}
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

	void SetMaxSpeed() {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
			maxSpeed *= 0.8f;
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
			maxSpeed *= 1.2f;
		}
	}

	void SetSlowSpeed() {
		if(MasterControl.instance.set_difficulty == MasterControl.difficulty.EASY) {
			slowSpeed = 0.6f;
		} else if (MasterControl.instance.set_difficulty == MasterControl.difficulty.HARD) {
			slowSpeed = 1f;
		} else {
			slowSpeed = 0.8f;
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
