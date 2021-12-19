using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool isWaitTimeOver;
    private float _waitTime;
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

        // TODO: if sees a predator exit

        animal.isHungry = (animal.curHunger < 70f);
        animal.isThirsty = (animal.curThirst < 70f);
        animal.isHorny = (animal.curHorny < 70f);
        animal.readyToBirth = (animal.curPergnantPersentance > 99f);

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

        else if (isWaitTimeOver && (animal.isHungry || animal.isThirsty || animal.isHorny || animal.readyToBirth))
        {
            if (animal.readyToBirth)
            {
                stateMachine.ChangeState(animal.makeBirth); 
            }
            else if (animal.isHungry && (animal.curHunger <= animal.curThirst) && (animal.curHunger <= animal.curHorny))
            {
                stateMachine.ChangeState(animal.searchForFood);
            }
            else if (animal.isThirsty && (animal.curThirst < animal.curHunger) && (animal.curThirst < animal.curHorny))
            {
                stateMachine.ChangeState(animal.searchForWater);
            }
            else if (animal.isHorny && (animal.curHorny < animal.curHunger) && (animal.curHorny < animal.curThirst))
            {
                stateMachine.ChangeState(animal.searchForMate);
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
