using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class BaseCharacterManager : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rBody;
    protected AnimationManager animationManager;

    protected int health;
    protected float speedX;
    protected bool jumping = false;
    protected Directions facingDirection;
    protected bool alive;

    public float PlayerSpeedX;

    public AudioSource hurtSound;
    public AudioSource jumpSound;

    public bool Alive
    {
        get { return alive; }
        set
        {
            if (!value)
            {
                Die();
            }

            alive = value;
        }
    }

    public Directions FacingDirection
    {
        get { return facingDirection; }
        set
        {
            if (value != facingDirection)
            {
                Vector3 tmp = transform.localScale;
                tmp.x *= -1;
                transform.localScale = tmp;

                facingDirection = facingDirection == Directions.Left ? Directions.Right : Directions.Left;
            }
        }
    }

    public bool Jumping
    {
        get { return jumping; }
        set
        {
            if (value == jumping)
            {
                return;
            }

            if (value)
            {
                animationManager.SetJumpingAnim();
            }
            else
            {
                animationManager.SetIdleAnim();
            }

            jumping = value;
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (value == 0)
            {
                Alive = false;
            }
        }
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        animationManager = new AnimationManager(animator);
        Alive = true;
    }

    public void Update()
    {
    }

    public virtual void Die()
    {
        animationManager.SetDyingAnim();
    }

    public void RunLeft()
    {
        speedX = -PlayerSpeedX;
        FacingDirection = Directions.Left;

        if (!Jumping)
        {
            animationManager.SetRunningAnim();
        }
    }

    public void RunRight()
    {
        speedX = PlayerSpeedX;
        FacingDirection = Directions.Right;

        if (!Jumping)
        {
            animationManager.SetRunningAnim();
        }
    }

    public void StopMoving()
    {
        speedX = 0;

        if (!Jumping)
        {
            animationManager.SetIdleAnim();
        }
    }

    public void SetVelocity()
    {
        rBody.velocity = new Vector3(speedX, rBody.velocity.y, 0);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var otherObject = other.gameObject.tag;

        switch (otherObject)
        {
            case "LAVA":
                Alive = false;
                break;
        }
    }
}
