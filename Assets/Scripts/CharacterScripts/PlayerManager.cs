using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : BaseCharacterManager
{
    public int PushBackValue = 80;
    private Transform firePosition;
	public GameObject leftBullet, rightBullet;
    public float JumpSpeedY;

	private Text HealthText;

    public new void Start() {
		Health = 9;
		HealthText = GameObject.FindWithTag("HealthText").GetComponent<Text>();
		HealthText.text = Health.ToString();
        firePosition = transform.Find ("firePos");
        base.Start();
	}

    public new void Update () {
        CheckControls();
        SetVelocity();
    }

    public void CheckControls()
    {
        if (Alive)
        {
            CheckInputStatus();
        }
    }

    protected void CheckInputStatus()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ControlsLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {       
            ControlsRight();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.RightArrow))
        {
            ControlsStop();
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

    public void ControlsLeft()
    {
        RunLeft();
        SetVelocity();
    }

    public void ControlsRight()
    {
        RunRight();
        SetVelocity();
    }

    public void ControlsStop()
    {
        StopMoving();
        SetVelocity();
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
			HealthText.text = Health.ToString();
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
