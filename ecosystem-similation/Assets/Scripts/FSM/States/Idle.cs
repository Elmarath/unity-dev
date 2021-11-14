using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool isWaitTimeOver;
    private float _waitTime;
    private bool _isHungry;

    public Idle(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Idle");

        base.Enter();
        _waitTime = animal.waitTime;

        isWaitTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _waitTime -= Time.deltaTime;

        // if sees a predator exit

        // TODO: if hungry exit
        _isHungry = animal.isHungry;
        // TODO: if thirsty exit

        // TODO: if horny exit

        // after curtain time exit


        if (_waitTime <= 0f)
        {
            isWaitTimeOver = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isWaitTimeOver && _isHungry)
        {
            stateMachine.ChangeState(animal.searchForFood);
        }
        else if (isWaitTimeOver)
        {
            stateMachine.ChangeState(animal.wanderAround);
        }
    }
}
