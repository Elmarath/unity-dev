using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool isIdle;
    private bool isWaitTimeOver;
    private float _waitTime;

    public Idle(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _waitTime = animal.waitTime;

        isIdle = true;
        isWaitTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
        isIdle = false;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _waitTime -= Time.deltaTime;

        // if sees a predator exit

        // TODO: if sees a food and hungry exit

        // TODO: if sees a water and thirsty exit

        // TODO: if sees a mate and horny exit

        // after curtain time exit
        if(_waitTime <= 0f)
        {
            isWaitTimeOver = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isWaitTimeOver)
        {
            stateMachine.ChangeState(animal.wanderAround);
        }
    }
}
