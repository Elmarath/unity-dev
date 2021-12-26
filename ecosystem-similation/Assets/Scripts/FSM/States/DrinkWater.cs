using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : State
{
    private bool isFull;
    private GameObject waterFound;

    private bool exitState;

    public DrinkWater(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (!animal.foundedWater)
        {
            exitState = true;
        }
        isFull = animal.curThirst < 1f;

        if (!isFull)
        {
            animal.curThirst -= Time.deltaTime * animal.drinkingRate;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (isFull)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (exitState)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
