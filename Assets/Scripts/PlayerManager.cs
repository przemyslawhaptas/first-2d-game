using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		HandleWalking ();
		HandleRunning ();
	}

	private void HandleWalking() {
		HandleKeyHold (KeyCode.W, "State", 1, 0);
	}

	private void HandleRunning() {
		HandleKeyHold (KeyCode.R, "State", 2, 0);
	}

	private void HandleKeyHold(KeyCode keyCode, string parameter, int downState, int upState) {
		if (Input.GetKeyDown (keyCode)) {
			animator.SetInteger (parameter, downState);
		}

		if (Input.GetKeyUp (keyCode)) {
			animator.SetInteger (parameter, upState);
		}
	}
}
