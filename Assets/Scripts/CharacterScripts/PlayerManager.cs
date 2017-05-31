using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : BaseCharacterManager
{
    public int PushBackValue = 80;
    private Transform firePosition;
	public GameObject leftBullet, rightBullet;
    public float JumpSpeedY;

    public new void Start() {
		Health = 9;
        firePosition = transform.Find ("firePos");
        base.Start();
	}

    public new void Update () {
        CheckControls();
        base.Update();
    }

    public void CheckControls()
    {
        if (Alive)
        {
            CheckInputStatus();
            SetVelocity();
        }
    }

    protected void CheckInputStatus()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RunLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {       
            RunRight();
        }

        if (!Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.RightArrow))
        {
            StopMoving();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (FacingDirection == Directions.Right)
        {
            Instantiate(rightBullet, firePosition.position, Quaternion.identity);
        }
        else
        {
            Instantiate(leftBullet, firePosition.position, Quaternion.identity);
        }
    }

    public void Jump()
    {
        if (!Jumping)
        {
            Jumping = true;
			jumpSound.Play ();
            rBody.AddForce(new Vector3(rBody.velocity.x, JumpSpeedY, 0));
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var otherObject = other.gameObject.tag;

        switch (otherObject)
        {
            case "DestroyableObject":
            case "UndestroyableObject":
            case "GROUND":
                Jumping = false;
                break;
            case "LAVA":
                Alive = false;
                break;
            case "NextLevel":
                SceneManager.LoadScene("GameFinished"); // TODO - load next level
                break;          
            case "Enemy":
                var enemyObject = other.gameObject.GetComponent<EnemyManager>();
                HandleEnemyCollision(enemyObject);
                break;
        }
    }

    private void HandleEnemyCollision(EnemyManager enemy)
    {
        if (enemy.Alive)
        {
            Health -= 1;
			hurtSound.Play ();
            PushbackPlayer();
        }
    }

    private void PushbackPlayer()
    {
        Jumping = true;

        float pushbackX = 0;
        if (FacingDirection == Directions.Left)
        {
            pushbackX = PushBackValue * PlayerSpeedX;
        }
        else
        {
            pushbackX = PushBackValue * -1 * PlayerSpeedX;
        }

        rBody.AddForce(new Vector3(pushbackX, (float)(JumpSpeedY * 0.6), 0));
    }

    public override void Die()
    {
        SceneManager.LoadScene("GameOver");
        base.Die();
    }
}
