using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public float speedX;
	public float jumpSpeedY;

	bool facingRight, jumping;
	float speed;

	Animator animator;
	Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		rBody = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {

		MovePlayer (speed);

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			speed = -speedX;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			speed = speedX;
		}

		if (Input.GetKeyUp (KeyCode.LeftArrow)
		    || Input.GetKeyUp (KeyCode.RightArrow)) {
			speed = 0;
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && !jumping) {
			jumping = true;
			rBody.AddForce (new Vector2 (rBody.velocity.x, jumpSpeedY));
			SetJumpingAnim ();
		}

	}

	private void MovePlayer(float playerSpeed) {

		if (!jumping) {
			if (playerSpeed == 0) {
				SetIdleAnim ();
			} else {
				SetRunningAnim ();
			}
		}

		rBody.velocity = new Vector3 (speed, rBody.velocity.y, 0);
	}


	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag.Equals("GROUND")) {
			jumping = false;
			SetIdleAnim ();
		}
	}

	private void SetIdleAnim() {
		animator.SetInteger ("State", 0);
	}

	private void SetWalkingAnim() {
		animator.SetInteger ("State", 1);
	}

	private void SetRunningAnim() {
		animator.SetInteger ("State", 2);
	}

	private void SetJumpingAnim() {
		animator.SetInteger ("State", 3);
	}



//		HandleWalking ();
//		HandleRunning ();
//		HandleJumping ();
//		HandleDying ();}
//
//	private void HandleWalking() {
//		HandleKeyHold (KeyCode.W, "State", 1, 0);
//	}
//
//	private void HandleRunning() {
//		HandleKeyHold (KeyCode.R, "State", 2, 0);
//	}
//
//	private void HandleJumping() {
//		HandleKeyHold (KeyCode.E, "State", 3, 0);
//	}
//
//	private void HandleDying() {
//		HandleKeyHold (KeyCode.D, "State", 5, 0);
//	}


//	private void HandleKeyHold(KeyCode keyCode, string parameter, int downState, int upState) {
//		if (Input.GetKeyDown (keyCode)) {
//			animator.SetInteger (parameter, downState);
//		}
//
//		if (Input.GetKeyUp (keyCode)) {
//			animator.SetInteger (parameter, upState);
//		}
//	}
}
