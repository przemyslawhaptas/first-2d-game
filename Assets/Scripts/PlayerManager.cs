using System;
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
        facingRight = true;
	}

	// Update is called once per frame
	void Update () {

		MovePlayer (speed);
        Flip();

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

    private void Flip()
    {
        if (speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 tmp = transform.localScale;
            tmp.x *= -1;
            transform.localScale = tmp;
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

    public void WalkLeft()
    {
        speed = -speedX;
    }

    public void WalkRight()
    {
        speed = speedX;
    }

    public void StopMoving()
    {
        speed = 0;
    }

    public void Jump()
    {
        if (!jumping)
        {
            jumping = true;
            rBody.AddForce(new Vector2(rBody.velocity.x, jumpSpeedY));
            SetJumpingAnim();
        }
    }

}
