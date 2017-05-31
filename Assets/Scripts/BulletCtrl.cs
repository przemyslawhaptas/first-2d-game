using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
	public Vector2 speed;

	private Rigidbody2D rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.velocity = speed;
	}

	void Update () {
		rigidBody.velocity = speed;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("DestroyableObject")) {
			Destroy (other.gameObject);
		}

		if (other.gameObject.CompareTag ("Enemy")) {
			EnemyManager enemyObject = other.gameObject.GetComponent<EnemyManager> ();

			if (enemyObject.Alive) {
				AudioSource enemyHurtSound = other.gameObject.GetComponent<AudioSource> ();
				enemyObject.Health -= 1;
				enemyHurtSound.Play ();
			}
		}

        Destroy(gameObject);
    }
}
