
using System;
using UnityEngine;

public class MainPlayer : MonoBehaviour {

	private Vector3 mAccel;
	private Rigidbody mBody;
	private UserInputComponent mController;
	private PlayerAttributes mAttribs;
	private bool IsAirborne;

	// Use this for initialization
	public void Start () {
		mAccel = new Vector3(0.0f, 0.0f, 0.0f);
		mBody = GetComponent<Rigidbody>();
		mController = GetComponent<UserInputComponent>();
		mAttribs = GetComponent<PlayerAttributes>();
		IsAirborne = false;
	}

	public void Reset() {
		mController.Reset();
		transform.position = new Vector3(0.0f, 1.0f, 0.0f);
		mAccel = new Vector3(0.0f, 0.0f, 0.0f);
		mBody.velocity = new Vector3(0.0f, 5.0f, 0.0f);
		mBody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
	}

	public void FixedUpdate () {
		mAccel.Set(mController.AxisX, 0.0f, mController.AxisY);
		mBody.AddForce(mAccel * mAttribs.MoveSpeed);

		AddInputForces();
	}
	
	public void OnTriggerStay(Collider c) {
		if (c.tag == "kill_volume") {
			Debug.Log("Collided with " + c.name + ":" + c.tag);
			Reset ();
		}
	}

	public void AddInputForces () {
		IsAirborne = !MainPlayer.IsGrounded(transform.position, GetComponent<SphereCollider>());

		if (mController.IsPressingJump() && !IsAirborne) {
			mBody.AddForce(new Vector3(0.0f, mAttribs.MaxVertAccel * 5.0f, 0.0f));
		}
	}

	public static bool IsGrounded(Vector3 pos, Collider collider) {
		return Physics.Raycast(pos, -Vector3.up, collider.bounds.extents.y + Mathf.Epsilon);
	}
}
