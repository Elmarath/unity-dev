using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool isWaitTimeOver;
    private float _waitTime;
    private bool _isHungry;
    private bool readyToDie;

    public Idle(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _waitTime = animal.waitTime;

        isWaitTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
        readyToDie = false;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _waitTime -= Time.deltaTime;

        // if sees a predator exit

        // TODO: if hungry exit
        animal.isHungry = (animal.curHunger < 70);
        _isHungry = animal.isHungry;
        // TODO: if thirsty exit

        // TODO: if horny exit

        // after curtain time exit


        if (_waitTime <= 0f)
        {
            isWaitTimeOver = true;
        }

        if (animal.curHunger <= 0)
        {
            readyToDie = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (readyToDie)
        {
            animal.Die();
        }

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
