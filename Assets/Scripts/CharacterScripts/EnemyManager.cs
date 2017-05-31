using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = System.Random;

public partial class EnemyManager : BaseCharacterManager
{
    private MoveState currentState;
    private Random rnd;

    public int ChangeStateProbability = 5;

    public new void Start()
    {
        base.Start();
        Health = 2;
        currentState = MoveState.Stay;
        rnd = new Random();
    }

    public new void Update()
    {
        if (Alive)
        {
            ConsiderChangeState();
            SetVelocity();
        }

        base.Update();
    }

    private void ConsiderChangeState()
    {
        var changeState = rnd.Next(0, 100);
        if (changeState <= ChangeStateProbability)
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        switch (currentState)
        {
            case MoveState.Left:
                ChangeStateFromLeft();
                break;
            case MoveState.Right:
                ChangeStateFromRight();
                break;
            case MoveState.Stay:
                ChangeStateFromStay();
                break;
        }
    }

    private void ChangeStateFromStay()
    {
        var nextState = rnd.Next(0, 1);
        if (nextState == 0)
        {
            currentState = MoveState.Left;
            MoveLeft();
        }
        else
        {
            currentState = MoveState.Right;
            MoveRight();
        }
    }

    private void ChangeStateFromRight()
    {
        var nextState = rnd.Next(0, 1);
        if (nextState == 0)
        {
            currentState = MoveState.Left;
            MoveLeft();
        }
        else
        {
            currentState = MoveState.Stay;
            Stay();
        }
    }

    private void ChangeStateFromLeft()
    {
        var nextState = rnd.Next(0, 1);
        if (nextState == 0)
        {
            currentState = MoveState.Right;
            MoveRight();
        }
        else
        {
            currentState = MoveState.Stay;
            Stay();
        }
    }

    private void Stay()
    {
        StopMoving();
    }

    private void MoveRight()
    {
        RunRight();
    }

    private void MoveLeft()
    {
        RunLeft();
    }
}
