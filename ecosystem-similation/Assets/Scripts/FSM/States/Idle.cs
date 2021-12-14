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
        // TODO: if thirsty exit
        animal.isThirsty = (animal.curThirst < 70);
        // TODO: if horny exit

        // after curtain time exit


        if (_waitTime <= 0f)
        {
            isWaitTimeOver = true;
        }

        if (animal.curHunger <= 0 || animal.curThirst <= 0)
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

        else if (isWaitTimeOver && (animal.isHungry || animal.isThirsty))
        {
            if (animal.isHungry && (animal.curHunger <= animal.curThirst))
            {
                stateMachine.ChangeState(animal.searchForFood);
            }
            else if (animal.isThirsty && (animal.curThirst < animal.curHunger))
            {
                stateMachine.ChangeState(animal.searchForWater);
            }
            else
            {
                Debug.LogError("Logic Error");
            }
        }
        else if (isWaitTimeOver)
        {
            stateMachine.ChangeState(animal.wanderAround);
        }
    }
}
